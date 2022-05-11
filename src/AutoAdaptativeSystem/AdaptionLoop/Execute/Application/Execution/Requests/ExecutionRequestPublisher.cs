namespace Execute.Service.Application.Execution.Requests;

using System.Threading;
using System.Threading.Tasks;
using Execute.Contracts.IntegrationEvents;
using MediatR;
using Rebus.Bus;

public class ExecutionRequestPublisher
    : IRequestHandler<ExecutionRequest>
{
    private readonly IBus _bus;

    public ExecutionRequestPublisher(IBus bus)
    {
        _bus = bus;
    }

    // TODO: Crear un request handler/consumer que permita especificar el topic.
    public async Task<Unit> Handle(ExecutionRequest request, CancellationToken cancellationToken)
    {
        await _bus.Advanced.Topics.Publish(request.ServiceName, request);

        return Unit.Value;
    }
}
