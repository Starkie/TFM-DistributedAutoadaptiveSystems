namespace Knowledge.Service.Controllers.IntegrationEvents;

using Core.Bus.Publisher;
using Knowledge.Contracts.IntegrationEvents;
using Knowledge.Contracts.IntegrationEvents.Configuration;
using Rebus.Bus;

public class ConfigurationChangeRequestIntegrationEventPublisher
    : IntegrationEventPublisher<ConfigurationChangeRequestIntegrationEvent>
{
    public ConfigurationChangeRequestIntegrationEventPublisher(IBus bus)
        : base(bus)
    {
    }
}
