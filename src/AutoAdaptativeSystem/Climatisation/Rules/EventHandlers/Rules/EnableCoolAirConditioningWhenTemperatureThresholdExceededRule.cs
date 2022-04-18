namespace Climatisation.Rules.Service.EventHandlers.Rules;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Analysis.Contracts.Attributes;
using Analysis.Service.ApiClient.Api;
using Analysis.Service.ApiClient.Model;
using Climatisation.Contracts;
using Climatisation.Rules.Service.Diagnostics;
using Climatisation.Rules.Service.Services;
using Microsoft.Extensions.DependencyInjection;

// TODO: Suscribirse a los cambios de configuración del sistema.
[RuleKnowledgePropertyDependency(Temperature)]
public class EnableCoolAirConditioningWhenTemperatureThresholdExceededRule : RuleBase
{
    private const string RuleName = nameof(EnableCoolAirConditioningWhenTemperatureThresholdExceededRule);

    private const string Temperature = "Temperature";

    // TODO: Extract to constants of the Air Conditiniong service.
    private const string AirConditioningServiceName = "airconditioning-service";

    private static readonly IEnumerable<string> propertyNames =
        typeof(EnableCoolAirConditioningWhenTemperatureThresholdExceededRule)
            .GetRulePropertyDependencies();

    private readonly IConfigurationService _configurationService;

    private readonly IPropertyService _propertyService;

    private readonly ISystemApi _systemApi;

    public EnableCoolAirConditioningWhenTemperatureThresholdExceededRule(
        ClimatisationRulesDiagnostics diagnostics,
        IConfigurationService configurationService,
        IPropertyService propertyService,
        ISystemApi systemApi)
        : base(diagnostics, RuleName, propertyNames)
    {
        _configurationService = configurationService;
        _propertyService = propertyService;
        _systemApi = systemApi;
    }

    protected override async Task<bool> EvaluateCondition()
    {
        var currentTemperature = await _propertyService.GetProperty<TemperatureMeasurementDTO>(Temperature, CancellationToken.None);

        if (currentTemperature is null)
        {
            return false;
        }

        // TODO: Extraer los nombres de las propiedades a contratos.
        var isEnabled = await _configurationService.GetConfigurationKey<bool?>(AirConditioningServiceName, "enabled", CancellationToken.None);

        var thresholdTemperature = await _configurationService.GetConfigurationKey<float?>(AirConditioningServiceName, "thresholdHotTemperature",  CancellationToken.None);
        thresholdTemperature ??= 25.0f;

        return isEnabled != true && currentTemperature.Value > thresholdTemperature;
    }

    protected override async Task Execute()
    {
        var changeRequests = new List<ServiceConfigurationDTO>
        {
            new()
            {
                ServiceName = AirConditioningServiceName,
                IsActive = true,
                ConfigurationProperties = new List<ConfigurationProperty>()
                {
                    new() { Name = "enabled", Value = "true" },
                },
            },
        };

        var symptoms = new List<SymptomDTO> { new("temperature-greater-than-target", "true") };

        var systemConfigurationChangeRequest = new SystemConfigurationChangeRequestDTO()
        {
            ServiceConfiguration = changeRequests,
            Symptoms = symptoms,
            Timestamp = DateTime.UtcNow,
        };

        await _systemApi.SystemRequestChangePostAsync(systemConfigurationChangeRequest, CancellationToken.None);
    }
}
