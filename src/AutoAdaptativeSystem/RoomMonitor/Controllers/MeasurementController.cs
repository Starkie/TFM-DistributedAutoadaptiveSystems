using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RoomMonitor.Controllers
{
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
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

        private readonly IMediator _mediator;

        private readonly IPropertyApi _propertyApi;

        private readonly RoomMonitorDiagnostics _roomMonitorDiagnostics;

        public MeasurementController(IMediator mediator, IPropertyApi propertyApi)
        {
            _mediator = mediator;
            _propertyApi = propertyApi;
        }

        [HttpPost("Temperature")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterTemperatureMeasurement([FromBody]TemperatureMeasurementDTO temperatureMeasurementDto)
        {
            await _mediator.Send(temperatureMeasurementDto);

            return Ok();
        }

        [HttpPost("Humidity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult RegisterHumidityMeasurement([FromBody]HumidityMeasurementDTO humidityMeasurementDto)
        {
            // this._logger.LogInformation("[Humidity] - Received Measurement: @{Measurement}", humidityMeasurementDto);

            // TODO: Implement Logic.
            return Ok();
        }
    }
}
