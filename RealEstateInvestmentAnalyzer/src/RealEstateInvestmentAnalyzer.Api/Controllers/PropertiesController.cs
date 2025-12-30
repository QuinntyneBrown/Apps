using RealEstateInvestmentAnalyzer.Api.Features.Properties;
using RealEstateInvestmentAnalyzer.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RealEstateInvestmentAnalyzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertiesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PropertiesController> _logger;

    public PropertiesController(IMediator mediator, ILogger<PropertiesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PropertyDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PropertyDto>>> GetProperties(
        [FromQuery] PropertyType? propertyType)
    {
        _logger.LogInformation("Getting properties");

        var result = await _mediator.Send(new GetPropertiesQuery
        {
            PropertyType = propertyType,
        });

        return Ok(result);
    }

    [HttpGet("{propertyId:guid}")]
    [ProducesResponseType(typeof(PropertyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PropertyDto>> GetPropertyById(Guid propertyId)
    {
        _logger.LogInformation("Getting property {PropertyId}", propertyId);

        var result = await _mediator.Send(new GetPropertyByIdQuery { PropertyId = propertyId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PropertyDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PropertyDto>> CreateProperty([FromBody] CreatePropertyCommand command)
    {
        _logger.LogInformation("Creating property");

        var result = await _mediator.Send(command);

        return Created($"/api/properties/{result.PropertyId}", result);
    }

    [HttpPut("{propertyId:guid}")]
    [ProducesResponseType(typeof(PropertyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PropertyDto>> UpdateProperty(Guid propertyId, [FromBody] UpdatePropertyCommand command)
    {
        if (propertyId != command.PropertyId)
        {
            return BadRequest("Property ID mismatch");
        }

        _logger.LogInformation("Updating property {PropertyId}", propertyId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{propertyId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProperty(Guid propertyId)
    {
        _logger.LogInformation("Deleting property {PropertyId}", propertyId);

        var result = await _mediator.Send(new DeletePropertyCommand { PropertyId = propertyId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
