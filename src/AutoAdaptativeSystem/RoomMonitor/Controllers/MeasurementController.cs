using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RoomMonitor.Controllers
{
    using Microsoft.AspNetCore.Http;
    using RoomMonitor.DTOS;

    [ApiController]
    [Route("[controller]")]
    public class MeasurementController : ControllerBase
    {
        private readonly ILogger<MeasurementController> _logger;

        public MeasurementController(ILogger<MeasurementController> logger)
        {
            this._logger = logger;
        }

        [HttpPost("Temperature")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult RegisterTemperatureMeasurement([FromBody]TemperatureMeasurementDTO temperatureMeasurementDto)
        {
            this._logger.LogInformation("[Temperature] - Received Measurement: {Value} {Unit}", temperatureMeasurementDto.Value, temperatureMeasurementDto.Unit);

            // TODO: Implement Logic.
            return this.Ok();
        }

        [HttpPost("Humidity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult RegisterHumidityMeasurement([FromBody]HumidityMeasurementDTO humidityMeasurementDto)
        {
            this._logger.LogInformation("[Humidity] - Received Measurement: {Value} {Unit}", humidityMeasurementDto.Value, humidityMeasurementDto.Unit);

            // TODO: Implement Logic.
            return this.Ok();
        }
    }
}