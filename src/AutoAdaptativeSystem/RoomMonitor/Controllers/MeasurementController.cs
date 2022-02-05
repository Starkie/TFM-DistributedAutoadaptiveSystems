using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RoomMonitor.Controllers;

using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using MonitoringService.ApiClient.Api;
using RoomMonitor.DTOS;
using PropertyDTO = MonitoringService.ApiClient.Model.PropertyDTO;

[ApiController]
[Route("[controller]")]
public class MeasurementController : ControllerBase
{
    private readonly ILogger<MeasurementController> _logger;

    private readonly IMediator _mediator;

    private readonly RoomMonitorDiagnostics _roomMonitorDiagnostics;

    public MeasurementController(IMediator mediator)
    {
        _mediator = mediator;
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
