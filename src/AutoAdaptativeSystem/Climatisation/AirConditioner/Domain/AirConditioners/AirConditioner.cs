namespace Climatisation.AirConditioner.Domain.AirConditioners;

using Climatisation.AirConditioner.Domain.Thermometers;
using Climatisation.AirConditioner.Domain.Thermometers.ValueObjects;

public abstract class AirConditioner
{
    private readonly Thermometer _thermometer;

    public AirConditioner(Thermometer thermometer)
    {
        _thermometer = thermometer;
        CurrentMode = AirConditioningMode.Disabled;
    }

    public AirConditioningMode CurrentMode { get; protected set; }

    public virtual Temperature GetRoomTemperature()
    {
        return _thermometer.Measure();
    }

    public void Cool()
    {
        CurrentMode = AirConditioningMode.Cooling;
    }

    public void Heat()
    {
        CurrentMode = AirConditioningMode.Heating;
    }

    public void TurnOff()
    {
        CurrentMode = AirConditioningMode.Disabled;
    }
}
