namespace Climatisation.AirConditioner.Domain.AirConditioners.Fakes;

using Climatisation.AirConditioner.Contracts;
using Climatisation.AirConditioner.Domain.Thermometers.Fakes;
using Climatisation.AirConditioner.Domain.Thermometers.ValueObjects;

public class FakeAirConditioner : AirConditioner
{
    private readonly FakeThermometer _thermometer;

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

    private Temperature UpdateTemperature(Temperature temperature, AirConditioningMode currentMode)
    {
        var newValue = currentMode switch
        {
            AirConditioningMode.Heating => temperature.Value + 1,
            AirConditioningMode.Cooling => temperature.Value - 1,
            _ => temperature.Value + 1, // Increase the temperature to force the rules to be executed.
        };

        return new Temperature(newValue, TemperatureUnit.CELSIUS);
    }
}
