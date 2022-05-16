namespace Climatisation.AirConditioner.Domain.Thermometers.Fakes;

using Climatisation.AirConditioner.Domain.Thermometers.ValueObjects;

public class FakeThermometer : Thermometer
{
    private Temperature _currentTemperature;

    public FakeThermometer()
    {
        _currentTemperature = new Temperature(21.0m, TemperatureUnit.CELSIUS);
    }

    public override Temperature Measure()
    {
        return _currentTemperature;
    }

    public void SetTemperature(Temperature newTemperature)
    {
        _currentTemperature = newTemperature;
    }
}
