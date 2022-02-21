namespace Climatisation.Rules.Diagnostics;

using System;
using System.Diagnostics;
using AnalysisService.Contracts.IntegrationEvents;
using Microsoft.Extensions.Logging;

public class ClimatisationRulesDiagnostics
{
    private static readonly Action<ILogger, PropertyChangedIntegrationEvent, Exception> LogPropertyChangeEventReceived = LoggerMessage.Define<PropertyChangedIntegrationEvent>(
        LogLevel.Information,
        ClimatisationRulesEventIds.PropertyChangeEventReceivedEventId,
        "Property changed: {@PropertyChangeEvent}");

    private static readonly Action<ILogger, string, Exception> LogRuleEvaluation = LoggerMessage.Define<string>(
        LogLevel.Information,
        ClimatisationRulesEventIds.EvaluatingRuleEventId,
        "Evaluating rule: {ruleName}");

    private static readonly Action<ILogger, string, Exception> LogRuleExecution = LoggerMessage.Define<string>(
        LogLevel.Information,
        ClimatisationRulesEventIds.EvaluatingRuleEventId,
        "Executing rule: {ruleName}");

    private readonly ActivitySource _activitySource;

    private readonly ILogger _logger;

    public ClimatisationRulesDiagnostics(ILoggerProvider loggerProvider)
    {
        _logger = loggerProvider.CreateLogger(ClimatisationRulesConstants.AppName);

        _activitySource = new ActivitySource(ClimatisationRulesConstants.AppName);
    }

    public Activity PropertyChangeEventReceived(PropertyChangedIntegrationEvent propertyChangedIntegrationEvent)
    {
        LogPropertyChangeEventReceived(_logger, propertyChangedIntegrationEvent, null);

        return _activitySource.StartActivity("Property Changed Event Received");
    }

    public Activity EvaluatingRule(string ruleName)
    {
        LogRuleEvaluation(_logger, ruleName, null);

        return _activitySource.StartActivity($"Evaluating rule: {ruleName}");
    }

    public Activity ExecutingRule(string ruleName)
    {
        LogRuleExecution(_logger, ruleName, null);

        return _activitySource.StartActivity($"Executing rule: {ruleName}");
    }

    private static class ClimatisationRulesEventIds
    {
        public static EventId PropertyChangeEventReceivedEventId = new EventId(100, nameof(PropertyChangeEventReceivedEventId));

        public static EventId EvaluatingRuleEventId = new EventId(200, nameof(EvaluatingRuleEventId));
    }
}
