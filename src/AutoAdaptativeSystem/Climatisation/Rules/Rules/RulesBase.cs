namespace Climatisation.Rules.Rules;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Climatisation.Rules.Diagnostics;
using Climatisation.Rules.Events;
using MediatR;

public abstract class RuleBase : INotificationHandler<PropertyChangedEvent>
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

        try
        {
            var activity = _diagnostics.EvaluatingRule(_ruleName);

            if (await EvaluateCondition(cancellationToken))
            {
                var activity2 = _diagnostics.ExecutingRule(_ruleName);

                await Execute(cancellationToken);
            }
        }
        catch (Exception e)
        {
            _diagnostics.RuleEvaluationError(_ruleName, e);

            throw;
        }
    }

    protected abstract Task<bool> EvaluateCondition(CancellationToken cancellationToken);

    protected abstract Task Execute(CancellationToken cancellationToken);

    private bool IsSubscribedToProperty(PropertyChangedEvent request)
    {
        return _propertyNames.Contains(request.Name, StringComparer.OrdinalIgnoreCase);
    }
}
