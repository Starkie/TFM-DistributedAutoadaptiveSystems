namespace Planning.Service.Application.ChangePlan.Requests;

using Core.Bus.Publisher;
using Planning.Contracts.IntegrationEvents;
using Rebus.Bus;

public class ConfigurationChangePlanCreatedIntegrationEventPublisher
    : RequestPublisher<ExecuteChangePlanRequest>
{
    public ConfigurationChangePlanCreatedIntegrationEventPublisher(IBus bus)
        : base(bus)
    {
    }
}
