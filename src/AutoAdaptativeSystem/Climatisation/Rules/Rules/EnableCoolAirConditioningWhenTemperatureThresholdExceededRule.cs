namespace Climatisation.Rules.Rules;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Analysis.Service.ApiClient.Api;
using Analysis.Service.ApiClient.Model;
using Climatisation.Contacts;
using Climatisation.Rules.Diagnostics;
using Climatisation.Rules.Services;

public class EnableCoolAirConditioningWhenTemperatureThresholdExceededRule : RuleBase
{
    private readonly IConfigurationApi _configurationApi;

    private readonly IPropertyService _propertyService;

    private const string RuleName = nameof(EnableCoolAirConditioningWhenTemperatureThresholdExceededRule);

    private const string Temperature = "Temperature";

    private static readonly IEnumerable<string> Properties = new[]
    {
        Temperature,
    };

    public EnableCoolAirConditioningWhenTemperatureThresholdExceededRule(
        ClimatisationRulesDiagnostics diagnostics,
        IConfigurationApi configurationApi,
        IPropertyService propertyService)
        : base(diagnostics, RuleName, Properties)
    {
        _configurationApi = configurationApi;
        _propertyService = propertyService;
    }

    protected override async Task<bool> EvaluateCondition(CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return false;
        }

        var currentTemperature = await _propertyService.GetProperty<TemperatureMeasurementDTO>(Temperature, cancellationToken);

        if (currentTemperature is null)
        {
            return false;
        }

        return currentTemperature.Value > 25.0;
    }

    protected override async Task Execute(CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return;
        }

        // TODO: Check if the AC is already enabled.

        var symptoms = new List<SymptomDTO>{ new SymptomDTO("temperature-greater-than-target", "true") };

        var changeRequests = new List<ChangeRequestDTO> { new ChangeRequestDTO("airconditioning-service", "enabled", "true") };

        await _configurationApi.ConfigurationRequestChangePostAsync(new ConfigurationChangeRequestDTO(DateTime.Now, symptoms, changeRequests), cancellationToken);
    }
}
