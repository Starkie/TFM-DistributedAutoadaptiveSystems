namespace Climatisation.Monitor.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Climatisation.Contacts;
using Climatisation.Monitor.DTOS;
using MediatR;
using Microsoft.AspNetCore.Http;

[ApiController]
[Route("[controller]")]
public class MeasurementController : ControllerBase
{
    private readonly IMediator _mediator;

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
