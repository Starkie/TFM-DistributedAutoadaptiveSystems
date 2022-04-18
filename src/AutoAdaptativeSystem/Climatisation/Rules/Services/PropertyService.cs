namespace Climatisation.Rules.Service.Services;

using System.Threading;
using System.Threading.Tasks;
using Analysis.Service.ApiClient.Api;
using Analysis.Service.ApiClient.Client;
using Analysis.Service.ApiClient.Model;
using Climatisation.Rules.Service.Diagnostics;
using Newtonsoft.Json;


public class PropertyService : IPropertyService
{
    private readonly ClimatisationRulesDiagnostics _diagnostics;

    private readonly IPropertyApi _propertyApi;

    public PropertyService(ClimatisationRulesDiagnostics diagnostics, IPropertyApi propertyApi)
    {
        _diagnostics = diagnostics;
        _propertyApi = propertyApi;
    }

    public async Task<T> GetProperty<T>(string propertyName, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return default;
        }

        T propertyValue = default;

        using var activity = _diagnostics.GetPropertyValue(propertyName);

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
