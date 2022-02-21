namespace AnalysisService.Contracts.Rules;

public interface IRule
{
    public static IEnumerable<string> properties = new[] { "temperature" };

    public Task<bool> EvaluateCondition();

    public Task<bool> Execute();
}
