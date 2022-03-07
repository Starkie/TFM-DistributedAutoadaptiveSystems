namespace Climatisation.Monitor;

using System;
using System.Diagnostics;
using Climatisation.Contacts;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

public class ClimatisationMonitorDiagnostics
{

    private static readonly Action<ILogger, Exception> GetPreviousMeasurement = LoggerMessage.Define(
        LogLevel.Information,
        RoomMonitorEventIds.GetPreviousMeasurementEventId,
        "Reading previous measurement");

    private static readonly Action<ILogger, TemperatureMeasurementDTO, Exception> LogNewTemperatureMeasurement = LoggerMessage.Define<TemperatureMeasurementDTO>(
        LogLevel.Information,
        RoomMonitorEventIds.RegisterTemperatureMeasurementEventId,
        "Received Temperature Measurement: {@Measurement}");

    private static readonly Action<ILogger, TemperatureMeasurementDTO, Exception> LogProbeReadingNotWithinSafeMargins = LoggerMessage.Define<TemperatureMeasurementDTO>(
        LogLevel.Warning,
        RoomMonitorEventIds.ProbeReadingNotWithinSafeMarginsId,
        "Probe reading not within safe margins: {@Measurement}");

    private readonly ActivitySource _activitySource;

    private readonly ILogger _logger;

    public ClimatisationMonitorDiagnostics(ILoggerProvider loggerProvider)
    {
        _logger = loggerProvider.CreateLogger(ClimatisationMonitorConstants.AppName);

        _activitySource = new ActivitySource(ClimatisationMonitorConstants.AppName);
    }

    public Activity LogGetPreviousMeasurement()
    {
        GetPreviousMeasurement(_logger, null);

        return _activitySource.StartActivity("Reading previous Temperature measurement");
    }

    public void ProbeReadingNotWithinSafeMargins(TemperatureMeasurementDTO temperatureMeasurementDTO)
    {
        LogProbeReadingNotWithinSafeMargins(_logger, temperatureMeasurementDTO, null);
    }

    public Activity RegisterTemperatureMeasurement(TemperatureMeasurementDTO measurementDto)
    {
        LogNewTemperatureMeasurement(_logger, measurementDto, null);

        return _activitySource.StartActivity("Register Temperature Measurement");
    }

    private static class RoomMonitorEventIds
    {
        public static EventId GetPreviousMeasurementEventId = new EventId(200, nameof(GetPreviousMeasurementEventId));

        public static EventId ProbeReadingNotWithinSafeMarginsId = new EventId(500, nameof(ProbeReadingNotWithinSafeMarginsId));

        public static EventId RegisterTemperatureMeasurementEventId = new EventId(100, nameof(RegisterTemperatureMeasurementEventId));
    }
}