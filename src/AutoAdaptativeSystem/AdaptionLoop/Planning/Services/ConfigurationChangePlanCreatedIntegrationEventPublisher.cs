namespace Planning.Service.Services;

using Core.Bus.Publisher;
using Planning.Contracts.IntegrationEvents;
using Rebus.Bus;

public class ConfigurationChangePlanCreatedIntegrationEventPublisher
    : IntegrationEventPublisher<ConfigurationChangePlanCreatedIntegrationEvent>
{
    public ConfigurationChangePlanCreatedIntegrationEventPublisher(IBus bus)
        : base(bus)
    {
    }
}
