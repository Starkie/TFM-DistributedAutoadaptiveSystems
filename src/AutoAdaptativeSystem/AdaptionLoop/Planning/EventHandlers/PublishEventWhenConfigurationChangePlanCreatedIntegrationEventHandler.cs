namespace Planning.Service.EventHandlers;

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Planning.Contracts.IntegrationEvents;
using Rebus.Bus;

public class PublishEventWhenConfigurationChangePlanCreatedIntegrationEventHandler
 : INotificationHandler<ConfigurationChangePlanCreatedIntegrationEvent>
{
    private readonly IBus _bus;

    public PublishEventWhenConfigurationChangePlanCreatedIntegrationEventHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(ConfigurationChangePlanCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        await _bus.Publish(notification);
    }
}
