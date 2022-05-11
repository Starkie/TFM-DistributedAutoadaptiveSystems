namespace Core.Bus.Handlers;

using Core.Bus.Contracts.Requests;
using Rebus.Handlers;

public interface IRequestConsumer<TRequest> : IHandleMessages<TRequest>
    where TRequest : Request
{
}
