namespace Analysis.Service.Controllers.IntegrationEvents;

using System.Threading;
using System.Threading.Tasks;
using Analysis.Contracts.IntegrationEvents;
using MediatR;
using Rebus.Bus;

public class PublishEventWhenSystemConfigurationChangeRequestIntegrationEventHandler
    : INotificationHandler<SystemConfigurationChangeRequestIntegrationEvent>
{
    private readonly IBus _bus;

    public PublishEventWhenSystemConfigurationChangeRequestIntegrationEventHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(SystemConfigurationChangeRequestIntegrationEvent notification, CancellationToken cancellationToken)
    {
        await _bus.Publish(notification);
    }
}
