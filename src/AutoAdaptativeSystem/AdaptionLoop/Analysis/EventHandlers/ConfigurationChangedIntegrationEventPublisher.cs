namespace Analysis.Service.Controllers.IntegrationEvents;

using Analysis.Contracts.IntegrationEvents;
using Core.Bus.Publisher;
using Rebus.Bus;

public class SystemConfigurationChangeRequestIntegrationEventPublisher
    : IntegrationEventPublisher<SystemConfigurationChangeRequestIntegrationEvent>
{
    public SystemConfigurationChangeRequestIntegrationEventPublisher(IBus bus)
        : base(bus)
    {
    }
}
