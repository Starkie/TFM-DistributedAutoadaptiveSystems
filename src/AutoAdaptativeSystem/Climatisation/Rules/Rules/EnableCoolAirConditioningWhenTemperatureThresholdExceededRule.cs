namespace Climatisation.Rules.Rules;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Climatisation.Contacts;
using Climatisation.Rules.Diagnostics;
using Climatisation.Rules.Services;

public class EnableCoolAirConditioningWhenTemperatureThresholdExceededRule : RuleBase
{
    private readonly IPropertyService _propertyService;

    private const string RuleName = nameof(EnableCoolAirConditioningWhenTemperatureThresholdExceededRule);

    private const string Temperature = "Temperature";

    private static readonly IEnumerable<string> Properties = new[]
    {
        Temperature,
    };

    public EnableCoolAirConditioningWhenTemperatureThresholdExceededRule(
        ClimatisationRulesDiagnostics diagnostics,
        IPropertyService propertyService)
        : base(diagnostics, RuleName, Properties)
    {
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

        // return false;
    }

    protected override async Task Execute(CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return;
        }

        // await _propertyApi.Get
    }
}
