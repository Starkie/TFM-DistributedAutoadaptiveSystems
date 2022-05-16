namespace Climatisation.AirConditioner.Application.AirConditioners;

using Climatisation.AirConditioner.Contracts;
using Climatisation.AirConditioner.Domain.Thermometers.ValueObjects;

public interface IAirConditionerService
{
    Task TurnOff();

    Temperature GetRoomTemperature();

    Task Cool();

    AirConditioningMode GetCurrentMode();

    Task Heat();
}
