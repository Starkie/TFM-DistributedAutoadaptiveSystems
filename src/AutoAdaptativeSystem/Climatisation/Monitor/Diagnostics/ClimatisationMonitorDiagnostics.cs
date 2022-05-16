namespace Climatisation.Monitor.Service.Diagnostics;

using System;
using System.Diagnostics;
using Climatisation.Contracts;
using Microsoft.Extensions.Logging;
using Monitoring.Service.ApiClient.Model;

public class ClimatisationMonitorDiagnostics
{
    private static readonly Action<ILogger, Exception> GetPreviousMeasurement = LoggerMessage.Define(
        LogLevel.Information,
        ClimatisationMonitorEventIds.GetPreviousMeasurementEventId,
        "Reading previous measurement");

    private static readonly Action<ILogger, TemperatureMeasurementDTO, Exception> LogNewTemperatureMeasurement = LoggerMessage.Define<TemperatureMeasurementDTO>(
        LogLevel.Information,
        ClimatisationMonitorEventIds.RegisterTemperatureMeasurementEventId,
        "Received Temperature Measurement: {@Measurement}");

    private static readonly Action<ILogger, TemperatureMeasurementDTO, Exception> LogProbeReadingNotWithinSafeMargins = LoggerMessage.Define<TemperatureMeasurementDTO>(
        LogLevel.Warning,
        ClimatisationMonitorEventIds.ProbeReadingNotWithinSafeMarginsId,
        "Probe reading not within safe margins: {@Measurement}");

    private static readonly Action<ILogger, string, string, Exception> LogGetConfigurationMessage = LoggerMessage.Define<string, string>(
        LogLevel.Information,
        ClimatisationMonitorEventIds.GetConfigurationEventId,
        "Get service '{serviceName}' configuration value request: {configurationName}");

    private static readonly Action<ILogger, string, string, ConfigurationDTO, Exception> LogConfigurationFoundMessage = LoggerMessage.Define<string, string, ConfigurationDTO>(
        LogLevel.Information,
        ClimatisationMonitorEventIds.ConfigurationFoundEventId,
        "Service '{serviceName}' Configuration '{ConfigurationName}' found. Value: '{@PropertyValue}'");

    private static readonly Action<ILogger, string, string, Exception> LogConfigurationNotFoundMessage = LoggerMessage.Define<string, string>(
        LogLevel.Information,
        ClimatisationMonitorEventIds.ConfigurationNotFoundEventId,
        "Service '{serviceName}' Configuration '{ConfigurationName}' not found.");

    private static readonly Action<ILogger, string, string, SetPropertyDTO, Exception> LogSetServiceConfigurationMessage = LoggerMessage.Define<string, string, SetPropertyDTO>(
        LogLevel.Information,
        ClimatisationMonitorEventIds.SetServiceConfigurationEventId,
        "Set Service '{serviceName}' Configuration '{ConfigurationName}' Value {@value}.");

    private readonly ActivitySource _activitySource;

    private readonly ILogger _logger;

    public ClimatisationMonitorDiagnostics(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger(ClimatisationMonitorConstants.AppName);

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

    public Activity LogSetServiceConfiguration(string serviceName, string configurationName, SetPropertyDTO setPropertyDto)
    {
        LogSetServiceConfigurationMessage(_logger, serviceName, configurationName, setPropertyDto, null);

        return _activitySource.StartActivity("Set Service Configuration");
    }

    private static class ClimatisationMonitorEventIds
    {
        public static EventId GetPreviousMeasurementEventId = new EventId(100, nameof(GetPreviousMeasurementEventId));

        public static EventId ProbeReadingNotWithinSafeMarginsId = new EventId(200, nameof(ProbeReadingNotWithinSafeMarginsId));

        public static EventId RegisterTemperatureMeasurementEventId = new EventId(300, nameof(RegisterTemperatureMeasurementEventId));

        public static EventId GetConfigurationEventId = new EventId(400, nameof(GetConfigurationEventId));

        public static EventId ConfigurationFoundEventId = new EventId(500, nameof(ConfigurationFoundEventId));

        public static EventId ConfigurationNotFoundEventId = new EventId(600, nameof(ConfigurationNotFoundEventId));

        public static EventId SetServiceConfigurationEventId = new EventId(700, nameof(ConfigurationNotFoundEventId));
    }
}
