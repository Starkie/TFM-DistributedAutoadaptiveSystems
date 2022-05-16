namespace Monitoring.Service.Controllers;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Knowledge.Service.ApiClient.Api;
using Knowledge.Service.ApiClient.Client;
using Knowledge.Service.ApiClient.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Monitoring.Service.Diagnostics;

[ApiController]
[Route("[controller]")]
public class ServiceController : ControllerBase
{
    private readonly MonitoringServiceDiagnostics _diagnostics;

    private readonly IServiceApi _serviceApi;

    public ServiceController(MonitoringServiceDiagnostics diagnostics, IServiceApi serviceApi)
    {
        _diagnostics = diagnostics;
        _serviceApi = serviceApi;
    }

    /// <summary>
    ///    Gets a configuration property given its name.
    /// </summary>
    /// <param name="serviceName"> The name of the service whose configuration we are looking for. </param>
    /// <param name="configurationName"> The name of the configuration property to find. </param>
    /// <returns> An IActionResult with result of the query. </returns>
    /// <response code="200"> The configuration property was found. Returns the value of the property. </response>
    /// <response code="404"> The configuration property was not found. </response>
    /// <response code="400"> There was an error with the provided arguments. </response>
    [HttpGet("{serviceName}/configuration/{configurationName}")]
    [ProducesResponseType(typeof(ConfigurationDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync([FromRoute]string serviceName, [FromRoute]string configurationName)
    {
        if (string.IsNullOrEmpty(serviceName)
            || string.IsNullOrEmpty(configurationName))
        {
            return BadRequest();
        }

        using var activity = _diagnostics.LogGetServiceConfiguration(serviceName, configurationName);

        ConfigurationDTO configuration = null;

        try
        {
            configuration = await _serviceApi.ServiceServiceNameConfigurationConfigurationNameGetAsync(serviceName, configurationName);
        }
        catch (ApiException exception)
        {
            if (exception.ErrorCode != StatusCodes.Status404NotFound)
            {
                throw;
            }
        }

        if (configuration is null)
        {
            _diagnostics.LogConfigurationNotFound(serviceName, configurationName);

            return NotFound();
        }

        _diagnostics.LogConfigurationFound(serviceName, configurationName, configuration);

        return Ok(configuration);
    }

    /// <summary>
    ///    Sets the values for the given configuration properties. If a given property
    ///     does not exist, it will be created.
    /// </summary>
    /// <param name="serviceName"> The name of the service. </param>
    /// <param name="setPropertiesDtos"> The collection of properties to set. </param>
    /// <returns> An IActionResult with result of the command. </returns>
    /// <response code="204"> The properties were created or updated successfully. </response>
    /// <response code="400"> There was an error with the provided arguments. </response>
    [HttpPut("{serviceName}/configuration")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SetPropertyAsync([FromRoute]string serviceName, [FromBody]ICollection<SetPropertyDTO> setPropertiesDtos)
    {
        if (string.IsNullOrEmpty(serviceName)
            || setPropertiesDtos.Any(p => string.IsNullOrEmpty(p.Name) || string.IsNullOrEmpty(p.Value)))
        {
            return BadRequest();
        }

        // _diagnostics.LogReportedMeasurement(propertyDto.Property.Key, propertyDto);

        await _serviceApi.ServiceServiceNameConfigurationPutAsync(
            serviceName,
            setPropertiesDtos.ToList(),
            CancellationToken.None);

        return NoContent();
    }
}
