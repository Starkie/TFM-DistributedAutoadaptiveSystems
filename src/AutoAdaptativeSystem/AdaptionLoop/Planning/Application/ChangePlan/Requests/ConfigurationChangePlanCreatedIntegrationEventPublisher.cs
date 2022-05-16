namespace Planning.Service.Application.ChangePlan.Requests;

using Core.Bus.Publisher;
using Execute.Service.Contracts;
using Planning.Contracts.IntegrationEvents;
using Rebus.Bus;

public class ConfigurationChangePlanCreatedIntegrationEventPublisher
    : RequestQueuePublisher<ExecuteChangePlanRequest>
{
    public ConfigurationChangePlanCreatedIntegrationEventPublisher(IBus bus)
        : base(bus, ExecuteServiceConstants.Queues.ExecuteServiceQueue)
    {
    }
}
