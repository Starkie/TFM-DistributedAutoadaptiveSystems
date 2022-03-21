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
}
