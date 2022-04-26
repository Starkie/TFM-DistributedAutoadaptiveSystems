namespace Climatisation.Rules.Service.EventHandlers.Rules;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Analysis.Contracts.Attributes;
using Analysis.Service.Contracts.IntegrationEvents;
using Climatisation.Rules.Service.Diagnostics;
using Core.Bus.Handlers;

[RuleKnowledgePropertyDependency]
public abstract class RuleBase
    : IIntegrationEventHandler<PropertyChangedIntegrationEvent>,
    IIntegrationEventHandler<ConfigurationChangedIntegrationEvent>
{
    private readonly ClimatisationRulesDiagnostics _diagnostics;

    private readonly string _ruleName;

    private readonly IEnumerable<string> _subscribedPropertyNames;

    private readonly IDictionary<string, IEnumerable<string>> _subscribedConfigurationNames;

    protected RuleBase(
        ClimatisationRulesDiagnostics diagnostics,
        string ruleName,
        IEnumerable<string> subscribedPropertyNames,
        IDictionary<string, IEnumerable<string>> subscribedConfigurationNames)
    {
        _diagnostics = diagnostics;
        _ruleName = ruleName;
        _subscribedPropertyNames = subscribedPropertyNames;
        _subscribedConfigurationNames = subscribedConfigurationNames;
    }

    public async Task Handle(PropertyChangedIntegrationEvent request)
    {
        if (!IsSubscribedToProperty(request))
        {
            return;
        }

        await Handle();
    }

    public async Task Handle(ConfigurationChangedIntegrationEvent message)
    {
        if (!IsSubscribedToConfiguration(message))
        {
            return;
        }

        await Handle();
    }

    private async Task Handle()
    {
        try
        {
            using var evaluatingRuleActivity = _diagnostics.EvaluatingRule(_ruleName);

            if (await EvaluateCondition())
            {
                using var executingRuleActivity = _diagnostics.ExecutingRule(_ruleName);

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
        return _subscribedPropertyNames.Contains(request.PropertyName, StringComparer.OrdinalIgnoreCase);
    }

    private bool IsSubscribedToConfiguration(ConfigurationChangedIntegrationEvent request)
    {
        bool subscribedToService = _subscribedConfigurationNames.TryGetValue(request.ServiceName, out IEnumerable<string> keys);

        return subscribedToService && keys.Contains(request.PropertyName, StringComparer.OrdinalIgnoreCase);
    }
}
