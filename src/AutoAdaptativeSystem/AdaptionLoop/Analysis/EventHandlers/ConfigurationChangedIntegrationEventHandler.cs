namespace Analysis.Service.Controllers.IntegrationEvents;

using System.Threading.Tasks;
using Analysis.Service.Diagnostics;
using Core.Bus.Handlers;
using Knowledge.Contracts.IntegrationEvents;
using Rebus.Bus;

public class ConfigurationChangedIntegrationEventHandler : IIntegrationEventHandler<ConfigurationChangedIntegrationEvent>
{
    private readonly AnalysisServiceDiagnostics _analysisServiceDiagnostics;

    private readonly IBus _bus;

    public ConfigurationChangedIntegrationEventHandler(AnalysisServiceDiagnostics analysisServiceDiagnostics, IBus bus)
    {
        _analysisServiceDiagnostics = analysisServiceDiagnostics;

        _bus = bus;
    }

    public async Task Handle(ConfigurationChangedIntegrationEvent message)
    {
        using var activity = _analysisServiceDiagnostics.ConfigurationChangedEventReceived(message);

        await _bus.Advanced.Topics.Publish(
            message.ServiceName + "." + message.ConfigurationName,
            new Analysis.Service.Contracts.IntegrationEvents.ConfigurationChangedIntegrationEvent(message.ServiceName, message.ConfigurationName));
    }
}
