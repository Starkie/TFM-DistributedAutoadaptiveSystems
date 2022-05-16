namespace Core.Bus.Publisher;

using Core.Bus.Contracts.Requests;
using MediatR;

public interface IRequestPublisher<TRequest>
    : IRequestHandler<TRequest>
    where TRequest : Request
{
}
