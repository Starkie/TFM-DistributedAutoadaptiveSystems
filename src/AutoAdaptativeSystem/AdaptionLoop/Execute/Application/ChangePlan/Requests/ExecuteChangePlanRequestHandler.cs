namespace Execute.Service.Application.ChangePlan.Requests;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Bus.Handlers;
using Execute.Contracts.IntegrationEvents;
using Execute.Service.Diagnostics;
using MediatR;
using Planning.Contracts.IntegrationEvents;
using Symptom = Execute.Contracts.IntegrationEvents.Symptom;

public class ExecuteChangePlanRequestHandler
    : IRequestConsumer<ExecuteChangePlanRequest>
{
    private readonly IMediator _mediator;

    private readonly ExecuteServiceDiagnostics _diagnostics;

    private readonly IMapper _mapper;

    public ExecuteChangePlanRequestHandler(
        ExecuteServiceDiagnostics diagnostics,
        IMapper mapper,
        IMediator mediator)
    {
        _diagnostics = diagnostics;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task Handle(ExecuteChangePlanRequest message)
    {
        using var activity = _diagnostics.StartExecuteChangePlan();

        var symptoms = _mapper.Map<IEnumerable<Symptom>>(message.Symptoms);

        var actionsByService = message.ChangePlan.Actions
            .GroupBy(k =>k.ServiceName)
            .ToDictionary(k => k.Key, e => e.ToList());

        foreach (var serviceName in actionsByService.Keys)
        {
            _diagnostics.ExecuteServiceActions(serviceName, actionsByService[serviceName]);

            var integrationEvent = new ExecutionRequestedIntegrationEvent
            {
                ServiceName = serviceName,
                Actions = actionsByService[serviceName],
                Symptoms = symptoms,
            };

            await _mediator.Send(integrationEvent);
        }
    }
}
