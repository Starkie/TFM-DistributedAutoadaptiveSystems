namespace Execute.Service.Application.Execution.Events;

using System.Threading;
using System.Threading.Tasks;
using Core.Bus.Contracts.Publisher;
using Execute.Contracts.IntegrationEvents;
using MediatR;
using Rebus.Bus;

public class ExecutionRequestedIntegrationEventPublisher
    : IIntegrationEventPublisher<ExecutionRequestedIntegrationEvent>
{
    private readonly IBus _bus;

    public ExecutionRequestedIntegrationEventPublisher(IBus bus)
    {
        _bus = bus;
    }

    public async Task PublishAsync(ExecutionRequestedIntegrationEvent integrationEvent)
    {
        await _bus.Advanced.Topics.Publish(integrationEvent.ServiceName, integrationEvent);
    }
}
