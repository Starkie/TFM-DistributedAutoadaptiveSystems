namespace Climatisation.AirConditioner.Service.Diagnostics;

using System.Diagnostics;
using Climatisation.AirConditioner.Contracts;
using Climatisation.AirConditioner.Domain.Thermometers.ValueObjects;
using Prometheus;

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

    private static readonly Action<ILogger, Exception> LogSeedAdaptionLoopConfiguration = LoggerMessage.Define(
        LogLevel.Information,
        ClimatisationAirConditionerServiceEventIds.SeedAdaptionLoopConfiguration,
        "Seeding Air Conditioner AdaptionLoop Configuration");

    private static readonly Action<ILogger, Exception> LogStartHeatingRoom = LoggerMessage.Define(
        LogLevel.Information,
        ClimatisationAirConditionerServiceEventIds.HeatRoomEventId,
        "AirConditionerMode: [HEAT]");

    private static readonly Action<ILogger, Exception> LogStartCoolingRoom = LoggerMessage.Define(
        LogLevel.Information,
        ClimatisationAirConditionerServiceEventIds.CoolRoomEventId,
        "AirConditionerMode: [COOLING]");

    private static readonly Action<ILogger, Exception> LogStartTurnOff = LoggerMessage.Define(
        LogLevel.Information,
        ClimatisationAirConditionerServiceEventIds.TurnOffEventId,
        "AirConditionerMode: [TURN OFF]");

    private readonly ActivitySource _activitySource;

    private readonly ILogger _logger;

    private readonly Gauge _temperatureGauge;

    public ClimatisationAirConditionerServiceDiagnostics(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger(ClimatisationAirConditionerConstants.AppName);

        _activitySource = new ActivitySource(ClimatisationAirConditionerConstants.AppName);

        _temperatureGauge = Metrics.CreateGauge("airconditioner_service_temperature", "The current temperature detected by the air conditioner.");
    }

    public Activity ReportingTemperature(Temperature temperature)
    {
        string formatedTemperature = temperature.Value.ToString("F2");

        _temperatureGauge.Set(decimal.ToDouble(temperature.Value));

        LogTemperatureReport(_logger, formatedTemperature, temperature.Unit, null);

        return _activitySource.StartActivity("Reporting temperature measurement", ActivityKind.Client);
    }

    public Activity StartSeedAdaptionLoopConfiguration()
    {
        LogSeedAdaptionLoopConfiguration(_logger, null);

        return _activitySource.StartActivity("Seed Adaption Loop Configuration");
    }

    public Activity StartHeating()
    {
        LogStartHeatingRoom(_logger, null);

        return _activitySource.StartActivity("Heat room");
    }

    public Activity StartCooling()
    {
        LogStartCoolingRoom(_logger, null);

        return _activitySource.StartActivity("Cool room");
    }

    public Activity StartTurnOff()
    {
        LogStartTurnOff(_logger, null);

        return _activitySource.StartActivity("Turn off");
    }

    public void ErrorReportingTemperature(Exception exception)
    {
        LogErrorReportingTemperature(_logger, exception);
    }

    private static class ClimatisationAirConditionerServiceEventIds
    {
        public static EventId TemperatureReportEventId = new EventId(100, nameof(TemperatureReportEventId));

        public static EventId ErrorReportingTemperatureEventId = new EventId(200, nameof(ErrorReportingTemperatureEventId));

        public static EventId SeedAdaptionLoopConfiguration = new EventId(300, nameof(SeedAdaptionLoopConfiguration));

        public static EventId HeatRoomEventId = new EventId(400, nameof(HeatRoomEventId));

        public static EventId CoolRoomEventId = new EventId(500, nameof(CoolRoomEventId));

        public static EventId TurnOffEventId = new EventId(600, nameof(TurnOffEventId));
    }
}
