namespace Climatisation.AirConditioner.Domain.AirConditioners.Fakes;

using Climatisation.AirConditioner.Contracts;
using Climatisation.AirConditioner.Domain.Thermometers.Fakes;
using Climatisation.AirConditioner.Domain.Thermometers.ValueObjects;

public class FakeAirConditioner : AirConditioner
{
    private readonly FakeThermometer _thermometer;

    private bool _increaseTemperatureWhenDisabled;

    public FakeAirConditioner(FakeThermometer thermometer)
        : base(thermometer)
    {
        _thermometer = thermometer;
    }

    public override Temperature GetRoomTemperature()
    {
         var updatedTemperature = UpdateTemperature(_thermometer.Measure(), CurrentMode);

         _thermometer.SetTemperature(updatedTemperature);

        return base.GetRoomTemperature();
    }

    public void WhenDisabled(bool increaseTemperature)
    {
        // When disabled, update the temperature to force the rules to be executed.
        // true -> increases temperature
        // false -> decreases it.
        _increaseTemperatureWhenDisabled = increaseTemperature;
    }

    public void SetTemperature(Temperature temperature)
    {
        _thermometer.SetTemperature(temperature);
    }

    private Temperature UpdateTemperature(Temperature temperature, AirConditioningMode currentMode)
    {
        var newValue = currentMode switch
        {
            AirConditioningMode.Heating => temperature.Value + 1,
            AirConditioningMode.Cooling => temperature.Value - 1,
            _ => _increaseTemperatureWhenDisabled?
                temperature.Value + 1 :
                temperature.Value - 1,
        };

        return new Temperature(newValue, TemperatureUnit.CELSIUS);
    }
}
