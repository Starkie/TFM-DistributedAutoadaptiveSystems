namespace Climatisation.Rules.EventHandlers.Rules;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Analysis.Contracts.Attributes;
using Analysis.Service.ApiClient.Api;
using Analysis.Service.ApiClient.Model;
using Analysis.Service.Contracts.IntegrationEvents;
using Climatisation.Contacts;
using Climatisation.Rules.Diagnostics;
using Climatisation.Rules.Services;
using Microsoft.Extensions.DependencyInjection;

[RuleKnowledgePropertyDependency(Temperature)]
public class EnableCoolAirConditioningWhenTemperatureThresholdExceededRule : RuleBase
{

    private const string RuleName = nameof(EnableCoolAirConditioningWhenTemperatureThresholdExceededRule);

    private const string Temperature = "Temperature";

    private static readonly IEnumerable<string> propertyNames =
        typeof(EnableCoolAirConditioningWhenTemperatureThresholdExceededRule)
            .GetRulePropertyDependencies();
    private readonly IConfigurationApi _configurationApi;

    private readonly IPropertyService _propertyService;

    public EnableCoolAirConditioningWhenTemperatureThresholdExceededRule(
        ClimatisationRulesDiagnostics diagnostics,
        IConfigurationApi configurationApi,
        IPropertyService propertyService)
        : base(diagnostics, RuleName, propertyNames)
    {
        _configurationApi = configurationApi;
        _propertyService = propertyService;
    }

    public override Task Handle(PropertyChangedIntegrationEvent message)
    {
        throw new NotImplementedException();
    }

    protected override async Task<bool> EvaluateCondition()
    {
        var currentTemperature = await _propertyService.GetProperty<TemperatureMeasurementDTO>(Temperature, CancellationToken.None);

        if (currentTemperature is null)
        {
            return false;
        }

        // TODO: Obtener la configuración del servicio.
        // var isEnabled = await _configurationApi.ConfigurationConfigurationNameGetAsync("");

        return currentTemperature.Value > 25.0;
    }

    protected override async Task Execute()
    {
        var symptoms = new List<SymptomDTO> { new SymptomDTO("temperature-greater-than-target", "true") };

        var changeRequests = new List<ChangeRequestDTO> { new ChangeRequestDTO("airconditioning-service", "enabled", "true") };

        await _configurationApi.ConfigurationRequestChangePostAsync(new ConfigurationChangeRequestDTO(DateTime.Now, symptoms, changeRequests), CancellationToken.None);
    }
}
