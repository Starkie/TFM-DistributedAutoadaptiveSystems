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
public sealed class ServiceController : ControllerBase
{
    private readonly KnowledgeServiceDiagnostics _diagnostics;

    private readonly ConfigurationChangeRequestIntegrationEventPublisher _configurationChangeRequestIntegrationEventPublisher;

    private static ConcurrentDictionary<string, ConcurrentDictionary<string, ConfigurationDTO>> configuration = new();

    public ServiceController(
        KnowledgeServiceDiagnostics diagnostics,
        ConfigurationChangeRequestIntegrationEventPublisher configurationChangeRequestIntegrationEventPublisher)
    {
        _diagnostics = diagnostics;
        _configurationChangeRequestIntegrationEventPublisher = configurationChangeRequestIntegrationEventPublisher;
    }

    /// <summary>
    ///    Gets a configuration property given its name.
    /// </summary>
    /// <param name="serviceName"> The name of the service whose configuration we are looking for. </param>
    /// <param name="configurationName"> The name of the configuration property to find. </param>
    /// <returns> An IActionResult with result of the query. </returns>
    /// <response code="200"> The configuration property was found. Returns the value of the property. </response>
    /// <response code="400"> There was an error with the provided arguments. </response>
    /// <response code="404"> The configuration property was not found. </response>
    [HttpGet("{serviceName}/configuration/{configurationName}")]
    [ProducesResponseType(typeof(ConfigurationDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetConfigurationProperty([FromRoute]string serviceName, [FromRoute]string configurationName)
    {
        if (string.IsNullOrEmpty(configurationName))
        {
            return BadRequest();
        }

        using var activity = _diagnostics.LogGetConfiguration(configurationName);

        var configurationDto = GetConfiguration(serviceName, configurationName);

        if (configurationDto is null)
        {
            _diagnostics.LogConfigurationNotFound(configurationName);

            return NotFound();
        }

        _diagnostics.LogConfigurationFound(configurationName, configurationDto);

        return Ok(configurationDto);
    }

    private static ConfigurationDTO GetConfiguration(string serviceName, string configurationName)
    {
        bool foundConfiguration = configuration.TryGetValue(serviceName, out ConcurrentDictionary<string, ConfigurationDTO> serviceConfigurations);

        if (!foundConfiguration)
        {
            return null;
        }

        serviceConfigurations.TryGetValue(configurationName, out ConfigurationDTO configurationDto);

        return configurationDto;
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
