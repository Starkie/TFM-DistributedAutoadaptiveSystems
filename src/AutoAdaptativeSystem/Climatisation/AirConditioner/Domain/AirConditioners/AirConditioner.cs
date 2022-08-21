namespace Climatisation.AirConditioner.Domain.AirConditioners;

using System.Collections.Concurrent;
using Climatisation.AirConditioner.Contracts;
using Climatisation.AirConditioner.Domain.AirConditioners.Events.Domain;
using Climatisation.AirConditioner.Domain.Thermometers;
using Climatisation.AirConditioner.Domain.Thermometers.ValueObjects;

public abstract class AirConditioner
{
    // TODO: Move to a base class Entity.
    // ConcurrentBag is used because the entity is shared between processes.
    // In a real system a List should be used and the entity would not be shared.
    // Every individual request should have their own instance.
    private ConcurrentBag<DomainEvent> _domainEvents;

    public IEnumerable<DomainEvent> DomainEvents => _domainEvents;

    private readonly Thermometer _thermometer;

    public AirConditioner(Thermometer thermometer)
    {
        _thermometer = thermometer;
        CurrentMode = AirConditioningMode.Off;
    }

    public AirConditioningMode CurrentMode { get; protected set; }

    public virtual Temperature GetRoomTemperature()
    {
        _domainEvents = new ConcurrentBag<DomainEvent>();

        return _thermometer.Measure();
    }

    public void Cool()
    {
        SetMode(AirConditioningMode.Cooling);
    }

    public void Heat()
    {
        SetMode(AirConditioningMode.Heating);
    }

    public void TurnOff()
    {
        SetMode(AirConditioningMode.Off);
    }

    private void SetMode(AirConditioningMode mode)
    {
        CurrentMode = mode;

        _domainEvents.Add(new AirConditionerModeChanged(CurrentMode));
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
