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

    private static readonly Action<ILogger, string, MeasurementDTO, Exception> LogReportedMeasurementMessage = LoggerMessage.Define<string, MeasurementDTO>(
        LogLevel.Information,
        KnowledgeServiceEventIds.ReportMeasurementEventId,
        "Reported Property Change: Name '{propertyName}' '{@measurement}'");

    private readonly ActivitySource _activitySource;

    private readonly ILogger _logger;

    public MonitoringServiceDiagnostics(ILoggerProvider loggerProvider)
    {
        _logger = loggerProvider.CreateLogger(MonitoringServiceConstants.AppName);

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

    internal class KnowledgeServiceEventIds
    {
        public static EventId GetPropertyEventId = new EventId(200, nameof(GetPropertyEventId));

        public static EventId PropertyNotFoundEventId = new EventId(300, nameof(PropertyNotFoundEventId));

        public static EventId PropertyFoundEventId = new EventId(400, nameof(PropertyFoundEventId));

        public static EventId ReportMeasurementEventId = new EventId(500, nameof(ReportMeasurementEventId));
    }
}
