namespace Climatisation.AirConditioner.Service.BackgroundServices;

using Climatisation.AirConditioner.Application.AirConditioners;
using Climatisation.AirConditioner.Domain.Thermometers.ValueObjects;
using Climatisation.AirConditioner.Service.Diagnostics;
using Climatisation.Monitor.Service.ApiClient.Api;
using Climatisation.Monitor.Service.ApiClient.Model;
using TemperatureUnit = Climatisation.AirConditioner.Domain.Thermometers.ValueObjects.TemperatureUnit;
using TemperatureUnitDto = Climatisation.Monitor.Service.ApiClient.Model.TemperatureUnit;

public class ThermometerBackgroundService : BackgroundService
{
    // TODO: Extract to configuration.
    Guid probeId = new Guid("c02234d3-329c-4b4d-aee0-d220dc25276b");

    private readonly ClimatisationAirConditionerServiceDiagnostics _diagnostics;

    private readonly IServiceProvider _rootServiceProvider;

    private IAirConditionerService _airConditionerService;

    private IMeasurementApi _measurementApi;

    public ThermometerBackgroundService(
        ClimatisationAirConditionerServiceDiagnostics diagnostics,
        IServiceProvider rootServiceProvider)
    {
        _diagnostics = diagnostics;
        _rootServiceProvider = rootServiceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _rootServiceProvider.CreateScope();

            ResolveDependencies(scope.ServiceProvider);

            var temperature = _airConditionerService.GetRoomTemperature();

            await ReportMeasurement(temperature, stoppingToken);

            Thread.Sleep(TimeSpan.FromSeconds(15));
        }
    }

    private void ResolveDependencies(IServiceProvider serviceProvider)
    {
        _airConditionerService = serviceProvider.GetRequiredService<IAirConditionerService>();
        _measurementApi = serviceProvider.GetRequiredService<IMeasurementApi>();
    }

    private static TemperatureUnitDto GetEquivalentUnit(TemperatureUnit unit)
    {
        return unit switch
        {
            TemperatureUnit.CELSIUS => TemperatureUnitDto.CELSIUS,
            TemperatureUnit.KELVIN => TemperatureUnitDto.KELVIN,
            TemperatureUnit.FAHRENHEIT => TemperatureUnitDto.FAHRENHEIT,
        };
    }

    private async Task ReportMeasurement(Temperature temperature, CancellationToken stoppingToken)
    {
        using var activity = _diagnostics.ReportingTemperature(temperature);

        try
        {
            var temperatureDto = new TemperatureMeasurementDTO
            {
                Unit = GetEquivalentUnit(temperature.Unit),
                Value = decimal.ToDouble(temperature.Value),
                DateTime = DateTime.UtcNow,
                ProbeId = probeId,
            };

            await _measurementApi.MeasurementTemperaturePostAsync(temperatureDto, stoppingToken);
        }
        catch (Exception e)
        {
            _diagnostics.ErrorReportingTemperature(e);
        }
    }
}
