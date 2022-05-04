namespace Planning.Service.Services;

using System.Collections.Generic;
using System.Threading.Tasks;
using Analysis.Contracts.IntegrationEvents;
using Knowledge.Service.ApiClient.Services;
using MediatR;
using Planning.Contracts;
using Planning.Contracts.IntegrationEvents;
using Planning.Contracts.IntegrationEvents.AdaptionActions;
using Planning.Service.Diagnostics;

public class PlanificationService : IPlanificationService
{
    private readonly PlanningServiceDiagnostics _diagnostics;

    private readonly IConfigurationService _configurationService;

    private readonly IMediator _mediator;

    public PlanificationService(
        PlanningServiceDiagnostics diagnostics,
        IConfigurationService configurationService,
        IMediator mediator)
    {
        _diagnostics = diagnostics;
        _configurationService = configurationService;
        _mediator = mediator;
    }

    public async Task PlanNextConfiguration(SystemConfigurationChangeRequestIntegrationEvent systemConfigurationChangeRequestIntegrationEvent)
    {
        using var activity = _diagnostics.DefininingChangePlan();

        var changePlan = new ConfigurationChangePlan();

        foreach (var request in systemConfigurationChangeRequestIntegrationEvent.ConfigurationRequests)
        {
            var deploymentAction = await BuildDeploymentAction(request);

            if (deploymentAction is not null)
            {
                changePlan.Actions.Add(deploymentAction);
            }

            changePlan.Actions.AddRange(await BuildBindingActions(request));

            changePlan.Actions.AddRange(await BuildSetParameterActions(request));
        }

        if (changePlan.Actions.Count == 0)
        {
            _diagnostics.ChangePlanDiscarded();

            return;
        }

        _diagnostics.ConfigurationChangePlanCreated(changePlan);

        await _mediator.Publish(new ConfigurationChangePlanCreatedIntegrationEvent()
        {
            ChangePlan = changePlan,
        });
    }

    private async Task<AdaptionAction> BuildDeploymentAction(SystemConfigurationRequest request)
    {
        var isDeployed =
            await _configurationService.GetConfigurationKey<bool?>(request.ServiceName, AdaptionLoopPlanningConstants.Configuration.IsDeployed);

        if (isDeployed == request.IsDeployed)
        {
            return null;
        }

        var deploymentAction = request.IsDeployed switch
        {
            true => AdaptionActionType.Deploy,
            false => AdaptionActionType.Undeploy,
        };

        return new DeploymentAction(deploymentAction, request.ServiceName);
    }

    private async Task<IEnumerable<AdaptionAction>> BuildBindingActions(SystemConfigurationRequest request)
    {
        var bindingActions = new List<BindingAction>();

        foreach (var binding in request.Bindings)
        {
            var currentValue =
                await _configurationService.GetConfigurationKey<string>(request.ServiceName, binding.TargetService);

            if (string.IsNullOrEmpty(currentValue) == binding.Active)
            {
                continue;
            }

            var bindingAction = binding.Active switch
            {
                true => AdaptionActionType.Bind,
                false => AdaptionActionType.Unbind,
            };

            bindingActions.Add(new BindingAction(bindingAction, request.ServiceName, binding.TargetService));
        }

        return bindingActions;
    }

    private async Task<IEnumerable<AdaptionAction>> BuildSetParameterActions(SystemConfigurationRequest request)
    {
        var setParameterActions = new List<SetParameterAction>();

        foreach (var configuration in request.ConfigurationProperties)
        {
            var currentValue =
                await _configurationService.GetConfigurationKey<string>(request.ServiceName, configuration.Name);

            if (string.IsNullOrEmpty(currentValue)
                || !configuration.Value.Equals(currentValue))
            {
                setParameterActions.Add(new SetParameterAction(request.ServiceName, configuration.Name, configuration.Value));
            }
        }

        return setParameterActions;
    }
}
