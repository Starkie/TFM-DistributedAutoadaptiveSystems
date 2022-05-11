namespace Execute.Service.Application.ChangePlan.Requests;

using System.Linq;
using System.Threading.Tasks;
using Core.Bus.Handlers;
using Execute.Contracts.IntegrationEvents;
using Execute.Service.Diagnostics;
using MediatR;
using Planning.Contracts.IntegrationEvents;

public class ExecuteChangePlanRequestHandler
    : IRequestConsumer<ExecuteChangePlanRequest>
{
    private readonly IMediator _mediator;

    private readonly ExecuteServiceDiagnostics _diagnostics;

    public ExecuteChangePlanRequestHandler(ExecuteServiceDiagnostics diagnostics, IMediator mediator)
    {
        _diagnostics = diagnostics;
        _mediator = mediator;
    }

    public async Task Handle(ExecuteChangePlanRequest message)
    {
        using var activity = _diagnostics.StartExecuteChangePlan();

        var actionsByService = message.ChangePlan.Actions
            .GroupBy(k =>k.ServiceName)
            .ToDictionary(k => k.Key, e => e.ToList());

        foreach (var serviceName in actionsByService.Keys)
        {
            _diagnostics.ExecuteServiceActions(serviceName, actionsByService[serviceName]);

            var request = new ExecutionRequest
            {
                ServiceName = serviceName,
                Actions = actionsByService[serviceName],
            };

            await _mediator.Send(request);
        }
    }
}
