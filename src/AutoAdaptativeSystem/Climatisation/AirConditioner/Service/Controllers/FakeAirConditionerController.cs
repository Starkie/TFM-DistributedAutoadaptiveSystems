namespace Climatisation.AirConditioner.Service.Controllers;

using Climatisation.AirConditioner.Contracts;
using Climatisation.AirConditioner.Domain.AirConditioners;
using Climatisation.AirConditioner.Domain.AirConditioners.Fakes;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class FakeAirConditionerController : ControllerBase
{
    private readonly FakeAirConditioner _fakeAirConditioner;

    public FakeAirConditionerController(AirConditioner airConditioner)
    {
        _fakeAirConditioner = airConditioner as FakeAirConditioner;
    }

    /// <summary>
    /// Configures the <see cref="FakeAirConditioner"/> to increase or decrease the
    /// ambient temperature when it is in <see cref="AirConditioningMode.Disabled"/>
    /// </summary>
    /// <param name="shouldIncreaseTemperature">
    ///     If true, the ambient temperature will increase when the air conditioner is disabled.
    ///     Otherwise, it will decrease.
    /// </param>
    [HttpPut("disabled-mode-configuration/{shouldIncreaseTemperature}")]
    public void UpdateIncreaseTemperatureWhenDisabled(bool shouldIncreaseTemperature)
    {
        _fakeAirConditioner.WhenDisabled(shouldIncreaseTemperature);
    }
}
