namespace Climatisation.Rules.Rules;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AnalysisService.Contracts.Rules;
using Climatisation.Rules.Diagnostics;
using Climatisation.Rules.Events;
using MediatR;

public abstract class RuleBase : INotificationHandler<PropertyChangedEvent>, IRule
{
    private readonly ClimatisationRulesDiagnostics _diagnostics;

    private readonly string _ruleName;

    private readonly IEnumerable<string> _propertyNames;

    protected RuleBase(ClimatisationRulesDiagnostics diagnostics, string ruleName, IEnumerable<string> propertyNames)
    {
        _diagnostics = diagnostics;
        _ruleName = ruleName;
        _propertyNames = propertyNames;
    }

    public async Task Handle(PropertyChangedEvent request, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested
            || !IsSubscribedToProperty(request))
        {
            return;
        }

        var activity = _diagnostics.EvaluatingRule(_ruleName);

        if (await EvaluateCondition())
        {
            var activity2 = _diagnostics.ExecutingRule(_ruleName);

            await Execute();
        }
    }

    public abstract Task<bool> EvaluateCondition();

    public abstract Task<bool> Execute();

    private bool IsSubscribedToProperty(PropertyChangedEvent request)
    {
        return _propertyNames.Contains(request.Name, StringComparer.OrdinalIgnoreCase);
    }
}
