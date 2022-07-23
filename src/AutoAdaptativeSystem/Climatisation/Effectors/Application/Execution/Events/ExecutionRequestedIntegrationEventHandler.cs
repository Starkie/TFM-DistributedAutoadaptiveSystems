namespace Climatisation.Effectors.Service.Application.Execution.Events;

using System.Threading.Tasks;
using Climatisation.AirConditioner.Contracts;
using Climatisation.Effectors.Service.Application.AirConditioner.Requests;
using Climatisation.Effectors.Service.Diagnostics;
using Core.Bus.Handlers;
using Execute.Contracts.IntegrationEvents;
using MediatR;
using Planning.Contracts.IntegrationEvents.AdaptionActions;

public class ExecutionRequestedIntegrationEventConsumer
    : IIntegrationEventConsumer<ExecutionRequestedIntegrationEvent>
{
    private readonly ClimatisationEffectorServiceDiagnostics _diagnostics;

    private readonly IMediator _mediator;

    public ExecutionRequestedIntegrationEventConsumer(ClimatisationEffectorServiceDiagnostics diagnostics, IMediator mediator)
    {
        _diagnostics = diagnostics;
        _mediator = mediator;
    }

    public async Task Handle(ExecutionRequestedIntegrationEvent requestedIntegrationEvent)
    {
        using var activity = _diagnostics.StartExecuteChangePlan();

        foreach (var action in requestedIntegrationEvent.Actions)
        {
            _diagnostics.ExecuteAdaptionAction(action, requestedIntegrationEvent.Symptoms);

            switch (action)
            {
                case DeploymentAction _:
                    // TODO: Implement.
                    break;
                case BindingAction _:
                    // TODO: Implement.
                    break;
                case SetParameterAction setParameterAction:
                    await ExecuteSetParameterAction(setParameterAction, requestedIntegrationEvent.Symptoms);

                    break;
            }
        }
    }

    private async Task ExecuteSetParameterAction(SetParameterAction setParameterAction, IEnumerable<Symptom> symptoms)
    {
        var request = setParameterAction.PropertyName switch
        {
            ClimatisationAirConditionerConstants.Configuration.Mode =>
                new SetAirConditionerModeRequest(setParameterAction.PropertyName, setParameterAction.PropertyValue),
            _ => null,
        };

        if (request is null)
        {
            return;
        }

        await _mediator.Send(request);
    }
}
