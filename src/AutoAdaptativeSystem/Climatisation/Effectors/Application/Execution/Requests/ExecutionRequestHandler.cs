namespace Climatisation.Effectors.Service.Application.Execution.Requests;

using System.Threading.Tasks;
using Climatisation.AirConditioner.Contracts;
using Climatisation.Effectors.Service.Application.AirConditioner.Requests;
using Climatisation.Effectors.Service.Diagnostics;
using Core.Bus.Handlers;
using Execute.Contracts.IntegrationEvents;
using MediatR;
using Planning.Contracts.IntegrationEvents.AdaptionActions;

public class ExecutionRequestHandler
    : IRequestConsumer<ExecutionRequest>
{
    private readonly ClimatisationEffectorServiceDiagnostics _diagnostics;

    private readonly IMediator _mediator;

    public ExecutionRequestHandler(ClimatisationEffectorServiceDiagnostics diagnostics, IMediator mediator)
    {
        _diagnostics = diagnostics;
        _mediator = mediator;
    }

    public async Task Handle(ExecutionRequest request)
    {
        using var activity = _diagnostics.StartExecuteChangePlan();

        foreach (var action in request.Actions)
        {
            _diagnostics.ExecuteAdaptionAction(action);

            switch (action)
            {
                case DeploymentAction _:
                    // TODO: Implement. ¿Debería ser responsabilidad del AdaptionLoop.Execute.Service desplegar servicios?.
                    break;
                case BindingAction _:
                    // TODO: Implement.
                    break;
                case SetParameterAction setParameterAction:
                    await ExecuteSetParameterAction(setParameterAction);

                    break;
            }
        }
    }

    private async Task ExecuteSetParameterAction(SetParameterAction setParameterAction)
    {
        var request = setParameterAction.PropertyName switch
        {
            ClimatisationAirConditionerConstants.Configuration.Mode =>
                new SetAirConditionerModeRequest(setParameterAction.PropertyName, setParameterAction.PropertyValue),
            _ => null,
        };

        if (request is not null)
        {
            await _mediator.Send(request);
        }
    }
}
