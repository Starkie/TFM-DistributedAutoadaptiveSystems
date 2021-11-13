namespace MonitoringModule.Controllers
{
    using System.Threading.Tasks;
    using KnowledgeService.ApiClient.Api;
    using KnowledgeService.ApiClient.Model;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly ILogger<PropertyController> _logger;

        private readonly IPropertyApi _propertyApi;

        public PropertyController(ILogger<PropertyController> logger, IPropertyApi propertyApi)
        {
            _logger = logger;
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
            _logger.LogInformation("[{ActionName}] - Requested property '{PropertyName}'", nameof(GetAsync), propertyName);
            
            PropertyDTO property = await _propertyApi.PropertyPropertyNameGetAsync(propertyName);

            if (property is null)
            {
                _logger.LogInformation("[{ActionName}] - Property '{PropertyName}' not found", nameof(GetAsync), propertyName);

                return NotFound();
            }

            _logger.LogInformation("[{ActionName}] - Property '{PropertyName}' found with value: '{PropertyValue}'", nameof(GetAsync), propertyName, property.Value);

            return Ok(property);
        }
    }
}