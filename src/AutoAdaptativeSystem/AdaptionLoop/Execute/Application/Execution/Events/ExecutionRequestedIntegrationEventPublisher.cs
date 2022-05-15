namespace Execute.Service.Application.Execution.Events;

using System.Threading.Tasks;
using Core.Bus.Publisher;
using Execute.Contracts.IntegrationEvents;
using Rebus.Bus;

public class ExecutionRequestedIntegrationEventPublisher
    : IntegrationEventPublisher<ExecutionRequestedIntegrationEvent>
{
    private readonly IBus _bus;

    public ExecutionRequestedIntegrationEventPublisher(IBus bus)
        : base(bus)
    {
        _bus = bus;
    }

    protected override async Task Publish(ExecutionRequestedIntegrationEvent integrationEvent)
    {
        // TODO: Implement an integration event publisher with topics.
        await _bus.Advanced.Topics.Publish(integrationEvent.ServiceName, integrationEvent);
    }
}
