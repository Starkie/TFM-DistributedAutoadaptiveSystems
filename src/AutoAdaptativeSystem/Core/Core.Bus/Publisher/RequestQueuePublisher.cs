namespace Core.Bus.Publisher;

using Core.Bus.Contracts.Requests;
using MediatR;
using Rebus.Bus;

public class RequestQueuePublisher<TRequest>
    : IRequestPublisher<TRequest>
    where TRequest : Request
{
    private readonly IBus _bus;

    private readonly string _queueName;

    protected RequestQueuePublisher(IBus bus, string queueName)
    {
        _bus = bus;
        _queueName = queueName;
    }

    public async Task<Unit> Handle(TRequest request, CancellationToken cancellationToken)
    {
        await Publish(request);

        return Unit.Value;
    }

    protected virtual async Task Publish(TRequest request)
    {
        await _bus.Advanced.Routing.Send(_queueName, request);
    }
}
