using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RoomMonitor.Controllers
{
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using MonitoringModule.ApiClient.Api;
    using MonitoringModule.ApiClient.Client;
    using MonitoringModule.ApiClient.Model;
    using Newtonsoft.Json;
    using RoomMonitor.DTOS;
    using PropertyDTO = MonitoringModule.ApiClient.Model.PropertyDTO;

    [ApiController]
    [Route("[controller]")]
    public class MeasurementController : ControllerBase
    {
        private const string Temperature = "Temperature";

        private readonly ILogger<MeasurementController> _logger;

        private readonly IMonitorApi _monitorApi;

        private readonly IPropertyApi _propertyApi;

        private readonly Guid _monitorId = new Guid("92bb183c-c94f-41b4-b1b8-916fec3e5db8");

        public MeasurementController(ILogger<MeasurementController> logger, IMonitorApi monitorApi, IPropertyApi propertyApi)
        {
            _logger = logger;
            _monitorApi = monitorApi;
            _propertyApi = propertyApi;
        }

        [HttpPost("Temperature")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterTemperatureMeasurement([FromBody]TemperatureMeasurementDTO temperatureMeasurementDto)
        {
            _logger.LogInformation("[Temperature] - Received Measurement: {Value} {Unit}", temperatureMeasurementDto.Value, temperatureMeasurementDto.Unit);


            TemperatureMeasurementDTO previousMeasurement = await GetPreviousMeasurement();

            if (previousMeasurement is not null)
            {
                _logger.LogInformation(
                    "[Temperature] - The previous value was: {TemperatureValue} {TemperatureUnit}",
                    previousMeasurement.Value,
                    previousMeasurement.Unit);
            }

            await _monitorApi.MonitorMonitorIdMeasurementPostAsync(_monitorId,  new MeasurementDTO()
            {
                ProbeId = Guid.NewGuid(),
                Property = new Property
                {
                    Key = Temperature,
                    Value = JsonConvert.SerializeObject(temperatureMeasurementDto),
                },
            }).ConfigureAwait(false);

            return Ok();
        }

        [HttpPost("Humidity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult RegisterHumidityMeasurement([FromBody]HumidityMeasurementDTO humidityMeasurementDto)
        {
            this._logger.LogInformation("[Humidity] - Received Measurement: {Value} {Unit}", humidityMeasurementDto.Value, humidityMeasurementDto.Unit);

            // TODO: Implement Logic.
            return Ok();
        }

        private async Task<TemperatureMeasurementDTO> GetPreviousMeasurement()
        {
            TemperatureMeasurementDTO previousMeasurement = null;

            try
            {
                PropertyDTO propertyValue = await _propertyApi.PropertyPropertyNameGetAsync(Temperature);

                previousMeasurement = JsonConvert.DeserializeObject<TemperatureMeasurementDTO>(propertyValue.Value);
            }
            catch (ApiException exception)
            {
                _logger.LogWarning("API Exception: Returned HTTP code: {Code}", exception.ErrorCode);
            }
            catch (JsonSerializationException exception)
            {
                _logger.LogWarning("Serialization error: Could not deserialize as '{Type}'", nameof(TemperatureMeasurementDTO));
            }

            return previousMeasurement;
        }

    }
}