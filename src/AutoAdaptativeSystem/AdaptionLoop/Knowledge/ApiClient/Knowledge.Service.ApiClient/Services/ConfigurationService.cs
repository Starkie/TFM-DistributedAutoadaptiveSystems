namespace Knowledge.Service.ApiClient.Services;

using System.Threading;
using System.Threading.Tasks;
using Knowledge.Service.ApiClient.Api;
using Knowledge.Service.ApiClient.Client;
using Newtonsoft.Json;

public class ConfigurationService : IConfigurationService
{
    private readonly IServiceApi _serviceConfigurationApi;

    public ConfigurationService(IServiceApi serviceApi)
    {
        _serviceConfigurationApi = serviceApi;
    }

    public async Task<T> GetConfigurationKey<T>(string serviceName, string configurationName, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return default;
        }

        T propertyValue = default;

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
