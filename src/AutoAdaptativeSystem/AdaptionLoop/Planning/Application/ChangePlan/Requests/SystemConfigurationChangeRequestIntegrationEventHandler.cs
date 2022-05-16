namespace Planning.Service.Application.ChangePlan.Requests;

using System.Threading.Tasks;
using Analysis.Contracts.IntegrationEvents;
using Core.Bus.Handlers;
using Planning.Service.Application.ChangePlan.Services;
using Planning.Service.Diagnostics;

public class SystemConfigurationChangeRequestIntegrationEventHandler
    : IRequestConsumer<SystemConfigurationChangeRequest>
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

    public async Task Handle(SystemConfigurationChangeRequest request)
    {
        _diagnostics.SystemConfigurationChangeRequestReceived(request);

        await _planificationService.PlanNextConfiguration(request);
    }
}
