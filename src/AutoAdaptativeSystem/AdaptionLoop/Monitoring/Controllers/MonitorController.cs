namespace Monitoring.Service.Controllers;

using System;
using System.Threading;
using System.Threading.Tasks;
using Knowledge.Service.ApiClient.Api;
using Knowledge.Service.ApiClient.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Monitoring.Service.Diagnostics;
using Monitoring.Service.DTOS;

[ApiController]
[Route("[controller]")]
public class MonitorController : ControllerBase
{
    private readonly MonitoringServiceDiagnostics _diagnostics;

    private readonly IPropertyApi _propertyApi;

    public MonitorController(MonitoringServiceDiagnostics diagnostics, IPropertyApi propertyApi)
    {
        _diagnostics = diagnostics;
        _propertyApi = propertyApi;
    }

    /// <summary>
    ///     Registers a measurement from a monitor.
    /// </summary>
    /// <param name="monitorId"> The identifier of the monitor reporting the measurement. </param>
    /// <param name="measurementDto"> The DTO containing information of the measurement. </param>
    /// <returns> The address, if found. Otherwise, returns null. </returns>
    /// <response code="200"> Indicates that the measurement was reported successfully. </response>
    /// <response code="400"> Indicates that there was an error with the request. </response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("{monitorId}/measurement")]
    public async Task<IActionResult> ReportMeasurementAsync([FromRoute]Guid monitorId, [FromBody]MeasurementDTO measurementDto, CancellationToken cancellationToken)
    {
        if (measurementDto == null || string.IsNullOrEmpty(measurementDto.Property?.Key))
        {
            return this.BadRequest();
        }

        using var activity = _diagnostics.LogReportedMeasurement(measurementDto.Property.Key, measurementDto);

        await _propertyApi.PropertyPutAsync(new()
        {
            new SetPropertyDTO(measurementDto.Property.Key, measurementDto.Property.Value)
        });

        return this.Ok();
    }
}
