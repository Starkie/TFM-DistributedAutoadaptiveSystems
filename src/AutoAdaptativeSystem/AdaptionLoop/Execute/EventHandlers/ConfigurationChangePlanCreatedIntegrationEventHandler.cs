namespace Execute.Service.EventHandlers;

using System.Linq;
using System.Threading.Tasks;
using Core.Bus.Handlers;
using Execute.Contracts.IntegrationEvents;
using Execute.Service.Diagnostics;
using Planning.Contracts.IntegrationEvents;
using Rebus.Bus;

public class ConfigurationChangePlanCreatedIntegrationEventHandler
    : IIntegrationEventHandler<ConfigurationChangePlanCreatedIntegrationEvent>
{
    private readonly IBus _bus;

    private readonly ExecuteServiceDiagnostics _diagnostics;

    public ConfigurationChangePlanCreatedIntegrationEventHandler(IBus bus, ExecuteServiceDiagnostics diagnostics)
    {
        _bus = bus;
        _diagnostics = diagnostics;
    }

    public async Task Handle(ConfigurationChangePlanCreatedIntegrationEvent message)
    {
        using var activity = _diagnostics.StartExecuteChangePlan();

        var actionsByService = message.ChangePlan.Actions
            .GroupBy(k =>k.ServiceName)
            .ToDictionary(k => k.Key, e => e.AsEnumerable());

        foreach (var serviceName in actionsByService.Keys)
        {
            _diagnostics.ExecuteServiceActions(serviceName, actionsByService[serviceName]);

            await _bus.Advanced.Topics.Publish(serviceName, new ExecutionRequestIntegrationEvent
            {
                ServiceName = serviceName,
                Actions = actionsByService[serviceName],
            });
        }
    }
}
