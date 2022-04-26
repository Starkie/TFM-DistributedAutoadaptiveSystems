namespace Knowledge.Service.Controllers.IntegrationEvents;

using System.Threading;
using System.Threading.Tasks;
using Knowledge.Contracts.IntegrationEvents;
using MediatR;
using Rebus.Bus;

public class PropertyChangedIntegrationEventPublisher
    : INotificationHandler<PropertyChangedIntegrationEvent>
{
    private readonly IBus _bus;

    public PropertyChangedIntegrationEventPublisher(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(PropertyChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        await _bus.Publish(notification);
    }
}
