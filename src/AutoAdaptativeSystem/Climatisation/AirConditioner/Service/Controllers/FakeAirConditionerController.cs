namespace Climatisation.AirConditioner.Service.Controllers;

using Climatisation.AirConditioner.Contracts;
using Climatisation.AirConditioner.Domain.AirConditioners;
using Climatisation.AirConditioner.Domain.AirConditioners.Fakes;
using Climatisation.AirConditioner.Domain.Thermometers.ValueObjects;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class FakeAirConditionerController : ControllerBase
{
    private readonly FakeAirConditioner _fakeAirConditioner;

    public FakeAirConditionerController(AirConditioner airConditioner)
    {
        _fakeAirConditioner = (airConditioner as FakeAirConditioner)!;
    }

    /// <summary>
    /// Configures the <see cref="FakeAirConditioner"/> to increase or decrease the
    /// ambient temperature when it is in <see cref="AirConditioningMode.Off"/>
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

    /// <summary>
    ///     Configures the current temperature of the <see cref="FakeAirConditioner"/>.
    ///     This way, tests can be carried out faster.
    /// </summary>
    /// <param name="temperature">
    ///     The new value of the temperature.
    /// </param>
    [HttpPut("temperature")]
    public void UpdateTemperature([FromBody]Temperature temperature)
    {
        _fakeAirConditioner.SetTemperature(temperature);
    }
}
