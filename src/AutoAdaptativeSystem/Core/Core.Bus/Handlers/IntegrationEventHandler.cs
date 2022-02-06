namespace Core.Bus.Handlers;

using Core.Bus.Events;
using Rebus.Handlers;

public abstract class IntegrationEventHandler<T> : IHandleMessages<T>
    where T : IIntegrationEvent
{
    public abstract Task Handle(T message);
}
