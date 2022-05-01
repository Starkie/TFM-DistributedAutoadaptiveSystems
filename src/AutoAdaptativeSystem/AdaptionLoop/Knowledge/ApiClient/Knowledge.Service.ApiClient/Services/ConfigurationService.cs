namespace Knowledge.Service.ApiClient.Services;

using System;
using System.Threading;
using System.Threading.Tasks;
using Knowledge.Service.ApiClient.Api;
using Knowledge.Service.ApiClient.Client;
using Knowledge.Service.ApiClient.Model;
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

            propertyValue = Deserialize<T>(configuration);
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

    private static T Deserialize<T>(ConfigurationDTO configuration)
    {
        if (configuration is null)
        {
            return default;
        }

        if (typeof(T) == typeof(string))
        {
            // Ew, boxing.
            return (T)(object)configuration.Value;
        }

        T propertyValue;

        var targetType = typeof(T);

        if (targetType.IsGenericType)
        {
            targetType = targetType.GenericTypeArguments[0];
        }

        if (targetType.IsEnum)
        {
            propertyValue = (T)Enum.Parse(targetType, configuration.Value);
        }
        else
        {
            propertyValue = JsonConvert.DeserializeObject<T>(configuration.Value);
        }

        return propertyValue;
    }
}
