namespace Climatisation.AirConditioner.Application.AirConditioners;

using Climatisation.AirConditioner.Contracts;
using Climatisation.AirConditioner.Domain.AirConditioners;
using Climatisation.AirConditioner.Domain.Thermometers.ValueObjects;
using MediatR;

public class AirConditionerService : IAirConditionerService
{
    private readonly AirConditioner _airConditioner;

    private readonly IMediator _mediator;

    public AirConditionerService(AirConditioner airConditioner, IMediator mediator)
    {
        _airConditioner = airConditioner;
        _mediator = mediator;
    }

    public async Task TurnOff()
    {
        _airConditioner.TurnOff();

        // TODO: Move somewhere else.
        await DispatchDomainEvents(_airConditioner);
    }

    public Temperature GetRoomTemperature()
    {
        return _airConditioner.GetRoomTemperature();
    }

    public async Task Cool()
    {
        _airConditioner.Cool();

        // TODO: Move somewhere else.
        await DispatchDomainEvents(_airConditioner);
    }

    public AirConditioningMode GetCurrentMode()
    {
        return _airConditioner.CurrentMode;
    }

    public async Task Heat()
    {
        _airConditioner.Heat();

        // TODO: Move somewhere else.
        await DispatchDomainEvents(_airConditioner);
    }

    private async Task DispatchDomainEvents(AirConditioner airConditioner)
    {
        // TODO: Enable recursive dispatch, in case the entity has new domainEvents.
        foreach (var domainEvent in airConditioner.DomainEvents)
        {
            await _mediator.Publish(domainEvent);
        }

        airConditioner.ClearDomainEvents();
    }
}
