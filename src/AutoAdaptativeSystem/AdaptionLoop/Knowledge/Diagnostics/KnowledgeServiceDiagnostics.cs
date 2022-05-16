namespace Knowledge.Service.Diagnostics;

using System;
using System.Diagnostics;
using Knowledge.Service.DTOs;
using Knowledge.Service.DTOs.Configuration;
using Microsoft.Extensions.Logging;
using Prometheus;

public class KnowledgeServiceDiagnostics
{
    private static readonly Action<ILogger, string, Exception> LogGetPropertyMessage = LoggerMessage.Define<string>(
        LogLevel.Information,
        KnowledgeServiceEventIds.GetPropertyEventId,
        "Get property value request: {propertyName}");

    private static readonly Action<ILogger, string, Exception> LogPropertyNotFoundMessage = LoggerMessage.Define<string>(
        LogLevel.Information,
        KnowledgeServiceEventIds.PropertyNotFoundEventId,
        "Property '{PropertyName}' not found.");

    private static readonly Action<ILogger, string, PropertyDTO, Exception> LogPropertyFoundMessage = LoggerMessage.Define<string, PropertyDTO>(
        LogLevel.Information,
        KnowledgeServiceEventIds.PropertyFoundEventId,
        "Property '{PropertyName}' found. Value: '{@PropertyValue}'");

    private static readonly Action<ILogger, int, Exception> LogSetPropertiesMessage = LoggerMessage.Define<int>(
        LogLevel.Information,
        KnowledgeServiceEventIds.SetPropertyId,
        "Setting the value of '{count}' properties");

    private static readonly Action<ILogger, string, SetPropertyDTO, Exception> LogSetPropertyMessage = LoggerMessage.Define<string, SetPropertyDTO>(
        LogLevel.Information,
        KnowledgeServiceEventIds.SetPropertyId,
        "Set property '{propertyName}' with value '{@setPropertyValue} ");

    private static readonly Action<ILogger, string, Exception> LogDeletePropertyMessage = LoggerMessage.Define<string>(
        LogLevel.Information,
        KnowledgeServiceEventIds.DeletePropertyId,
        "Delete property request: {propertyName}");

    private static readonly Action<ILogger, string, Exception> LogGetConfigurationMessage = LoggerMessage.Define<string>(
        LogLevel.Information,
        KnowledgeServiceEventIds.GetConfigurationEventId,
        "Get configuration value request: {configurationName}");

    private static readonly Action<ILogger, string, ConfigurationDTO, Exception> LogConfigurationFoundMessage = LoggerMessage.Define<string, ConfigurationDTO>(
        LogLevel.Information,
        KnowledgeServiceEventIds.ConfigurationFoundEventId,
        "Configuration '{ConfigurationName}' found. Value: '{@PropertyValue}'");

    private static readonly Action<ILogger, string, Exception> LogConfigurationNotFoundMessage = LoggerMessage.Define<string>(
        LogLevel.Information,
        KnowledgeServiceEventIds.ConfigurationNotFoundEventId,
        "Configuration '{ConfigurationName}' not found.");

    private static readonly Action<ILogger, int, string, Exception> LogSetConfigurationKeysMessage = LoggerMessage.Define<int, string>(
        LogLevel.Information,
        KnowledgeServiceEventIds.SetPropertyId,
        "Setting the value of '{count}' configuration keys of the service '{serviceName}'");

    private static readonly Action<ILogger, string, string, SetPropertyDTO, Exception> LogSetConfigurationMessage = LoggerMessage.Define<string, string, SetPropertyDTO>(
        LogLevel.Information,
        KnowledgeServiceEventIds.SetConfigurationId,
        "Set service '{serviceName}' configuration key '{propertyName}' with value '{@setPropertyValue} ");

    private readonly ActivitySource _activitySource;

    private readonly ILogger _logger;

    private readonly Counter _getPropertyCounter;

    private readonly Counter _getPropertyNotFoundCounter;

    private readonly Counter _setPropertyCounter;

    private readonly Counter _getConfigurationCounter;

    private readonly Counter _getConfigurationNotFoundCounter;

    private readonly Counter _setConfigurationCounter;

