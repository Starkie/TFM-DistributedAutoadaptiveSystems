namespace Climatisation.AirConditioner.Domain.Thermometers;

using Climatisation.AirConditioner.Domain.Thermometers.ValueObjects;

public abstract class Thermometer
{
    public abstract Temperature Measure();
}
