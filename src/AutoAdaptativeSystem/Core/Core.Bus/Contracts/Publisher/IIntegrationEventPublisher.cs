namespace Core.Bus.Contracts.Publisher;

using Core.Bus.Contracts.Events;

public interface IIntegrationEventPublisher<TIntegrationEvent> where TIntegrationEvent : IIntegrationEvent
{
    Task PublishAsync(TIntegrationEvent integrationEvent);
}
