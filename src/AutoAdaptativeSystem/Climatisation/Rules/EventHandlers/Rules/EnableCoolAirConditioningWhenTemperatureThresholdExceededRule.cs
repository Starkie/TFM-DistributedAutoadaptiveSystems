namespace Climatisation.Rules.Service.EventHandlers.Rules;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Analysis.Contracts.Attributes;
using Analysis.Service.ApiClient.Api;
using Analysis.Service.ApiClient.Model;
using Climatisation.Contracts;
using Climatisation.Rules.Service.Diagnostics;
using Climatisation.Rules.Service.Services;
using Microsoft.Extensions.DependencyInjection;

[RuleKnowledgePropertyDependency(Temperature, HotTemperatureThreshold)]
[RuleKnowledgeConfigurationDependency(AirConditioningServiceName, ModeConfiguration)]
public class EnableCoolAirConditioningWhenTemperatureThresholdExceededRule : RuleBase
{
    private const string HotTemperatureThreshold = "HotTemperatureThreshold";

    private const string RuleName = nameof(EnableCoolAirConditioningWhenTemperatureThresholdExceededRule);

    private const string Temperature = "Temperature";

    private const string CoolingMode = "Cooling";

    // TODO: Extract to constants of the Air Conditiniong service.
    private const string AirConditioningServiceName = "airconditioning-service";

    private const string SymptomName = "temperature-greater-than-target";

    private const string ModeConfiguration = "Mode";

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

        var isEnabled = await _configurationService.GetConfigurationKey<bool?>(AirConditioningServiceName, ModeConfiguration, CancellationToken.None);

        var thresholdTemperature = await _configurationService.GetConfigurationKey<float?>(
            AirConditioningServiceName,
        HotTemperatureThreshold,
            CancellationToken.None);

        thresholdTemperature ??= 25.0f;

        return isEnabled != true && currentTemperature.Value > thresholdTemperature;
    }

    protected override async Task Execute()
    {
        var changeRequests = new List<ServiceConfigurationDTO>
        {
            new()
            {
                ServiceName = AirConditioningServiceName,
                IsDeployed = true,
                ConfigurationProperties = new List<ConfigurationProperty>()
                {
                    new() { Name = ModeConfiguration, Value = CoolingMode },
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
