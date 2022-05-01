namespace Climatisation.AirConditioner.Domain.AirConditioners.Events.Domain;

using Climatisation.AirConditioner.Contracts;

public class AirConditionerModeChanged : DomainEvent
{
    public AirConditioningMode Mode { get; }

    public AirConditionerModeChanged(AirConditioningMode mode)
    {
        Mode = mode;
    }
}
