namespace Analysis.Service.EventHandlers;

using System.Threading.Tasks;
using Analysis.Service.Diagnostics;
using Core.Bus.Handlers;
using Knowledge.Contracts.IntegrationEvents;
using Rebus.Bus;

public class PropertyChangedIntegrationEventHandler : IIntegrationEventHandler<PropertyChangedIntegrationEvent>
{
    private readonly AnalysisServiceDiagnostics _analysisServiceDiagnostics;

    private readonly IBus _bus;

    public PropertyChangedIntegrationEventHandler(AnalysisServiceDiagnostics analysisServiceDiagnostics, IBus bus)
    {
        _analysisServiceDiagnostics = analysisServiceDiagnostics;

        _bus = bus;
    }

    public async Task Handle(PropertyChangedIntegrationEvent message)
    {
        using var activity = _analysisServiceDiagnostics.PropertyChangeEventReceived(message);

        await _bus.Advanced.Topics.Publish(
            message.PropertyName,
            new Analysis.Service.Contracts.IntegrationEvents.PropertyChangedIntegrationEvent(message.PropertyName));
    }
}
