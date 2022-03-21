namespace Knowledge.Service.Controllers;

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Knowledge.Contracts.IntegrationEvents;
using Knowledge.Contracts.IntegrationEvents.Configuration;
using Knowledge.Service.Controllers.IntegrationEvents;
using Knowledge.Service.Diagnostics;
using Knowledge.Service.DTOs;
using Knowledge.Service.DTOs.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public sealed class ConfigurationController : ControllerBase
{
    private readonly KnowledgeServiceDiagnostics _diagnostics;

    private readonly ConfigurationChangeRequestIntegrationEventPublisher _configurationChangeRequestIntegrationEventPublisher;

    private static ConcurrentDictionary<string, ConfigurationDTO> configuration = new();

    public ConfigurationController(
        KnowledgeServiceDiagnostics diagnostics,
        ConfigurationChangeRequestIntegrationEventPublisher configurationChangeRequestIntegrationEventPublisher)
    {
        _diagnostics = diagnostics;
        _configurationChangeRequestIntegrationEventPublisher = configurationChangeRequestIntegrationEventPublisher;
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
    [ProducesResponseType(typeof(PropertyDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetConfigurationProperty([FromRoute]string configurationName)
    {
        if (string.IsNullOrEmpty(configurationName))
        {
            return BadRequest();
        }

        using var activity = _diagnostics.LogGetConfiguration(configurationName);

        bool foundProperty = configuration.TryGetValue(configurationName, out ConfigurationDTO configurationDto);

        if (!foundProperty)
        {
            _diagnostics.LogConfigurationNotFound(configurationName);

            return NotFound();
        }

        _diagnostics.LogConfigurationFound(configurationName, configurationDto);

        return Ok(configurationDto);
    }

    /// <summary>
    ///    Requests a change in a configuration key of a given service. For example,
    ///    could be used to set the target temperature of an AC system.
    /// </summary>
    /// <param name="configurationChangeRequestDto"> The DTO containing the request to change the property. </param>
    /// <returns> An IActionResult with result of the command. </returns>
    /// <response code="204"> The configuration property was updated or created successfully. </response>
    /// <response code="400"> There was an error with the provided arguments. </response>
    [HttpPost("request-change")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RequestConfigurationChangeAsync([FromBody]ConfigurationChangeRequestDTO configurationChangeRequestDto)
    {
        var integrationEvent = new ConfigurationChangeRequestIntegrationEvent(
            configurationChangeRequestDto.RequestedChanges.Select(cr =>  new ChangeRequest(cr.ServiceName, cr.PropertyName, cr.PropertyNewValue)),
            configurationChangeRequestDto.Symptoms.Select(s => new Symptom(s.Name, s.Value)),
            configurationChangeRequestDto.Timestamp
        );

        await _configurationChangeRequestIntegrationEventPublisher.PublishAsync(integrationEvent);

        return NoContent();
    }


    // /// <summary>
    // ///    Sets value of a given configuration property. If the property does not exist, it will be created.
    // /// </summary>
    // /// <param name="configurationName"> The name of the property to set. </param>
    // /// <param name="setPropertyDto"> The DTO containing the value to set. </param>
    // /// <returns> An IActionResult with result of the command. </returns>
    // /// <response code="204"> The property was updated or created successfully. </response>
    // /// <response code="400"> There was an error with the provided arguments. </response>
    // [HttpPut("{configurationName}")]
    // [ProducesResponseType(StatusCodes.Status204NoContent)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public async Task<IActionResult> SetPropertyAsync([FromRoute]string configurationName, [FromBody]SetPropertyDTO setPropertyDto)
    // {
    //     if (string.IsNullOrEmpty(configurationName) || string.IsNullOrEmpty(setPropertyDto?.Value))
    //     {
    //         return BadRequest();
    //     }
    //
    //     using var activity = _diagnostics.LogSetProperty(configurationName, setPropertyDto);
    //
    //     PropertyDTO newValue = new()
    //     {
    //         Value = setPropertyDto.Value,
    //         LastModification = DateTime.UtcNow,
    //     };
    //
    //     configuration.AddOrUpdate(configurationName, newValue, (_, _) => newValue);
    //
    //     return NoContent();
    // }
}
