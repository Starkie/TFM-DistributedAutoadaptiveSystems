namespace Climatisation.AirConditioner.Application.AirConditioners;

using Climatisation.AirConditioner.Domain.Thermometers.ValueObjects;

public interface IAirConditionerService
{
    void TurnOff();

    Temperature GetRoomTemperature();

    void Cool();

    void Heat();
}
