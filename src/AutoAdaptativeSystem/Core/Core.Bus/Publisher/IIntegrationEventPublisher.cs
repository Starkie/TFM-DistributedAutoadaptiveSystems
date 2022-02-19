namespace Core.Bus.Publisher;

using Core.Bus.Events;

public interface IIntegrationEventPublisher<TIntegrationEvent> where TIntegrationEvent : IIntegrationEvent
{
    Task PublishAsync(TIntegrationEvent integrationEvent);
}
