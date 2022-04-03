namespace Analysis.Service.Diagnostics;

using System;
using System.Diagnostics;
using Knowledge.Contracts.IntegrationEvents;
using Knowledge.Service.ApiClient.Model;
using Microsoft.Extensions.Logging;

public class AnalysisServiceDiagnostics
{
    private static readonly Action<ILogger, string, Exception> LogGetPropertyMessage = LoggerMessage.Define<string>(
        LogLevel.Information,
        AnalysisServiceEventIds.GetPropertyEventId,
        "Get property value request: {propertyName}");

    private static readonly Action<ILogger, string, Exception> LogPropertyNotFoundMessage = LoggerMessage.Define<string>(
        LogLevel.Information,
        AnalysisServiceEventIds.PropertyNotFoundEventId,
        "Property '{PropertyName}' not found.");

    private static readonly Action<ILogger, string, PropertyDTO, Exception> LogPropertyFoundMessage = LoggerMessage.Define<string, PropertyDTO>(
        LogLevel.Information,
        AnalysisServiceEventIds.PropertyFoundEventId,
        "Property '{PropertyName}' found. Value: '{@PropertyValue}'");

    private static readonly Action<ILogger, PropertyChangedIntegrationEvent, Exception> LogPropertyChangeEventReceived = LoggerMessage.Define<PropertyChangedIntegrationEvent>(
        LogLevel.Information,
        AnalysisServiceEventIds.PropertyChangeEventReceivedEventId,
        "Property changed: {@PropertyChangeEvent}");

    private static readonly Action<ILogger, string, Exception> LogGetConfigurationMessage = LoggerMessage.Define<string>(
        LogLevel.Information,
        AnalysisServiceEventIds.GetConfigurationEventId,
        "Get configuration value request: {configurationName}");

    private static readonly Action<ILogger, string, ConfigurationDTO, Exception> LogConfigurationFoundMessage = LoggerMessage.Define<string, ConfigurationDTO>(
        LogLevel.Information,
        AnalysisServiceEventIds.ConfigurationFoundEventId,
        "Configuration '{ConfigurationName}' found. Value: '{@PropertyValue}'");

    private static readonly Action<ILogger, string, Exception> LogConfigurationNotFoundMessage = LoggerMessage.Define<string>(
        LogLevel.Information,
        AnalysisServiceEventIds.ConfigurationNotFoundEventId,
        "Configuration '{ConfigurationName}' not found.");

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

    public Activity LogGetProperty(string propertyName)
    {
        LogGetPropertyMessage(_logger, propertyName, null);

        return _activitySource.StartActivity("Get Property's Value");
    }

    public void LogPropertyNotFound(string propertyName)
    {
        LogPropertyNotFoundMessage(_logger, propertyName, null);
    }

    public void LogPropertyFound(string propertyName, PropertyDTO value)
    {
        LogPropertyFoundMessage(_logger, propertyName, value, null);
    }

    public Activity LogGetConfiguration(string configurationName)
    {
        LogGetConfigurationMessage(_logger, configurationName, null);

        return _activitySource.StartActivity("Get Configuration Value");
    }

    public void LogConfigurationFound(string configurationName, ConfigurationDTO value)
    {
        LogConfigurationFoundMessage(_logger, configurationName, value, null);
    }

    public void LogConfigurationNotFound(string configurationName)
    {
        LogConfigurationNotFoundMessage(_logger, configurationName, null);
    }

    private static class AnalysisServiceEventIds
    {
        public static EventId PropertyChangeEventReceivedEventId = new EventId(100, nameof(PropertyChangeEventReceivedEventId));

        public static EventId GetPropertyEventId = new EventId(200, nameof(GetPropertyEventId));

        public static EventId PropertyNotFoundEventId = new EventId(300, nameof(PropertyNotFoundEventId));

        public static EventId PropertyFoundEventId = new EventId(400, nameof(PropertyFoundEventId));

        public static EventId GetConfigurationEventId = new EventId(500, nameof(GetConfigurationEventId));

        public static EventId ConfigurationFoundEventId = new EventId(600, nameof(ConfigurationFoundEventId));

        public static EventId ConfigurationNotFoundEventId = new EventId(700, nameof(ConfigurationNotFoundEventId));
    }
}
