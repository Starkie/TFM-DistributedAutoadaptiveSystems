namespace Analysis.Service.SystemConfiguration.Events;

using System.Threading.Tasks;
using Analysis.Service.Diagnostics;
using Core.Bus.Handlers;
using Knowledge.Contracts.IntegrationEvents;
using Rebus.Bus;

public class ConfigurationChangedIntegrationEventConsumer
    : IIntegrationEventConsumer<ConfigurationChangedIntegrationEvent>
{
    private readonly AnalysisServiceDiagnostics _analysisServiceDiagnostics;

    private readonly IBus _bus;

    public ConfigurationChangedIntegrationEventConsumer(AnalysisServiceDiagnostics analysisServiceDiagnostics, IBus bus)
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
