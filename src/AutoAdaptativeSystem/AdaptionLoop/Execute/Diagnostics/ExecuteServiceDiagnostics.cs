namespace Execute.Service.Diagnostics;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Planning.Contracts.IntegrationEvents.AdaptionActions;

public class ExecuteServiceDiagnostics
{
    private readonly ActivitySource _activitySource;

    private readonly ILogger _logger;

    private static readonly Action<ILogger, Exception> LogExecuteChangePlan = LoggerMessage.Define(
        LogLevel.Information,
        ExecuteServiceEventIds.ExecuteChangePlanEventId,
        "Executing change plan");

    private static readonly Action<ILogger, string, IEnumerable<AdaptionAction>, Exception> LogExecuteServiceChangePlan =
        LoggerMessage.Define<string, IEnumerable<AdaptionAction>>(
            LogLevel.Information,
            ExecuteServiceEventIds.ExecuteExecuteServiceActionsEventId,
            "Executing service '{serviceName}' change plan: {@actions}");

    public ExecuteServiceDiagnostics(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger(ExecuteServiceConstants.AppName);

        _activitySource = new ActivitySource(ExecuteServiceConstants.AppName);
    }

    public Activity StartExecuteChangePlan()
    {
        LogExecuteChangePlan(_logger, null);

        return _activitySource.StartActivity("Execute change plan");
    }

    public void ExecuteServiceActions(string serviceName, IEnumerable<AdaptionAction> actions)
    {
        LogExecuteServiceChangePlan(_logger, serviceName, actions, null);
    }

    private static class ExecuteServiceEventIds
    {
        internal static EventId ExecuteChangePlanEventId = new EventId(100, nameof(ExecuteChangePlanEventId));

        internal static EventId ExecuteExecuteServiceActionsEventId = new EventId(200, nameof(ExecuteExecuteServiceActionsEventId));
    }
}
