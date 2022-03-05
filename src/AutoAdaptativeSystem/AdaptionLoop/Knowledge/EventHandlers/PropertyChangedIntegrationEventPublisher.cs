namespace Knowledge.Service.Controllers.IntegrationEvents;

using Core.Bus.Publisher;
using Knowledge.Contracts.IntegrationEvents;
using Rebus.Bus;

public class PropertyChangedIntegrationEventPublisher
    : IntegrationEventPublisher<PropertyChangedIntegrationEvent>
{
    public PropertyChangedIntegrationEventPublisher(IBus bus) : base(bus)
    {
    }
}
