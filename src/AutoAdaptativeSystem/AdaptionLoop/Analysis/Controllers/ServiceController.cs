namespace Analysis.Service.Controllers;

using System.Threading.Tasks;
using Analysis.Service.Diagnostics;
using Knowledge.Service.ApiClient.Api;
using Knowledge.Service.ApiClient.Client;
using Knowledge.Service.ApiClient.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ServiceController : ControllerBase
{
    private readonly AnalysisServiceDiagnostics _diagnostics;

    private readonly IServiceApi _serviceApi;

    public ServiceController(AnalysisServiceDiagnostics diagnostics, IServiceApi serviceApi)
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
}
