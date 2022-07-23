namespace Knowledge.Service.Controllers.IntegrationEvents;

using System.Threading;
using System.Threading.Tasks;
using Core.Bus.Contracts.Publisher;
using Knowledge.Contracts.IntegrationEvents;
using MediatR;
using Rebus.Bus;

public class ConfigurationChangeRequestIntegrationEventPublisher
    : IIntegrationEventPublisher<ConfigurationChangedIntegrationEvent>
{
    private readonly IBus _bus;

    public ConfigurationChangeRequestIntegrationEventPublisher(IBus bus)
    {
        _bus = bus;
    }

    public async Task<Unit> Handle(ConfigurationChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        await _bus.Publish(notification);

        return Unit.Value;
    }
}
