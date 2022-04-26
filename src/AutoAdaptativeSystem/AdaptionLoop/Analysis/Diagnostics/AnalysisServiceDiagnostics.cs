namespace Analysis.Service.Diagnostics;

using System;
using System.Diagnostics;
using Analysis.Service.DTOs.Configuration;
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

    private static readonly Action<ILogger, string, string, Exception> LogGetConfigurationMessage = LoggerMessage.Define<string, string>(
        LogLevel.Information,
        AnalysisServiceEventIds.GetConfigurationEventId,
        "Get service '{serviceName}' configuration value request: {configurationName}");

    private static readonly Action<ILogger, string, string, ConfigurationDTO, Exception> LogConfigurationFoundMessage = LoggerMessage.Define<string, string, ConfigurationDTO>(
        LogLevel.Information,
        AnalysisServiceEventIds.ConfigurationFoundEventId,
        "Service '{serviceName}' Configuration '{ConfigurationName}' found. Value: '{@PropertyValue}'");

    private static readonly Action<ILogger, string, string, Exception> LogConfigurationNotFoundMessage = LoggerMessage.Define<string, string>(
        LogLevel.Information,
        AnalysisServiceEventIds.ConfigurationNotFoundEventId,
        "Service '{serviceName}' Configuration '{ConfigurationName}' not found.");

    private static readonly Action<ILogger, SystemConfigurationChangeRequestDTO, Exception> LogConfigurationChangeRequestEventReceived =
        LoggerMessage.Define<SystemConfigurationChangeRequestDTO>(
            LogLevel.Information,
            AnalysisServiceEventIds.SystemConfigurationChangeRequestedEventId,
            "Requested system configuration change '@{configurationChangeRequest}'.");

    private static readonly Action<ILogger, string, string, Exception> LogConfigurationChangedEventReceived =
        LoggerMessage.Define<string, string>(
            LogLevel.Information,
            AnalysisServiceEventIds.ConfigurationChangedEventId,
            "Configuration changed: {serviceName}.{configurationName}'");

    private readonly ActivitySource _activitySource;

    private readonly ILogger _logger;

    public AnalysisServiceDiagnostics(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger(AnalysisServiceConstants.AppName);

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

    public Activity LogGetServiceConfiguration(string serviceName, string configurationName)
    {
        LogGetConfigurationMessage(_logger, serviceName, configurationName, null);

        return _activitySource.StartActivity("Get Service Configuration Value");
    }

    public void LogConfigurationFound(string serviceName, string configurationName, ConfigurationDTO value)
    {
        LogConfigurationFoundMessage(_logger, serviceName, configurationName, value, null);
    }

    public void LogConfigurationNotFound(string serviceName, string configurationName)
    {
        LogConfigurationNotFoundMessage(_logger, serviceName, configurationName, null);
    }

    public Activity LogConfigurationChangeRequested(SystemConfigurationChangeRequestDTO configurationChangeRequestDto)
    {
        LogConfigurationChangeRequestEventReceived(_logger, configurationChangeRequestDto, null);

        return _activitySource.StartActivity("Configuration change requested");
    }

    public Activity ConfigurationChangedEventReceived(ConfigurationChangedIntegrationEvent configurationCHangedEvent)
    {
        LogConfigurationChangedEventReceived(_logger, configurationCHangedEvent.ServiceName, configurationCHangedEvent.ConfigurationName, null);

        return _activitySource.StartActivity("Configuration Changed Event Received");
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

        public static EventId SystemConfigurationChangeRequestedEventId = new EventId(800, nameof(SystemConfigurationChangeRequestedEventId));

        public static EventId ConfigurationChangedEventId = new EventId(900, nameof(ConfigurationChangedEventId));
    }
}
