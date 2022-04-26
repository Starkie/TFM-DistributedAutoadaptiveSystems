namespace Knowledge.Service.Controllers.IntegrationEvents;

using System.Threading;
using System.Threading.Tasks;
using Knowledge.Contracts.IntegrationEvents;
using MediatR;
using Rebus.Bus;

public class ConfigurationChangeRequestIntegrationEventPublisher
    : INotificationHandler<ConfigurationChangedIntegrationEvent>
{
    private readonly IBus _bus;

    public ConfigurationChangeRequestIntegrationEventPublisher(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(ConfigurationChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        await _bus.Publish(notification);
    }
}
