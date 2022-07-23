namespace Core.Bus.Handlers;

using Core.Bus.Contracts.Events;
using MediatR;

public interface IIntegrationEventHandler<T> : INotificationHandler<T>
    where T : IIntegrationEvent
{
}
