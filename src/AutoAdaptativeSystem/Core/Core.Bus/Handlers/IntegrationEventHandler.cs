namespace Core.Bus.Handlers;

using Core.Bus.Contracts.Events;
using Rebus.Handlers;

public interface IIntegrationEventHandler<T> : IHandleMessages<T>
    where T : IIntegrationEvent
{
}
