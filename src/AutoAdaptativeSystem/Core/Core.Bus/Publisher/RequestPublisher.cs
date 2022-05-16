namespace Core.Bus.Publisher;

using Core.Bus.Contracts.Requests;
using MediatR;
using Rebus.Bus;

public class RequestPublisher<TRequest>
    : IRequestPublisher<TRequest>
    where TRequest : Request
{
    private readonly IBus _bus;

    public RequestPublisher(IBus bus)
    {
        _bus = bus;
    }

    public async Task<Unit> Handle(TRequest request, CancellationToken cancellationToken)
    {
        await Publish(request);

        return Unit.Value;
    }

    public virtual async Task Publish(TRequest request)
    {
        await _bus.Publish(request);
    }
}
