namespace AnalysisService.EventHandlers;

using System.Threading.Tasks;
using AnalysisService.Diagnostics;
using Core.Bus.Handlers;
using Knowledge.Contracts.Controllers.IntegrationEvents;
using Microsoft.Extensions.Logging;

public class PropertyChangedIntegrationEventHandler : IntegrationEventHandler<PropertyChangedIntegrationEvent>
{
    private readonly AnalysisServiceDiagnostics _analysisServiceDiagnostics;

    public PropertyChangedIntegrationEventHandler(AnalysisServiceDiagnostics analysisServiceDiagnostics)
    {
        _analysisServiceDiagnostics = analysisServiceDiagnostics;
    }

    public override Task Handle(PropertyChangedIntegrationEvent message)
    {
        _analysisServiceDiagnostics.PropertyChangeEventReceived(message);

        return Task.CompletedTask;
    }
}
