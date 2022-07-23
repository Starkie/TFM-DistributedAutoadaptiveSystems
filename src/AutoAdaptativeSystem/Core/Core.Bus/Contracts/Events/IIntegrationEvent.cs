namespace Core.Bus.Contracts.Events;

using MediatR;

public interface IIntegrationEvent : IRequest, INotification
{
}
