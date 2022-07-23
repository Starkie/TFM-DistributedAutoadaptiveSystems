namespace Knowledge.Service.Controllers.IntegrationEvents;

using System.Threading;
using System.Threading.Tasks;
using Core.Bus.Contracts.Publisher;
using Knowledge.Contracts.IntegrationEvents;
using MediatR;
using Rebus.Bus;

public class PropertyChangedIntegrationEventPublisher
    : IIntegrationEventPublisher<PropertyChangedIntegrationEvent>
{
    private readonly IBus _bus;

    public PropertyChangedIntegrationEventPublisher(IBus bus)
    {
        _bus = bus;
    }

    public async Task<Unit> Handle(PropertyChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        await _bus.Publish(notification);

        return Unit.Value;
    }
}
