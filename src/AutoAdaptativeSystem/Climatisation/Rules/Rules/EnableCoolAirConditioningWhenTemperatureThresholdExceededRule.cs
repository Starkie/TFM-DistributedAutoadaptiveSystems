namespace Climatisation.Rules.Rules;

using System.Collections.Generic;
using System.Threading.Tasks;
using Climatisation.Rules.Diagnostics;

public class EnableCoolAirConditioningWhenTemperatureThresholdExceededRule : RuleBase
{
    private const string RuleName = nameof(EnableCoolAirConditioningWhenTemperatureThresholdExceededRule);

    private static readonly IEnumerable<string> Properties = new[]
    {
        "temperature",
    };

    public EnableCoolAirConditioningWhenTemperatureThresholdExceededRule(ClimatisationRulesDiagnostics diagnostics)
        : base(diagnostics, RuleName, Properties)
    {
    }

    public override Task<bool> EvaluateCondition()
    {
        return Task.FromResult(true);
    }

    public override Task<bool> Execute()
    {
        return Task.FromResult(true);
    }
}
