namespace Planning.Service.EventHandlers;

using System.Threading.Tasks;
using Core.Bus.Handlers;
using Planning.Contracts.IntegrationEvents;
using Planning.Service.Diagnostics;
using Rebus.Bus;

public class SystemChangeRequestIntegrationEventHandler : IntegrationEventHandler<SystemChangeRequestIntegrationEvent>
{
    private readonly PlanningServiceDiagnostics _diagnostics;

    private readonly IBus _bus;

    public SystemChangeRequestIntegrationEventHandler(PlanningServiceDiagnostics diagnostics, IBus bus)
    {
        _diagnostics = diagnostics;

        _bus = bus;
    }

    public override async Task Handle(SystemChangeRequestIntegrationEvent message)
    {
        _diagnostics.SystemChangeRequestReceived(message);
    }
}
