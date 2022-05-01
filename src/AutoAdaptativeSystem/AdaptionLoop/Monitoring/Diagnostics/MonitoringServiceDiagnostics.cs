namespace Monitoring.Service.Diagnostics;

using System;
using System.Diagnostics;
using Knowledge.Service.ApiClient.Model;
using Microsoft.Extensions.Logging;
using Monitoring.Service.DTOS;

public class MonitoringServiceDiagnostics
{
    private static readonly Action<ILogger, string, Exception> LogGetPropertyMessage = LoggerMessage.Define<string>(
        LogLevel.Information,
        MonitoringServiceEventIds.GetPropertyEventId,
        "Get property value request: {propertyName}");

    private static readonly Action<ILogger, string, Exception> LogPropertyNotFoundMessage = LoggerMessage.Define<string>(
        LogLevel.Information,
        MonitoringServiceEventIds.PropertyNotFoundEventId,
        "Property '{PropertyName}' not found.");

    private static readonly Action<ILogger, string, PropertyDTO, Exception> LogPropertyFoundMessage = LoggerMessage.Define<string, PropertyDTO>(
        LogLevel.Information,
        MonitoringServiceEventIds.PropertyFoundEventId,
        "Property '{PropertyName}' found. Value: '{@PropertyValue}'");

    private static readonly Action<ILogger, string, MeasurementDTO, Exception> LogReportedMeasurementMessage = LoggerMessage.Define<string, MeasurementDTO>(
        LogLevel.Information,
        MonitoringServiceEventIds.ReportMeasurementEventId,
        "Reported Property Change: Name '{propertyName}' '{@measurement}'");

    private static readonly Action<ILogger, string, string, Exception> LogGetConfigurationMessage = LoggerMessage.Define<string, string>(
        LogLevel.Information,
        MonitoringServiceEventIds.GetConfigurationEventId,
        "Get service '{serviceName}' configuration value request: {configurationName}");

    private static readonly Action<ILogger, string, string, ConfigurationDTO, Exception> LogConfigurationFoundMessage = LoggerMessage.Define<string, string, ConfigurationDTO>(
        LogLevel.Information,
        MonitoringServiceEventIds.ConfigurationFoundEventId,
        "Service '{serviceName}' Configuration '{ConfigurationName}' found. Value: '{@PropertyValue}'");

    private static readonly Action<ILogger, string, string, Exception> LogConfigurationNotFoundMessage = LoggerMessage.Define<string, string>(
        LogLevel.Information,
        MonitoringServiceEventIds.ConfigurationNotFoundEventId,
        "Service '{serviceName}' Configuration '{ConfigurationName}' not found.");

    private readonly ActivitySource _activitySource;

    private readonly ILogger _logger;

    public MonitoringServiceDiagnostics(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger(MonitoringServiceConstants.AppName);

        _activitySource = new ActivitySource(MonitoringServiceConstants.AppName);
    }

    public Activity LogGetProperty(string propertyName)
    {
        LogGetPropertyMessage(_logger, propertyName, null);

        return _activitySource.StartActivity("Get Property's Value");
    }

    public Activity LogReportedMeasurement(string propertyName, MeasurementDTO measurementDto)
    {
        LogReportedMeasurementMessage(_logger, propertyName, measurementDto, null);

        return _activitySource.StartActivity("Reported Measurement");
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

    internal class MonitoringServiceEventIds
    {
        public static EventId GetPropertyEventId = new EventId(200, nameof(GetPropertyEventId));

        public static EventId PropertyNotFoundEventId = new EventId(300, nameof(PropertyNotFoundEventId));

        public static EventId PropertyFoundEventId = new EventId(400, nameof(PropertyFoundEventId));

        public static EventId ReportMeasurementEventId = new EventId(500, nameof(ReportMeasurementEventId));

        public static EventId GetConfigurationEventId = new EventId(500, nameof(GetConfigurationEventId));

        public static EventId ConfigurationFoundEventId = new EventId(600, nameof(ConfigurationFoundEventId));

        public static EventId ConfigurationNotFoundEventId = new EventId(700, nameof(ConfigurationNotFoundEventId));
    }
}
