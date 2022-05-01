namespace Climatisation.AirConditioner.Application.AirConditioners.EventHandlers.Domain;

using Climatisation.AirConditioner.Domain.AirConditioners.Events.Domain;
using MediatR;

public interface IDomainEventHandler<TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : DomainEvent
{
}
