namespace Planning.Service.Diagnostics;

using System;
using System.Diagnostics;
using Analysis.Contracts.IntegrationEvents;
using Microsoft.Extensions.Logging;
using Planning.Contracts.IntegrationEvents;

public class PlanningServiceDiagnostics
{
    private static readonly Action<ILogger, SystemConfigurationChangeRequest, Exception> LogSystemChangeRequestReceivedMessage =
        LoggerMessage.Define<SystemConfigurationChangeRequest>(
            LogLevel.Information,
            PlanningServiceEventIds.SystemConfigurationChangeRequestEventId,
            "System change request received: {@SystemChangeRequest}");

    private static readonly Action<ILogger, Exception> LogDefiningChangePlanMessage =
        LoggerMessage.Define(
            LogLevel.Information,
            PlanningServiceEventIds.DefiningChangePlanEventId,
            "Defining change plan");

    private static readonly Action<ILogger, string, Exception> LogChangePlanCreatedMessage =
        LoggerMessage.Define<string>(
            LogLevel.Information,
            PlanningServiceEventIds.ChangePlanCreatedEventId,
            @"Configuration Plan Created:
-----------------------
{changePlan}");

    private static readonly Action<ILogger, Exception> LogChangePlanDiscardedMessage =
        LoggerMessage.Define(
            LogLevel.Information,
            PlanningServiceEventIds.ChangePlanDiscardedEventId,
            "Change plan discarded. No changes were required!");

    private readonly ActivitySource _activitySource;

    private readonly ILogger _logger;

    public PlanningServiceDiagnostics(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger(PlanningServiceConstants.AppName);

        _activitySource = new ActivitySource(PlanningServiceConstants.AppName);
    }

    public void SystemConfigurationChangeRequestReceived(SystemConfigurationChangeRequest message)
    {
        LogSystemChangeRequestReceivedMessage(_logger, message, null);
    }

    public Activity DefininingChangePlan()
    {
        LogDefiningChangePlanMessage(_logger, null);

        return _activitySource.StartActivity("Defining change plan");
    }

    public void ConfigurationChangePlanCreated(ConfigurationChangePlan configurationChangePlan)
    {
        LogChangePlanCreatedMessage(_logger, configurationChangePlan.ToString(), null);
    }

    public void ChangePlanDiscarded()
    {
        LogChangePlanDiscardedMessage(_logger, null);
    }

    private static class PlanningServiceEventIds
    {
        public static EventId SystemConfigurationChangeRequestEventId = new EventId(100, nameof(SystemConfigurationChangeRequestEventId));

        public static EventId DefiningChangePlanEventId = new EventId(200, nameof(DefiningChangePlanEventId));

        public static EventId ChangePlanCreatedEventId = new EventId(300, nameof(ChangePlanCreatedEventId));

        public static EventId ChangePlanDiscardedEventId = new EventId(400, nameof(ChangePlanDiscardedEventId));
    }
}
