namespace MonitoringService.Controllers;

using System.Threading.Tasks;
using KnowledgeService.ApiClient.Api;
using KnowledgeService.ApiClient.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonitoringService.Diagnostics;

[ApiController]
[Route("[controller]")]
public class PropertyController : ControllerBase
{
    private readonly MonitoringServiceDiagnostics _diagnostics;

    private readonly IPropertyApi _propertyApi;

    public PropertyController(MonitoringServiceDiagnostics diagnostics, IPropertyApi propertyApi)
    {
        _diagnostics = diagnostics;
        _propertyApi = propertyApi;
    }

    /// <summary>
    ///     Looks for the Knowledge property with the given name.
    /// </summary>
    /// <param name="propertyName">The name of the property to look for.</param>
    /// <returns> An IActionResult containing the result of the query </returns>
    /// <response code="200"> The property was found. Returns the property's value. </response>
    /// <response code="404"> No property with that name was found. </response>
    [HttpGet("{propertyName}")]
    [ProducesResponseType(typeof(PropertyDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync([FromRoute]string propertyName)
    {
        var activity = _diagnostics.LogGetProperty(propertyName);

        PropertyDTO property = await _propertyApi.PropertyPropertyNameGetAsync(propertyName);

        if (property is null)
        {
            _diagnostics.LogPropertyNotFound(propertyName);

            return NotFound();
        }

        _diagnostics.LogPropertyFound(propertyName, property);

        return Ok(property);
    }
}
