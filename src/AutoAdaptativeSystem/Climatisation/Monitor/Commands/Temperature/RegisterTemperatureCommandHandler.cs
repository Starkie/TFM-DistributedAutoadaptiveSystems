namespace Climatisation.Monitor.Service.Commands.Temperature;

using System;
using System.Threading;
using System.Threading.Tasks;
using Climatisation.Contracts;
using Climatisation.Monitor.Service.Diagnostics;
using MediatR;
using Monitoring.Service.ApiClient.Api;
using Monitoring.Service.ApiClient.Client;
using Monitoring.Service.ApiClient.Model;
using Newtonsoft.Json;

public class RegisterTemperatureCommandHandler : IRequestHandler<TemperatureMeasurementDTO>
{
    private readonly IMonitorApi _monitorApi;

    private readonly IPropertyApi _propertyApi;

    private readonly ClimatisationMonitorDiagnostics _diagnostics;

    private string TemperaturePropertyName = "Temperature";

    public RegisterTemperatureCommandHandler(IMonitorApi monitorApi, IPropertyApi propertyApi, ClimatisationMonitorDiagnostics diagnostics)
    {
        _monitorApi = monitorApi;
        _propertyApi = propertyApi;
        _diagnostics = diagnostics;
    }

    public async Task<Unit> Handle(TemperatureMeasurementDTO request, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Unit.Value;
        }

        using var activity = _diagnostics.RegisterTemperatureMeasurement(request);

        var previousMeasurement = await GetPreviousMeasurement();

        if (previousMeasurement is not null && !IsValidReading(request, previousMeasurement))
        {
            _diagnostics.ProbeReadingNotWithinSafeMargins(request);

            return Unit.Value;
        }

        await RegisterNewTemperatureMeasurement(request);

        return Unit.Value;
    }

    private static bool IsValidReading(TemperatureMeasurementDTO request, TemperatureMeasurementDTO previousMeasurement)
    {
        var differenceTimings = request.DateTime - previousMeasurement.DateTime;

        var readingsWithinOneMinute = differenceTimings <= TimeSpan.FromMinutes(1);

        var isWithinErrorMargin = Math.Abs(request.Value - previousMeasurement.Value) <= 5.0;

        return isWithinErrorMargin || !readingsWithinOneMinute;
    }

    private async Task<TemperatureMeasurementDTO> GetPreviousMeasurement()
    {
        TemperatureMeasurementDTO previousMeasurement = null;

        using var activity = _diagnostics.LogGetPreviousMeasurement();

        try
        {
            PropertyDTO propertyValue = await _propertyApi.PropertyPropertyNameGetAsync(TemperaturePropertyName);

            previousMeasurement = JsonConvert.DeserializeObject<TemperatureMeasurementDTO>(propertyValue.Value);
        }
        catch (ApiException exception)
        {
            // _logger.LogWarning("API Exception: Returned HTTP code: {Code}", exception.ErrorCode);
        }
        catch (JsonSerializationException exception)
        {
            // _logger.LogWarning("Serialization error: Could not deserialize as '{Type}'", nameof(TemperatureMeasurementDTO));
        }

        return previousMeasurement;
    }

    private async Task RegisterNewTemperatureMeasurement(TemperatureMeasurementDTO temperatureMeasurement)
    {
        await _monitorApi.MonitorMonitorIdMeasurementPostAsync(ClimatisationMonitorConstants.MonitorId, new MeasurementDTO()
        {
            ProbeId = Guid.NewGuid(),
            Property = new Property
            {
                Key = TemperaturePropertyName,
                Value = JsonConvert.SerializeObject(temperatureMeasurement),
            },
        }).ConfigureAwait(false);
    }
}
