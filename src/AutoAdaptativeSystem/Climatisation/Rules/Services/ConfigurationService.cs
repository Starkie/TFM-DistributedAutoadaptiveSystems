namespace Climatisation.Rules.Service.Services;

using System;
using System.Threading;
using System.Threading.Tasks;
using Analysis.Service.ApiClient.Api;
using Analysis.Service.ApiClient.Client;
using Analysis.Service.ApiClient.Model;
using Climatisation.Rules.Service.Diagnostics;
using Newtonsoft.Json;

public class ConfigurationService : IConfigurationService
{
    private readonly ClimatisationRulesDiagnostics _diagnostics;

    private readonly IServiceApi _serviceConfigurationApi;

    public ConfigurationService(ClimatisationRulesDiagnostics diagnostics, IServiceApi serviceApi)
    {
        _diagnostics = diagnostics;
        _serviceConfigurationApi = serviceApi;
    }

    public async Task<T> GetConfigurationKey<T>(string serviceName, string configurationName, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return default;
        }

        T propertyValue = default;

        ConfigurationDTO configuration = null;

        // TODO: Implementar.
        // using var activity = _diagnostics.GetPropertyValue(propertyName);

        try
        {
            configuration = await _serviceConfigurationApi.ServiceServiceNameConfigurationConfigurationNameGetAsync(serviceName, configurationName, cancellationToken);

            propertyValue = Deserialize<T>(configuration);
        }
        catch (ApiException exception)
        {
            // _logger.LogWarning("API Exception: Returned HTTP code: {Code}", exception.ErrorCode);
        }
        catch (JsonException exception)
        {
            // _logger.LogWarning("Serialization error: Could not deserialize as '{Type}'", nameof(TemperatureMeasurementDTO));
        }

        return propertyValue;
    }

    private static T Deserialize<T>(ConfigurationDTO configuration)
    {
        T propertyValue;

        var targetType = typeof(T);

        if (targetType.IsGenericType)
        {
            targetType = targetType.GenericTypeArguments[0];
        }

        if (configuration is not null && targetType.IsEnum)
        {
            propertyValue = (T)Enum.Parse(targetType, configuration.Value);
        }
        else
        {
            propertyValue = JsonConvert.DeserializeObject<T>(configuration?.Value);
        }

        return propertyValue;
    }
}
