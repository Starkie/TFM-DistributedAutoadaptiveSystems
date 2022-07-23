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
    ClimatisationConstants.Configuration.HotTemperatureThreshold,
    ClimatisationAirConditionerConstants.Configuration.Mode)]
public class EnableAirConditionerCoolingModeWhenTemperatureThresholdExceededAdaptionRule : AdaptionRuleBase
{
    private const string RuleName = nameof(EnableAirConditionerCoolingModeWhenTemperatureThresholdExceededAdaptionRule);

    private const string TemperatureGreaterThanHotThreshold = "temperature-greater-than-hot-threshold";

    private static readonly IEnumerable<string> propertyNames =
        typeof(EnableAirConditionerCoolingModeWhenTemperatureThresholdExceededAdaptionRule)
            .GetRulePropertyDependencies();

    private static readonly IDictionary<string, IEnumerable<string>> configurationNames =
        typeof(EnableAirConditionerCoolingModeWhenTemperatureThresholdExceededAdaptionRule)
            .GetRuleConfigurationDependencies();

    private readonly IConfigurationService _configurationService;

    private readonly IPropertyService _propertyService;

    private readonly ISystemService _systemService;

    public EnableAirConditionerCoolingModeWhenTemperatureThresholdExceededAdaptionRule(
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

        var thresholdTemperature = await _configurationService.GetConfigurationKey<float?>(
            ClimatisationAirConditionerConstants.AppName,
            ClimatisationConstants.Configuration.HotTemperatureThreshold,
            CancellationToken.None);

        return currentTemperature.Value >= thresholdTemperature
            && airConditionerMode != AirConditioningMode.Cooling;
    }

    protected override async Task Execute()
    {
       // TODO: Comparación esta API vs código original con DTOs.
        await _systemService.RequestConfigurationChange(changeRequest =>
        {
            changeRequest
                .ForSymptom(TemperatureGreaterThanHotThreshold)
                .WithService(ClimatisationAirConditionerConstants.AppName, service =>
                {
                    service.MustBePresent()
                        .WithParameter(
                            ClimatisationAirConditionerConstants.Configuration.Mode,
                            AirConditioningMode.Cooling.ToString());
                });
        });
    }
}
