namespace Planning.Service.EventHandlers;

using System.Threading;
using System.Threading.Tasks;
using Analysis.Contracts.IntegrationEvents;
using Core.Bus.Handlers;
using Planning.Service.Diagnostics;
using Planning.Service.Services;
using Rebus.Bus;

public class SystemConfigurationChangeRequestIntegrationEventHandler
    : IIntegrationEventHandler<SystemConfigurationChangeRequestIntegrationEvent>
{
    private readonly PlanningServiceDiagnostics _diagnostics;

    private readonly IPlanificationService _planificationService;

    public SystemConfigurationChangeRequestIntegrationEventHandler(
        PlanningServiceDiagnostics diagnostics,
        IPlanificationService planificationService)
    {
        _diagnostics = diagnostics;
        _planificationService = planificationService;
    }

    public async Task Handle(SystemConfigurationChangeRequestIntegrationEvent message)
    {
        _diagnostics.SystemConfigurationChangeRequestReceived(message);

        await _planificationService.PlanNextConfiguration(message);
    }
}