    public KnowledgeServiceDiagnostics(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger(KnowledgeServiceConstants.AppName);

        _activitySource = new ActivitySource(KnowledgeServiceConstants.AppName);

        _getPropertyCounter = Metrics.CreateCounter("knowledge_service_property_get_requests_count", "The number of requests to get the value of a property.");
        _getPropertyNotFoundCounter = Metrics.CreateCounter("knowledge_service_property_get_not_found_count", "The number of requests to get the value of a property where its value was not found.");
        _setPropertyCounter = Metrics.CreateCounter("knowledge_service_property_set_requests_count", "The number of requests to update the value of a property.");

        _getConfigurationCounter = Metrics.CreateCounter("knowledge_service_configuration_get_requests_count", "The number of requests to get a configuration key of a service.");
        _getConfigurationNotFoundCounter = Metrics.CreateCounter("knowledge_service_configuration_get_not_found_count", "The number of requests to get a configuration key where its value was not found.");
        _setConfigurationCounter = Metrics.CreateCounter("knowledge_service_configuration_get_requests_count", "The number of requests to update the value of a property.");
    }

    public Activity LogGetProperty(string propertyName)
    {
        LogGetPropertyMessage(_logger, propertyName, null);

        _getPropertyCounter.Inc();

        return _activitySource.StartActivity("Get Property's Value");
    }

    public Activity LogSetProperties(int count)
    {
        LogSetPropertiesMessage(_logger, count, null);

        return _activitySource.StartActivity("Set Properties Values");
    }

    public void LogSetProperty(SetPropertyDTO setPropertyDto)
    {
        LogSetPropertyMessage(_logger, setPropertyDto.Name, setPropertyDto, null);

        _setPropertyCounter.Inc();
    }

    public Activity LogDeleteProperty(string propertyName)
    {
        LogDeletePropertyMessage(_logger, propertyName, null);

        return _activitySource.StartActivity("Delete Property");
    }

    public void LogPropertyNotFound(string propertyName)
    {
        LogPropertyNotFoundMessage(_logger, propertyName, null);

        _getPropertyNotFoundCounter.Inc();
    }

    public void LogPropertyFound(string propertyName, PropertyDTO value)
    {
        LogPropertyFoundMessage(_logger, propertyName, value, null);
    }

    public Activity LogGetConfiguration(string configurationName)
    {
        LogGetConfigurationMessage(_logger, configurationName, null);

        _getConfigurationCounter.Inc();

        return _activitySource.StartActivity("Get Configuration Value");
    }

    public void LogConfigurationFound(string configurationName, ConfigurationDTO value)
    {
        LogConfigurationFoundMessage(_logger, configurationName, value, null);
    }

    public void LogConfigurationNotFound(string configurationName)
    {
        LogConfigurationNotFoundMessage(_logger, configurationName, null);

        _getConfigurationNotFoundCounter.Inc();
    }

    public Activity LogSetConfigurationKeys(string serviceName, int count)
    {
        LogSetConfigurationKeysMessage(_logger, count, serviceName, null);

        return _activitySource.StartActivity("Set Configuration Key Value");
    }

    public void LogSetConfigurationKey(string serviceName, string propertyName, SetPropertyDTO setPropertyDto)
    {
        LogSetConfigurationMessage(_logger, serviceName, propertyName, setPropertyDto, null);

        _setConfigurationCounter.Inc();
    }

    private class KnowledgeServiceEventIds
    {
        public static EventId GetPropertyEventId = new EventId(200, nameof(GetPropertyEventId));

        public static EventId PropertyNotFoundEventId = new EventId(300, nameof(PropertyNotFoundEventId));

        public static EventId PropertyFoundEventId = new EventId(400, nameof(PropertyFoundEventId));

        public static EventId SetPropertyId = new EventId(500, nameof(SetPropertyId));

        public static EventId DeletePropertyId = new EventId(600, nameof(DeletePropertyId));

        public static EventId GetConfigurationEventId = new EventId(700, nameof(GetConfigurationEventId));

        public static EventId ConfigurationFoundEventId = new EventId(800, nameof(ConfigurationFoundEventId));

        public static EventId ConfigurationNotFoundEventId = new EventId(900, nameof(ConfigurationNotFoundEventId));

        public static EventId SetConfigurationId = new EventId(1000, nameof(SetConfigurationId));
    }
}
