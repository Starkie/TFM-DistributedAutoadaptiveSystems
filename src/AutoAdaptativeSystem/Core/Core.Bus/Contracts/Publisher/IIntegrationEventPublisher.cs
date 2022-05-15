namespace Core.Bus.Contracts.Publisher;

using Core.Bus.Contracts.Events;
using MediatR;

public interface IIntegrationEventPublisher<TIntegrationEvent> : INotificationHandler<TIntegrationEvent>
    where TIntegrationEvent : IIntegrationEvent
{
}
