namespace Knowledge.Service.Controllers;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Knowledge.Contracts.IntegrationEvents;
using Knowledge.Service.Controllers.IntegrationEvents;
using Knowledge.Service.Diagnostics;
using Knowledge.Service.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class PropertyController : ControllerBase
{
    private readonly KnowledgeServiceDiagnostics _diagnostics;

    private readonly IMediator _mediator;

    private static ConcurrentDictionary<string, PropertyDTO> properties = new();

    public PropertyController(KnowledgeServiceDiagnostics diagnostics, IMediator mediator)
    {
        _diagnostics = diagnostics;
        _mediator = mediator;
    }

    /// <summary>
    ///    Gets all the property registered in the knowledge.
    /// </summary>
    /// <returns> An IActionResult with result of the query. </returns>
    /// <response code="200"> The collection of registered properties. </response>
    [HttpGet]
    [ProducesResponseType(typeof(IDictionary<string, PropertyDTO>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        return Ok(properties);
    }

    /// <summary>
    ///    Gets a property given its name.
    /// </summary>
    /// <param name="propertyName"> The name of the property to find. </param>
    /// <returns> An IActionResult with result of the query. </returns>
    /// <response code="200"> The property was found. Returns the value of the property. </response>
    /// <response code="404"> The property was not found. </response>
    /// <response code="400"> There was an error with the provided arguments. </response>
    [HttpGet("{propertyName}")]
    [ProducesResponseType(typeof(PropertyDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetProperty([FromRoute]string propertyName)
    {
        if (string.IsNullOrEmpty(propertyName))
        {
            return BadRequest();
        }

        using var activity = _diagnostics.LogGetProperty(propertyName);

        bool foundProperty = properties.TryGetValue(propertyName, out PropertyDTO property);

        if (!foundProperty)
        {
            _diagnostics.LogPropertyNotFound(propertyName);

            return NotFound();
        }

        _diagnostics.LogPropertyFound(propertyName, property);

        return Ok(property);
    }

    /// <summary>
    ///    Sets the value of the given properties. If a given property does not exist, it will be created.
    /// </summary>
    /// <param name="setPropertiesDtos"> The collection of properties to set.. </param>
    /// <returns> An IActionResult with result of the command. </returns>
    /// <response code="204"> The property was updated or created successfully. </response>
    /// <response code="400"> There was an error with the provided arguments. </response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SetPropertyAsync([FromBody]ICollection<SetPropertyDTO> setPropertiesDtos)
    {
        using var activity = _diagnostics.LogSetProperties(setPropertiesDtos.Count);

        foreach (var propertyDto in setPropertiesDtos)
        {
            if (string.IsNullOrEmpty(propertyDto.Name) || string.IsNullOrEmpty(propertyDto.Value))
            {
                return BadRequest();
            }

            await SetProperty(propertyDto);
        }

        return NoContent();
    }

    private async Task SetProperty(SetPropertyDTO propertyDto)
    {
        _diagnostics.LogSetProperty(propertyDto);

        PropertyDTO newValue = new()
        {
            Value = propertyDto.Value,
            LastModification = DateTime.UtcNow,
        };

        properties.AddOrUpdate(propertyDto.Name, newValue, (_, _) => newValue);

        // TODO: Investigar persistencia de mensajes:
        // https://stackoverflow.com/questions/6148381/rabbitmq-persistent-message-with-topic-exchange
        await _mediator.Publish(new PropertyChangedIntegrationEvent(propertyDto.Name));
    }

    /// <summary>
    ///    Deletes the value of a given property.
    /// </summary>
    /// <param name="propertyName"> The name of the property to delete. </param>
    /// <returns> An IActionResult with result of the command. </returns>
    /// <response code="204"> The property was deleted successfully. </response>
    /// <response code="400"> There was an error with the provided arguments. </response>
    /// <response code="404"> The property was not found. </response>
    [HttpDelete("{propertyName}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteProperty([FromRoute]string propertyName)
    {
        if (string.IsNullOrEmpty(propertyName))
        {
            return BadRequest();
        }

        using var activity = _diagnostics.LogDeleteProperty(propertyName);

        properties.TryRemove(propertyName, out var value);

        if (value is null)
        {
            _diagnostics.LogPropertyNotFound(propertyName);

            return NotFound();
        }

        return NoContent();
    }
}
