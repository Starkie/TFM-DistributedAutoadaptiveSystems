namespace Climatisation.AirConditioner.Domain.Thermometers.ValueObjects;

public class Temperature
{
    public Temperature(decimal value, TemperatureUnit unit)
    {
        Value = value;
        Unit = unit;
    }

    public decimal Value { get; }

    public TemperatureUnit Unit { get; }
}
