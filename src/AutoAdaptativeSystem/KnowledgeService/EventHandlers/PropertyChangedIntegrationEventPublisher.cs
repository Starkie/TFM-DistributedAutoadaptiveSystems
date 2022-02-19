namespace KnowledgeService.Controllers.IntegrationEvents;

using Core.Bus.Publisher;
using Knowledge.Contracts.Controllers.IntegrationEvents;
using Rebus.Bus;

public class PropertyChangedIntegrationEventPublisher
    : IntegrationEventPublisher<PropertyChangedIntegrationEvent>
{
    public PropertyChangedIntegrationEventPublisher(IBus bus) : base(bus)
    {
    }
}
