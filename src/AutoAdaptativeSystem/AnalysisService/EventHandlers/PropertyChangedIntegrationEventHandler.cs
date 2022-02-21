namespace AnalysisService.EventHandlers;

using System.Threading.Tasks;
using AnalysisService.Diagnostics;
using Core.Bus.Handlers;
using Knowledge.Contracts.Controllers.IntegrationEvents;
using Rebus.Bus;

public class PropertyChangedIntegrationEventHandler : IntegrationEventHandler<PropertyChangedIntegrationEvent>
{
    private readonly AnalysisServiceDiagnostics _analysisServiceDiagnostics;

    private readonly IBus _bus;

    public PropertyChangedIntegrationEventHandler(AnalysisServiceDiagnostics analysisServiceDiagnostics, IBus bus)
    {
        _analysisServiceDiagnostics = analysisServiceDiagnostics;

        _bus = bus;
    }

    public override async Task Handle(PropertyChangedIntegrationEvent message)
    {
        _analysisServiceDiagnostics.PropertyChangeEventReceived(message);

        await _bus.Advanced.Topics.Publish(
            message.PropertyName,
            new AnalysisService.Contracts.IntegrationEvents.PropertyChangedIntegrationEvent(message.PropertyName));
    }
}
