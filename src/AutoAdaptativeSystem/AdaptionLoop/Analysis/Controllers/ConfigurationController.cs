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
public class ConfigurationController : ControllerBase
{
    private readonly AnalysisServiceDiagnostics _diagnostics;

    private readonly IConfigurationApi _configurationApi;

    public ConfigurationController(AnalysisServiceDiagnostics diagnostics, IConfigurationApi configurationApi)
    {
        _diagnostics = diagnostics;
        _configurationApi = configurationApi;
    }

    /// <summary>
    ///    Requests a change in a configuration key of a given service. For example,
    ///    could be used to set the target temperature of an AC system.
    /// </summary>
    /// <param name="configurationChangeRequestDto"> The DTO containing the request to change the property. </param>
    /// <returns> An IActionResult with result of the command. </returns>
    /// <response code="204"> The configuration change request was submitted successfully. </response>
    /// <response code="400"> The request was formed incorrectly (null request DTO, invalid values..). </response>
    [HttpPost("request-change")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RequestChangeAsync([FromBody]ConfigurationChangeRequestDTO configurationChangeRequestDto)
    {
        if (configurationChangeRequestDto is null)
        {
            return BadRequest();
        }

        await _configurationApi.ConfigurationRequestChangePostAsync(configurationChangeRequestDto);

        return NoContent();
    }

    /// <summary>
    ///    Gets a configuration property given its name.
    /// </summary>
    /// <param name="configurationName"> The name of the configuration property to find. </param>
    /// <returns> An IActionResult with result of the query. </returns>
    /// <response code="200"> The configuration property was found. Returns the value of the property. </response>
    /// <response code="404"> The configuration property was not found. </response>
    /// <response code="400"> There was an error with the provided arguments. </response>
    [HttpGet("{configurationName}")]
    [ProducesResponseType(typeof(ConfigurationDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAsync([FromRoute]string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return BadRequest();
        }

        using var activity = _diagnostics.LogGetConfiguration(name);

        ConfigurationDTO configuration = null;

        try
        {
            configuration = await _configurationApi.ConfigurationConfigurationNameGetAsync(name);
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
            _diagnostics.LogConfigurationNotFound(name);

            return NotFound();
        }

        _diagnostics.LogConfigurationFound(name, configuration);

        return Ok(configuration);
    }
}
