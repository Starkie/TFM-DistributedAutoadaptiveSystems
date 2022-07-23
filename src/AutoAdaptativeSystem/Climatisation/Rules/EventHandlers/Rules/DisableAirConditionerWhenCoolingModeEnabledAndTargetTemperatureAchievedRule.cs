namespace Climatisation.Rules.Service.EventHandlers.Rules;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Analysis.Contracts.Attributes;
using Analysis.Service.ApiClient.Services;
using Climatisation.AirConditioner.Contracts;
using Climatisation.Contracts;
using Climatisation.Rules.Service.Diagnostics;
using Climatisation.Rules.Service.Services;
using Microsoft.Extensions.DependencyInjection;

[RuleKnowledgePropertyDependency(ClimatisationConstants.Property.Temperature)]
[RuleServiceConfigurationDependency(
    ClimatisationAirConditionerConstants.AppName,
    ClimatisationConstants.Configuration.TargetTemperature,
    ClimatisationAirConditionerConstants.Configuration.Mode)]
public class DisableAirConditionerWhenCoolingModeEnabledAndTargetTemperatureAchievedAdaptionRule : AdaptionRuleBase
{
    private const string RuleName = nameof(DisableAirConditionerWhenCoolingModeEnabledAndTargetTemperatureAchievedAdaptionRule);

    private const string TargetTemperatureAchieved = "target-temperature-achieved";

    private static readonly IEnumerable<string> propertyNames =
        typeof(DisableAirConditionerWhenCoolingModeEnabledAndTargetTemperatureAchievedAdaptionRule)
            .GetRulePropertyDependencies();

    private static readonly IDictionary<string, IEnumerable<string>> configurationNames =
        typeof(DisableAirConditionerWhenCoolingModeEnabledAndTargetTemperatureAchievedAdaptionRule)
            .GetRuleConfigurationDependencies();

    private readonly IConfigurationService _configurationService;

    private readonly IPropertyService _propertyService;

    private readonly ISystemService _systemService;

    public DisableAirConditionerWhenCoolingModeEnabledAndTargetTemperatureAchievedAdaptionRule(
        ClimatisationRulesDiagnostics diagnostics,
        IConfigurationService configurationService,
        IPropertyService propertyService,
        ISystemService systemService)
        : base(diagnostics, RuleName, propertyNames, configurationNames)
    {
        _configurationService = configurationService;
        _propertyService = propertyService;
        _systemService = systemService;
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

        var targetTemperature = await _configurationService.GetConfigurationKey<float?>(
            ClimatisationAirConditionerConstants.AppName,
            ClimatisationConstants.Configuration.TargetTemperature,
            CancellationToken.None);

        return airConditionerMode == AirConditioningMode.Cooling && currentTemperature.Value <= targetTemperature;
    }

    protected override async Task Execute()
    {
       // TODO: Comparación esta API vs código original con DTOs.
        await _systemService.RequestConfigurationChange(changeRequest =>
        {
            changeRequest
                .ForSymptom(TargetTemperatureAchieved)
                .WithService(ClimatisationAirConditionerConstants.AppName, service =>
                {
                    service.MustBePresent()
                        .WithParameter(
                            ClimatisationAirConditionerConstants.Configuration.Mode,
                            AirConditioningMode.Off.ToString());
                });
        });
    }
}
