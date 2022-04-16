namespace Planning.Service.Diagnostics;

using System;
using System.Diagnostics;
using Analysis.Contracts.IntegrationEvents;
using Microsoft.Extensions.Logging;

public class PlanningServiceDiagnostics
{
    private static readonly Action<ILogger, SystemConfigurationChangeRequestIntegrationEvent, Exception> LogSystemChangeRequestReceivedMessage =
        LoggerMessage.Define<SystemConfigurationChangeRequestIntegrationEvent>(
            LogLevel.Information,
            PlanningServiceEventIds.SystemConfigurationChangeRequestEventId,
            "System change request received: @{systemChangeRequest}");

    private readonly ActivitySource _activitySource;

    private readonly ILogger _logger;

    public PlanningServiceDiagnostics(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger(PlanningServiceConstants.AppName);

        _activitySource = new ActivitySource(PlanningServiceConstants.AppName);
    }

    public Activity SystemConfigurationChangeRequestReceived(SystemConfigurationChangeRequestIntegrationEvent message)
    {
        LogSystemChangeRequestReceivedMessage(_logger, message, null);

        return _activitySource.StartActivity("System configuration change request received");
    }

    private static class PlanningServiceEventIds
    {
        public static EventId SystemConfigurationChangeRequestEventId = new EventId(100, nameof(SystemConfigurationChangeRequestEventId));
    }
}
