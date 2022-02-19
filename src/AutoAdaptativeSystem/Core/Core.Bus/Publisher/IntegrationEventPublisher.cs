namespace Core.Bus.Publisher;

using Core.Bus.Events;
using Rebus.Bus;

public abstract class IntegrationEventPublisher<TIntegrationEvent> : IIntegrationEventPublisher<TIntegrationEvent> where TIntegrationEvent : IIntegrationEvent
{
    private readonly IBus _bus;

    protected IntegrationEventPublisher(IBus bus)
    {
        _bus = bus;
    }

    public async Task PublishAsync(TIntegrationEvent integrationEvent)
    {
        await _bus.Publish(integrationEvent);
    }
}
