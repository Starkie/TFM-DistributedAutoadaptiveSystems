namespace Core.Bus.Publisher;

using Core.Bus.Contracts.Events;
using Core.Bus.Contracts.Publisher;
using Rebus.Bus;

public abstract class IntegrationEventPublisher<TIntegrationEvent> : IIntegrationEventPublisher<TIntegrationEvent>
    where TIntegrationEvent : IIntegrationEvent
{
    private readonly IBus _bus;

    protected IntegrationEventPublisher(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(TIntegrationEvent notification, CancellationToken cancellationToken)
    {
        await Publish(notification);
    }

    protected virtual async Task Publish(TIntegrationEvent integrationEvent)
    {
        await _bus.Publish(integrationEvent);
    }
}
