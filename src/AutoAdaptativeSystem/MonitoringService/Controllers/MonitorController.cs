namespace MonitoringService.Controllers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeService.ApiClient.Api;
using KnowledgeService.ApiClient.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using MonitoringService.DTOS;

[ApiController]
[Route("[controller]")]
public class MonitorController : ControllerBase
{
    private readonly ILogger<MonitorController> _logger;

    private readonly IPropertyApi _propertyApi;

    public MonitorController(ILogger<MonitorController> logger, IPropertyApi propertyApi)
    {
        _logger = logger;
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
    public async Task<IActionResult> ReportMeasurementAsync([FromRoute]Guid monitorId, [FromBody]MeasurementDTO measurementDto)
    {
        if (measurementDto == null || string.IsNullOrEmpty(measurementDto.Property?.Key))
        {
            return this.BadRequest();
        }

        this._logger.LogInformation(
            "[{methodName}] - Reported Property Change: MonitorId = {monitorId}, ProbeId = {probeId}, Property: Key {propertyName} Value: {propertyValue}",
            nameof(ReportMeasurementAsync),
            monitorId,
            measurementDto.ProbeId,
            measurementDto.Property.Key,
            measurementDto.Property.Value);

        await _propertyApi.PropertyPropertyNamePutAsync(
            measurementDto.Property.Key,
            new SetPropertyDTO(measurementDto.Property.Value));

        return this.Ok();
    }
}
