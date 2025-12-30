using ProfessionalReadingList.Api.Features.Resources;
using ProfessionalReadingList.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProfessionalReadingList.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResourcesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ResourcesController> _logger;

    public ResourcesController(IMediator mediator, ILogger<ResourcesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ResourceDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ResourceDto>>> GetResources(
        [FromQuery] Guid? userId,
        [FromQuery] ResourceType? resourceType,
        [FromQuery] string? topic)
    {
        _logger.LogInformation("Getting resources for user {UserId}", userId);

        var result = await _mediator.Send(new GetResourcesQuery
        {
            UserId = userId,
            ResourceType = resourceType,
            Topic = topic,
        });

        return Ok(result);
    }

    [HttpGet("{resourceId:guid}")]
    [ProducesResponseType(typeof(ResourceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ResourceDto>> GetResourceById(Guid resourceId)
    {
        _logger.LogInformation("Getting resource {ResourceId}", resourceId);

        var result = await _mediator.Send(new GetResourceByIdQuery { ResourceId = resourceId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResourceDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ResourceDto>> CreateResource([FromBody] CreateResourceCommand command)
    {
        _logger.LogInformation("Creating resource for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/resources/{result.ResourceId}", result);
    }

    [HttpPut("{resourceId:guid}")]
    [ProducesResponseType(typeof(ResourceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ResourceDto>> UpdateResource(Guid resourceId, [FromBody] UpdateResourceCommand command)
    {
        if (resourceId != command.ResourceId)
        {
            return BadRequest("Resource ID mismatch");
        }

        _logger.LogInformation("Updating resource {ResourceId}", resourceId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{resourceId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteResource(Guid resourceId)
    {
        _logger.LogInformation("Deleting resource {ResourceId}", resourceId);

        var result = await _mediator.Send(new DeleteResourceCommand { ResourceId = resourceId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
