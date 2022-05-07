namespace Analysis.Service.Controllers;

using System;
using System.Threading.Tasks;
using Analysis.Contracts.IntegrationEvents;
using Analysis.Service.Controllers.IntegrationEvents;
using Analysis.Service.Diagnostics;
using Analysis.Service.DTOs.Configuration;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
public class SystemController : ControllerBase
{
    private readonly AnalysisServiceDiagnostics _diagnostics;

    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    public SystemController(
        AnalysisServiceDiagnostics diagnostics,
        IMapper mapper,
        IMediator mediator)
    {
        _diagnostics = diagnostics;
        _mapper = mapper;
        _mediator = mediator;
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
    public async Task<IActionResult> RequestConfigurationChangeAsync([FromBody]SystemConfigurationChangeRequestDTO configurationChangeRequestDto)
    {
        if (configurationChangeRequestDto is null)
        {
            return BadRequest();
        }

        if (configurationChangeRequestDto.Symptoms.Count == 0)
        {
            return BadRequest("At least one symptom must be specified");
        }

        if (configurationChangeRequestDto.ServiceConfiguration.Count == 0)
        {
            return BadRequest("At least one service configuration must be specified.");
        }

        using var activity = _diagnostics.LogConfigurationChangeRequested(configurationChangeRequestDto);

        var integrationEvent = _mapper.Map<SystemConfigurationChangeRequestIntegrationEvent>(configurationChangeRequestDto);

        await _mediator.Publish(integrationEvent);

        return NoContent();
    }
}
