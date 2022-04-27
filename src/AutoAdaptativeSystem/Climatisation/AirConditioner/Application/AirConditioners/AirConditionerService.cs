namespace Climatisation.AirConditioner.Application.AirConditioners;

using Climatisation.AirConditioner.Domain.AirConditioners;
using Climatisation.AirConditioner.Domain.Thermometers.ValueObjects;

public class AirConditionerService : IAirConditionerService
{
    private readonly AirConditioner _airConditioner;

    public AirConditionerService(AirConditioner airConditioner)
    {
        _airConditioner = airConditioner;
    }

    public void TurnOff()
    {
        _airConditioner.TurnOff();
    }

    public Temperature GetRoomTemperature()
    {
        return _airConditioner.GetRoomTemperature();
    }

    public void Cool()
    {
        _airConditioner.Cool();
    }

    public void Heat()
    {
        _airConditioner.Heat();
    }
}
