namespace Knowledge.Service.Diagnostics;

using System;
using System.Diagnostics;
using Knowledge.Service.DTOs;
using Knowledge.Service.DTOs.Configuration;
using Microsoft.Extensions.Logging;

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

    private readonly ActivitySource _activitySource;

    private readonly ILogger _logger;

    public KnowledgeServiceDiagnostics(ILoggerProvider loggerProvider)
    {
        _logger = loggerProvider.CreateLogger(KnowledgeServiceConstants.AppName);

        _activitySource = new ActivitySource(KnowledgeServiceConstants.AppName);
    }

    public Activity LogGetProperty(string propertyName)
    {
        LogGetPropertyMessage(_logger, propertyName, null);

        return _activitySource.StartActivity("Get Property's Value");
    }

    public Activity LogSetProperty(string propertyName, SetPropertyDTO setPropertyDto)
    {
        LogSetPropertyMessage(_logger, propertyName, setPropertyDto, null);

        return _activitySource.StartActivity("Set Property's Value");
    }

    public Activity LogDeleteProperty(string propertyName)
    {
        LogDeletePropertyMessage(_logger, propertyName, null);

        return _activitySource.StartActivity("Delete Property");
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

    internal class KnowledgeServiceEventIds
    {
        public static EventId GetPropertyEventId = new EventId(200, nameof(GetPropertyEventId));

        public static EventId PropertyNotFoundEventId = new EventId(300, nameof(PropertyNotFoundEventId));

        public static EventId PropertyFoundEventId = new EventId(400, nameof(PropertyFoundEventId));

        public static EventId SetPropertyId = new EventId(500, nameof(SetPropertyId));

        public static EventId DeletePropertyId = new EventId(600, nameof(DeletePropertyId));

        public static EventId GetConfigurationEventId = new EventId(700, nameof(GetConfigurationEventId));

        public static EventId ConfigurationFoundEventId = new EventId(800, nameof(ConfigurationFoundEventId));

        public static EventId ConfigurationNotFoundEventId = new EventId(900, nameof(ConfigurationNotFoundEventId));
    }
}
