namespace Climatisation.AirConditioner.Service.Diagnostics;

using System.Diagnostics;
using Climatisation.AirConditioner.Domain.Thermometers.ValueObjects;

public class ClimatisationAirConditionerServiceDiagnostics
{
    private static readonly Action<ILogger, string, TemperatureUnit, Exception> LogTemperatureReport = LoggerMessage.Define<string, TemperatureUnit>(
        LogLevel.Information,
        ClimatisationAirConditionerServiceEventIds.TemperatureReportEventId,
        "[Temperature] - Reporting {temperature}º {temperatureUnit}");

    private static readonly Action<ILogger, Exception> LogErrorReportingTemperature = LoggerMessage.Define(
        LogLevel.Error,
        ClimatisationAirConditionerServiceEventIds.ErrorReportingTemperatureEventId,
        "Error while reporting temperature");

    private readonly ActivitySource _activitySource;

    private readonly ILogger _logger;

    public ClimatisationAirConditionerServiceDiagnostics(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger(ClimatisationAirConditionerServiceConstants.AppName);

        _activitySource = new ActivitySource(ClimatisationAirConditionerServiceConstants.AppName);
    }

    public Activity ReportingTemperature(Temperature temperature)
    {
        string formatedTemperature = temperature.Value.ToString("F2");

        LogTemperatureReport(_logger, formatedTemperature, temperature.Unit, null);

        return _activitySource.StartActivity("Reporting temperature measurement");
    }

    public void ErrorReportingTemperature(Exception exception)
    {
        LogErrorReportingTemperature(_logger, exception);
    }

    private static class ClimatisationAirConditionerServiceEventIds
    {
        public static EventId TemperatureReportEventId = new EventId(100, nameof(TemperatureReportEventId));

        public static EventId ErrorReportingTemperatureEventId = new EventId(200, nameof(ErrorReportingTemperatureEventId));
    }
}
