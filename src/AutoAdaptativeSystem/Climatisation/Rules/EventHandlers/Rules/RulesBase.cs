namespace Climatisation.Rules.EventHandlers.Rules;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Analysis.Contracts.Attributes;
using Analysis.Service.Contracts.IntegrationEvents;
using Climatisation.Rules.Diagnostics;
using Climatisation.Rules.Events;
using Core.Bus.Handlers;
using MediatR;

[RuleKnowledgePropertyDependency]
public abstract class RuleBase : IntegrationEventHandler<PropertyChangedIntegrationEvent>
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

    public override async Task Handle(PropertyChangedIntegrationEvent request)
    {
        if (!IsSubscribedToProperty(request))
        {
            return;
        }

        try
        {
            using var activity = _diagnostics.EvaluatingRule(_ruleName);

            if (await EvaluateCondition())
            {
                using var activity2 = _diagnostics.ExecutingRule(_ruleName);

                await Execute();
            }
        }
        catch (Exception e)
        {
            _diagnostics.RuleEvaluationError(_ruleName, e);

            throw;
        }
    }

    protected abstract Task<bool> EvaluateCondition();

    protected abstract Task Execute();

    private bool IsSubscribedToProperty(PropertyChangedIntegrationEvent request)
    {
        return _propertyNames.Contains(request.PropertyName, StringComparer.OrdinalIgnoreCase);
    }
}
