namespace KnowledgeService.Controllers
{
    using System;
    using System.Collections.Concurrent;
    using KnowledgeService.Diagnostics;
    using KnowledgeService.DTOs;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly KnowledgeServiceDiagnostics _diagnostics;

        private static ConcurrentDictionary<string, PropertyDTO> properties = new();

        public PropertyController(KnowledgeServiceDiagnostics diagnostics)
        {
            _diagnostics = diagnostics;
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
        ///    Sets value of a given property. If the property does not exist, it will be created.
        /// </summary>
        /// <param name="propertyName"> The name of the property to set. </param>
        /// <param name="setPropertyDto"> The DTO containing the value to set. </param>
        /// <returns> An IActionResult with result of the command. </returns>
        /// <response code="204"> The property was updated or created successfully. </response>
        /// <response code="400"> There was an error with the provided arguments. </response>
        [HttpPut("{propertyName}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SetProperty([FromRoute]string propertyName, [FromBody]SetPropertyDTO setPropertyDto)
        {
            if (string.IsNullOrEmpty(propertyName) || string.IsNullOrEmpty(setPropertyDto?.Value))
            {
                return BadRequest();
            }

            using var activity = _diagnostics.LogSetProperty(propertyName, setPropertyDto);

            PropertyDTO newValue = new()
            {
                Value = setPropertyDto.Value,
                LastModification = DateTime.UtcNow,
            };

            properties.AddOrUpdate(propertyName, newValue, (_, _) => newValue);

            return NoContent();
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
}
