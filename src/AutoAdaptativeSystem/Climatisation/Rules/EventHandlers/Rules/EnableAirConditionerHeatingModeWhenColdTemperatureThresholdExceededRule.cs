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
    ClimatisationConstants.Configuration.ColdTemperatureThreshold,
    ClimatisationAirConditionerConstants.Configuration.Mode)]
public class EnableAirConditionerHeatingModeWhenColdTemperatureThresholdExceededAdaptionRule : AdaptionRuleBase
{
    private const string RuleName = nameof(EnableAirConditionerHeatingModeWhenColdTemperatureThresholdExceededAdaptionRule);

    private const string TemperatureLesserThanColdThreshold = "temperature-lesser-than-cold-threshold";

    private static readonly IEnumerable<string> propertyNames =
        typeof(EnableAirConditionerHeatingModeWhenColdTemperatureThresholdExceededAdaptionRule)
            .GetRulePropertyDependencies();

    private static readonly IDictionary<string, IEnumerable<string>> configurationNames =
        typeof(EnableAirConditionerHeatingModeWhenColdTemperatureThresholdExceededAdaptionRule)
            .GetRuleConfigurationDependencies();

    private readonly IConfigurationService _configurationService;

    private readonly IPropertyService _propertyService;

    private readonly ISystemService _systemService;

    public EnableAirConditionerHeatingModeWhenColdTemperatureThresholdExceededAdaptionRule(
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
            ClimatisationConstants.Configuration.ColdTemperatureThreshold,
            CancellationToken.None);

        return currentTemperature.Value <= thresholdTemperature
            && airConditionerMode != AirConditioningMode.Heating;
    }

    protected override async Task Execute()
    {
       // TODO: Comparación esta API vs código original con DTOs.
        await _systemService.RequestChangeAsync(changeRequest =>
        {
            changeRequest
                .ForSymptom(TemperatureLesserThanColdThreshold)
                .WithService(ClimatisationAirConditionerConstants.AppName, service =>
                {
                    service.MustBePresent()
                        .WithParameter(
                            ClimatisationAirConditionerConstants.Configuration.Mode,
                            AirConditioningMode.Heating.ToString());
                });
        });
    }
}
