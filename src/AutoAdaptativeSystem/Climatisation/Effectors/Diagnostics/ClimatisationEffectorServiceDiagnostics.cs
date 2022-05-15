namespace Climatisation.Effectors.Service.Diagnostics;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Execute.Contracts.IntegrationEvents;
using Microsoft.Extensions.Logging;
using Planning.Contracts.IntegrationEvents.AdaptionActions;
using Prometheus;

public class ClimatisationEffectorServiceDiagnostics
{
    private readonly ActivitySource _activitySource;

    private readonly ILogger _logger;

    private static readonly Action<ILogger, Exception> LogExecuteChangePlan = LoggerMessage.Define(
        LogLevel.Information,
        ExecuteServiceEventIds.ExecuteChangePlanEventId,
        "Executing change plan");

    private static readonly Action<ILogger, AdaptionAction, IEnumerable<Symptom>, Exception> LogExecuteAdaptionAction =
        LoggerMessage.Define<AdaptionAction, IEnumerable<Symptom>>(
            LogLevel.Information,
            ExecuteServiceEventIds.ExecuteAdaptionActionEventId,
            "Executing adaption action {@adaptionAction} for symptoms {@symptoms}");

    private readonly Counter _executedAdaptionActionsCounter;

    public ClimatisationEffectorServiceDiagnostics(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger(ClimatisationEffectorConstants.AppName);

        _activitySource = new ActivitySource(ClimatisationEffectorConstants.AppName);

        _executedAdaptionActionsCounter = Metrics.CreateCounter("climatisation_effectors_service_adaptionactions_executed_count", "The number of adaption actions executed by this service.");
    }

    public Activity StartExecuteChangePlan()
    {
        LogExecuteChangePlan(_logger, null);

        return _activitySource.StartActivity("Execute change plan");
    }

    // TODO: Mover AdaptionAction a ExecuteContracts.
    public void ExecuteAdaptionAction(AdaptionAction action, IEnumerable<Symptom> symptoms)
    {
        _executedAdaptionActionsCounter.Inc();

        LogExecuteAdaptionAction(_logger, action, symptoms, null);
    }

    private static class ExecuteServiceEventIds
    {
        internal static EventId ExecuteChangePlanEventId = new EventId(100, nameof(ExecuteChangePlanEventId));

        internal static EventId ExecuteAdaptionActionEventId = new EventId(200, nameof(ExecuteAdaptionActionEventId));
    }
}
