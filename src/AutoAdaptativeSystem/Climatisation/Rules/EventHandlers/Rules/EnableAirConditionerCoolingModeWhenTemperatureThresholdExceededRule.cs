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

[RuleKnowledgePropertyDependency(ClimatisationConstants.Property.Temperature)]
[RuleKnowledgeConfigurationDependency(
    ClimatisationAirConditionerConstants.AppName,
    ClimatisationConstants.Configuration.HotTemperatureThreshold,
    ClimatisationAirConditionerConstants.Configuration.Mode)]
public class EnableAirConditionerCoolingModeWhenTemperatureThresholdExceededRule : RuleBase
{
    private const string RuleName = nameof(EnableAirConditionerCoolingModeWhenTemperatureThresholdExceededRule);

    private const string SymptomName = "temperature-greater-than-target";

    private static readonly IEnumerable<string> propertyNames =
        typeof(EnableAirConditionerCoolingModeWhenTemperatureThresholdExceededRule)
            .GetRulePropertyDependencies();

    private static readonly IDictionary<string, IEnumerable<string>> configurationNames =
        typeof(EnableAirConditionerCoolingModeWhenTemperatureThresholdExceededRule)
            .GetRuleConfigurationDependencies();

    private readonly IConfigurationService _configurationService;

    private readonly IPropertyService _propertyService;

    private readonly ISystemApi _systemApi;

    public EnableAirConditionerCoolingModeWhenTemperatureThresholdExceededRule(
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
        var currentTemperature =
            await _propertyService.GetProperty<TemperatureMeasurementDTO>(ClimatisationConstants.Property.Temperature, CancellationToken.None);

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
            ClimatisationConstants.Configuration.HotTemperatureThreshold,
            CancellationToken.None);

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
