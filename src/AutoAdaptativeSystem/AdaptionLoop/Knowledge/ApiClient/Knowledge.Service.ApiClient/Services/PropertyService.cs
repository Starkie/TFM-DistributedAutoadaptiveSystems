namespace Knowledge.Service.ApiClient.Services;

using System.Threading;
using System.Threading.Tasks;
using Knowledge.Service.ApiClient.Api;
using Knowledge.Service.ApiClient.Client;
using Knowledge.Service.ApiClient.Model;
using Newtonsoft.Json;


public class PropertyService : IPropertyService
{
    private readonly IPropertyApi _propertyApi;

    public PropertyService(IPropertyApi propertyApi)
    {
        _propertyApi = propertyApi;
    }

    public async Task<T> GetProperty<T>(string propertyName, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return default;
        }

        T propertyValue = default;

        try
        {
            PropertyDTO property = await _propertyApi.PropertyPropertyNameGetAsync(propertyName, cancellationToken);

            propertyValue = JsonConvert.DeserializeObject<T>(property.Value);
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
