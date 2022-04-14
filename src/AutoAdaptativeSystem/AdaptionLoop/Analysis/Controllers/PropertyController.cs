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
public class PropertyController : ControllerBase
{
    private readonly AnalysisServiceDiagnostics _diagnostics;

    private readonly IPropertyApi _propertyApi;

    public PropertyController(AnalysisServiceDiagnostics diagnostics, IPropertyApi propertyApi)
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
        using var activity = _diagnostics.LogGetProperty(propertyName);

        PropertyDTO property = null;

        try
        {
            property = await _propertyApi.PropertyPropertyNameGetAsync(propertyName);
        }
        catch (ApiException exception)
        {
            if (exception.ErrorCode != StatusCodes.Status404NotFound)
            {
                throw;
            }
        }

        if (property is null)
        {
            _diagnostics.LogPropertyNotFound(propertyName);

            return NotFound();
        }

        _diagnostics.LogPropertyFound(propertyName, property);

        return Ok(property);
    }

    /// <summary>
    ///    Sets value of a given property. If the property does not exist, it will be created.
    /// </summary>
    /// <param name="propertyName"> The name of the property to set. </param>
    /// <param name="propertyDto"> The DTO containing the value to set. </param>
    /// <returns> An IActionResult with result of the command. </returns>
    /// <response code="204"> The property was updated or created successfully. </response>
    /// <response code="400"> There was an error with the provided arguments. </response>
    [HttpPut("{propertyName}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SetAsync([FromRoute]string propertyName, [FromBody]PropertyDTO propertyDto)
    {
        if (propertyDto == null || string.IsNullOrEmpty(propertyName))
        {
            return this.BadRequest();
        }

        // _diagnostics.LogReportedMeasurement(propertyDto.Property.Key, propertyDto);

        await _propertyApi.PropertyPropertyNamePutAsync(
            propertyName,
            new SetPropertyDTO(propertyDto.Value));

        return this.CreatedAtAction("Get", propertyName);
    }
}
