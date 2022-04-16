namespace Climatisation.Rules.Diagnostics;

using System;
using System.Diagnostics;
using Analysis.Service.Contracts.IntegrationEvents;
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
        ClimatisationRulesEventIds.ExecutingRuleEventId,
        "Executing rule: {ruleName}");

    private static readonly Action<ILogger, string, Exception> LogGetPropertyValue = LoggerMessage.Define<string>(
        LogLevel.Information,
        ClimatisationRulesEventIds.GetPropertyValueEventId,
        "Requesting property '{propertyName}' value");

    private static readonly Action<ILogger, string, Exception> LogRuleEvaluationError = LoggerMessage.Define<string>(
        LogLevel.Error,
        ClimatisationRulesEventIds.ErrorEvaluatingRuleEventId,
        "Error evaluating rule '{ruleName}' value");

    private readonly ActivitySource _activitySource;

    private readonly ILogger _logger;

    public ClimatisationRulesDiagnostics(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger(ClimatisationRulesConstants.AppName);

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

    public void RuleEvaluationError(string ruleName, Exception exception)
    {
        LogRuleEvaluationError(_logger, ruleName, exception);
    }

    public Activity GetPropertyValue(string propertyName)
    {
        LogGetPropertyValue(_logger, propertyName, null);

        return _activitySource.StartActivity($"Get property: {propertyName}");
    }

    private static class ClimatisationRulesEventIds
    {
        public static EventId PropertyChangeEventReceivedEventId = new EventId(100, nameof(PropertyChangeEventReceivedEventId));

        public static EventId EvaluatingRuleEventId = new EventId(200, nameof(EvaluatingRuleEventId));

        public static EventId ErrorEvaluatingRuleEventId = new EventId(201, nameof(ErrorEvaluatingRuleEventId));

        public static EventId ExecutingRuleEventId = new EventId(300, nameof(ExecutingRuleEventId));

        public static EventId GetPropertyValueEventId = new EventId(400, nameof(GetPropertyValueEventId));
    }
}
