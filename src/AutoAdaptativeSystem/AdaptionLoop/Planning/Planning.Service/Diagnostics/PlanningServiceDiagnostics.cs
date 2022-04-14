namespace Planning.Service.Diagnostics;

using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Planning.Contracts.IntegrationEvents;

public class PlanningServiceDiagnostics
{
    private static readonly Action<ILogger, SystemChangeRequestIntegrationEvent, Exception> LogSystemChangeRequestReceivedMessage =
        LoggerMessage.Define<SystemChangeRequestIntegrationEvent>(
            LogLevel.Information,
            PlanningServiceEventIds.SystemChangeRequestEventId,
            "System change request received: @{systemChangeRequest}");

    private readonly ActivitySource _activitySource;

    private readonly ILogger _logger;

    public PlanningServiceDiagnostics(ILoggerProvider loggerProvider)
    {
        _logger = loggerProvider.CreateLogger(PlanningServiceConstants.AppName);

        _activitySource = new ActivitySource(PlanningServiceConstants.AppName);
    }

    public Activity SystemChangeRequestReceived(SystemChangeRequestIntegrationEvent message)
    {
        LogSystemChangeRequestReceivedMessage(_logger, message, null);

        return _activitySource.StartActivity("System change request received");
    }

    private static class PlanningServiceEventIds
    {
        public static EventId SystemChangeRequestEventId = new EventId(100, nameof(SystemChangeRequestEventId));
    }
}
