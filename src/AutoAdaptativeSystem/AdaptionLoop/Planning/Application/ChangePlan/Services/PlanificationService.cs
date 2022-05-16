namespace Planning.Service.Application.ChangePlan.Services;

using System.Collections.Generic;
using System.Threading.Tasks;
using Analysis.Contracts.IntegrationEvents;
using AutoMapper;
using Knowledge.Service.ApiClient.Services;
using MediatR;
using Planning.Contracts;
using Planning.Contracts.IntegrationEvents;
using Planning.Contracts.IntegrationEvents.AdaptionActions;
using Planning.Service.Diagnostics;
using Symptom = Planning.Contracts.IntegrationEvents.Symptom;

public class PlanificationService : IPlanificationService
{
    private readonly PlanningServiceDiagnostics _diagnostics;

    private readonly IConfigurationService _configurationService;

    private readonly IMediator _mediator;

    private readonly IMapper _mapper;

    public PlanificationService(
        PlanningServiceDiagnostics diagnostics,
        IConfigurationService configurationService,
        IMediator mediator,
        IMapper mapper)
    {
        _diagnostics = diagnostics;
        _configurationService = configurationService;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task PlanNextConfiguration(SystemConfigurationChangeRequest systemConfigurationChangeRequest)
    {
        using var activity = _diagnostics.DefininingChangePlan();

        var changePlan = new ConfigurationChangePlan();

        foreach (var configurationRequest in systemConfigurationChangeRequest.ConfigurationRequests)
        {
            var deploymentAction = await BuildDeploymentAction(configurationRequest);

            if (deploymentAction is not null)
            {
                changePlan.Actions.Add(deploymentAction);
            }

            changePlan.Actions.AddRange(await BuildBindingActions(configurationRequest));

            changePlan.Actions.AddRange(await BuildSetParameterActions(configurationRequest));
        }

        if (changePlan.Actions.Count == 0)
        {
            _diagnostics.ChangePlanDiscarded();

            return;
        }

        _diagnostics.ConfigurationChangePlanCreated(changePlan);

        var request = new ExecuteChangePlanRequest()
        {
            ChangePlan = changePlan,
            Symptoms = _mapper.Map<IEnumerable<Symptom>>(systemConfigurationChangeRequest.Symptoms),
        };

        await _mediator.Send(request);
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
