namespace AnalysisService.Diagnostics;

using System;
using System.Diagnostics;
using Knowledge.Contracts.Controllers.IntegrationEvents;
using Microsoft.Extensions.Logging;

public class AnalysisServiceDiagnostics
{
    private static readonly Action<ILogger, PropertyChangedIntegrationEvent, Exception> LogPropertyChangeEventReceived = LoggerMessage.Define<PropertyChangedIntegrationEvent>(
        LogLevel.Information,
        AnalysisServiceEventIds.PropertyChangeEventReceivedEventId,
        "Property changed: {@PropertyChangeEvent}");

    private readonly ActivitySource _activitySource;

    private readonly ILogger _logger;

    public AnalysisServiceDiagnostics(ILoggerProvider loggerProvider)
    {
        _logger = loggerProvider.CreateLogger(AnalysisServiceConstants.AppName);

        _activitySource = new ActivitySource(AnalysisServiceConstants.AppName);
    }

    public Activity PropertyChangeEventReceived(PropertyChangedIntegrationEvent propertyChangedIntegrationEvent)
    {
        LogPropertyChangeEventReceived(_logger, propertyChangedIntegrationEvent, null);

        return _activitySource.StartActivity("Property Changed Event Received");
    }

    private static class AnalysisServiceEventIds
    {
        public static EventId PropertyChangeEventReceivedEventId = new EventId(100, nameof(PropertyChangeEventReceivedEventId));
    }
}
