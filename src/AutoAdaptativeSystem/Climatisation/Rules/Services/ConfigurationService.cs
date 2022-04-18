namespace Climatisation.Rules.Service.Services;

using System.Threading;
using System.Threading.Tasks;
using Analysis.Service.ApiClient.Api;
using Analysis.Service.ApiClient.Client;
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

        // TODO: Implementar.
        // using var activity = _diagnostics.GetPropertyValue(propertyName);

        try
        {
            var configuration = await _serviceConfigurationApi.ServiceServiceNameConfigurationConfigurationNameGetAsync(serviceName, configurationName, cancellationToken);

            propertyValue = JsonConvert.DeserializeObject<T>(configuration.Value);
        }
        catch (ApiException exception)
        {
            // _logger.LogWarning("API Exception: Returned HTTP code: {Code}", exception.ErrorCode);
        }
        catch (JsonSerializationException exception)
        {
            // _logger.LogWarning("Serialization error: Could not deserialize as '{Type}'", nameof(TemperatureMeasurementDTO));
        }

        return propertyValue;
    }
}
