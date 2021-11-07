namespace KnowledgeService.Controllers
{
    using System.Collections.Concurrent;
    using KnowledgeService.DTOs;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly ILogger<PropertyController> _logger;

        private readonly ConcurrentDictionary<string, string> _properties;

        public PropertyController(ILogger<PropertyController> logger)
        {
            _logger = logger;

            _properties = new ConcurrentDictionary<string, string>();
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
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetProperty([FromRoute]string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return BadRequest();
            }

            _logger.LogInformation("[{Action}] - Get property '{PropertyName}'.", nameof(GetProperty), propertyName);

            bool foundProperty = _properties.TryGetValue(propertyName, out string value);

            if (!foundProperty)
            {
                _logger.LogInformation("[{Action}] - Property '{PropertyName}' not found.", nameof(GetProperty), propertyName);

                return NotFound();
            }

            _logger.LogInformation("[{Action}] - Property '{PropertyName}' found. Value: '{PropertyValue}'", nameof(GetProperty), propertyName, value);

            return Ok(value);
        }

        /// <summary>
        ///    Sets value of a given property. If the property does not exist, it will be created.
        /// </summary>
        /// <param name="propertyName"> The name of the property to set. </param>
        /// <param name="value"> The DTO containing the value to set. </param>
        /// <returns> An IActionResult with result of the command. </returns>
        /// <response code="204"> The property was updated or created successfully. </response>
        /// <response code="400"> There was an error with the provided arguments. </response>
        [HttpPut("{propertyName}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SetProperty([FromRoute]string propertyName, [FromBody]SetPropertyDTO value)
        {
            if (string.IsNullOrEmpty(propertyName) || string.IsNullOrEmpty(value?.Value))
            {
                return BadRequest();
            }

            _logger.LogInformation("[{Action}] - Set property '{PropertyName}' with value '{PropertyValue}'.", nameof(SetProperty), propertyName, value.Value);

            _properties.AddOrUpdate(propertyName, value.Value, (_, _) => value.Value);

            return NoContent();
        }
    }
}