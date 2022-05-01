namespace Climatisation.AirConditioner.Domain.AirConditioners;

using Climatisation.AirConditioner.Contracts;
using Climatisation.AirConditioner.Domain.AirConditioners.Events.Domain;
using Climatisation.AirConditioner.Domain.Thermometers;
using Climatisation.AirConditioner.Domain.Thermometers.ValueObjects;

public abstract class AirConditioner
{
    // TODO: Move to a base class Entity.
    private List<DomainEvent> _domainEvents;

    public IEnumerable<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    private readonly Thermometer _thermometer;

    public AirConditioner(Thermometer thermometer)
    {
        _thermometer = thermometer;
        CurrentMode = AirConditioningMode.Disabled;
    }

    public AirConditioningMode CurrentMode { get; protected set; }

    public virtual Temperature GetRoomTemperature()
    {
        _domainEvents = new List<DomainEvent>();

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
        SetMode(AirConditioningMode.Disabled);
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
