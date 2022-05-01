namespace Climatisation.Rules.Service.EventHandlers.Rules;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Analysis.Contracts.Attributes;
using Analysis.Service.ApiClient.Api;
using Analysis.Service.ApiClient.Model;
using Climatisation.AirConditioner.Contracts;
using Climatisation.Contracts;
using Climatisation.Rules.Service.Diagnostics;
using Climatisation.Rules.Service.Services;
using Microsoft.Extensions.DependencyInjection;

[RuleKnowledgePropertyDependency(Temperature, HotTemperatureThreshold)]
[RuleKnowledgeConfigurationDependency(
    ClimatisationAirConditionerConstants.AppName,
    ClimatisationAirConditionerConstants.Configuration.Mode)]
public class EnableCoolAirConditioningWhenTemperatureThresholdExceededRule : RuleBase
{
    private const string HotTemperatureThreshold = "HotTemperatureThreshold";

    private const string RuleName = nameof(EnableCoolAirConditioningWhenTemperatureThresholdExceededRule);

    private const string Temperature = "Temperature";

    private const string SymptomName = "temperature-greater-than-target";

    private static readonly IEnumerable<string> propertyNames =
        typeof(EnableCoolAirConditioningWhenTemperatureThresholdExceededRule)
            .GetRulePropertyDependencies();

    private static readonly IDictionary<string, IEnumerable<string>> configurationNames =
        typeof(EnableCoolAirConditioningWhenTemperatureThresholdExceededRule)
            .GetRuleConfigurationDependencies();

    private readonly IConfigurationService _configurationService;

    private readonly IPropertyService _propertyService;

    private readonly ISystemApi _systemApi;

    public EnableCoolAirConditioningWhenTemperatureThresholdExceededRule(
        ClimatisationRulesDiagnostics diagnostics,
        IConfigurationService configurationService,
        IPropertyService propertyService,
        ISystemApi systemApi)
        : base(diagnostics, RuleName, propertyNames, configurationNames)
    {
        _configurationService = configurationService;
        _propertyService = propertyService;
        _systemApi = systemApi;
    }

    protected override async Task<bool> EvaluateCondition()
    {
        var currentTemperature = await _propertyService.GetProperty<TemperatureMeasurementDTO>(Temperature, CancellationToken.None);

        if (currentTemperature is null)
        {
            return false;
        }

        var airConditionerMode = await _configurationService.GetConfigurationKey<AirConditioningMode?>(
            ClimatisationAirConditionerConstants.AppName,
            ClimatisationAirConditionerConstants.Configuration.Mode,
            CancellationToken.None);

        var thresholdTemperature = await _configurationService.GetConfigurationKey<float?>(
            ClimatisationAirConditionerConstants.AppName,
        HotTemperatureThreshold,
            CancellationToken.None);

        thresholdTemperature ??= 25.0f;

        return currentTemperature.Value > thresholdTemperature
            && airConditionerMode != AirConditioningMode.Cooling;
    }

    protected override async Task Execute()
    {
        var changeRequests = new List<ServiceConfigurationDTO>
        {
            new()
            {
                ServiceName = ClimatisationAirConditionerConstants.AppName,
                IsDeployed = true,
                ConfigurationProperties = new List<ConfigurationProperty>()
                {
                    new()
                    {
                        Name = ClimatisationAirConditionerConstants.Configuration.Mode,
                        Value = AirConditioningMode.Cooling.ToString(),
                    },
                },
            },
        };

        var symptoms = new List<SymptomDTO> { new(SymptomName, "true") };

        var systemConfigurationChangeRequest = new SystemConfigurationChangeRequestDTO()
        {
            ServiceConfiguration = changeRequests,
            Symptoms = symptoms,
            Timestamp = DateTime.UtcNow,
        };

        await _systemApi.SystemRequestChangePostAsync(systemConfigurationChangeRequest, CancellationToken.None);
    }
}
