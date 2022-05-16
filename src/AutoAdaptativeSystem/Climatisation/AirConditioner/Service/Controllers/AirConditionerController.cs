namespace Climatisation.AirConditioner.Service.Controllers;

using Climatisation.AirConditioner.Application.AirConditioners;
using Climatisation.AirConditioner.Service.Diagnostics;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class AirConditionerController : ControllerBase
{
    private readonly ClimatisationAirConditionerServiceDiagnostics _diagnostics;

    private readonly IAirConditionerService _airConditionerService;

    public AirConditionerController(
        ClimatisationAirConditionerServiceDiagnostics diagnostics,
        IAirConditionerService airConditionerService)
    {
        _diagnostics = diagnostics;
        _airConditionerService = airConditionerService;
    }

    [HttpPost("TurnOff")]
    public async Task TurnOff()
    {
        using var activity = _diagnostics.StartTurnOff();

        await _airConditionerService.TurnOff();
    }

    [HttpPost("Cool")]
    public async Task Cool()
    {
        using var activity = _diagnostics.StartCooling();

        await _airConditionerService.Cool();
    }

    [HttpPost("Heat")]
    public async Task Heat()
    {
        using var activity = _diagnostics.StartHeating();

        await _airConditionerService.Heat();
    }
}
