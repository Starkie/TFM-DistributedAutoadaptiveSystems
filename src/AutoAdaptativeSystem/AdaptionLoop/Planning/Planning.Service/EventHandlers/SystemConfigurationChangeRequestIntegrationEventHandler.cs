namespace Planning.Service.EventHandlers;

using System.Threading.Tasks;
using Analysis.Contracts.IntegrationEvents;
using Core.Bus.Handlers;
using Planning.Service.Diagnostics;
using Rebus.Bus;

public class SystemConfigurationChangeRequestIntegrationEventHandler : IntegrationEventHandler<SystemConfigurationChangeRequestIntegrationEvent>
{
    private readonly PlanningServiceDiagnostics _diagnostics;

    private readonly IBus _bus;

    public SystemConfigurationChangeRequestIntegrationEventHandler(PlanningServiceDiagnostics diagnostics, IBus bus)
    {
        _diagnostics = diagnostics;

        _bus = bus;
    }

    public override async Task Handle(SystemConfigurationChangeRequestIntegrationEvent message)
    {
        using var activity = _diagnostics.SystemConfigurationChangeRequestReceived(message);
    }
}
