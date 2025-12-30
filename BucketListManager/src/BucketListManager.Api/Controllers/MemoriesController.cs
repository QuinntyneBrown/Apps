using BucketListManager.Api.Features.Memories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BucketListManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MemoriesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<MemoriesController> _logger;

    public MemoriesController(IMediator mediator, ILogger<MemoriesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MemoryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MemoryDto>>> GetMemories(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? bucketListItemId)
    {
        _logger.LogInformation("Getting memories for user {UserId}", userId);

        var result = await _mediator.Send(new GetMemoriesQuery
        {
            UserId = userId,
            BucketListItemId = bucketListItemId,
        });

        return Ok(result);
    }

    [HttpGet("{memoryId:guid}")]
    [ProducesResponseType(typeof(MemoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MemoryDto>> GetMemoryById(Guid memoryId)
    {
        _logger.LogInformation("Getting memory {MemoryId}", memoryId);

        var result = await _mediator.Send(new GetMemoryByIdQuery { MemoryId = memoryId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(MemoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MemoryDto>> CreateMemory([FromBody] CreateMemoryCommand command)
    {
        _logger.LogInformation("Creating memory for bucket list item {BucketListItemId}", command.BucketListItemId);

        var result = await _mediator.Send(command);

        return Created($"/api/memories/{result.MemoryId}", result);
    }

    [HttpPut("{memoryId:guid}")]
    [ProducesResponseType(typeof(MemoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MemoryDto>> UpdateMemory(
        Guid memoryId,
        [FromBody] UpdateMemoryCommand command)
    {
        if (memoryId != command.MemoryId)
        {
            return BadRequest("Memory ID mismatch");
        }

        _logger.LogInformation("Updating memory {MemoryId}", memoryId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{memoryId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMemory(Guid memoryId)
    {
        _logger.LogInformation("Deleting memory {MemoryId}", memoryId);

        var result = await _mediator.Send(new DeleteMemoryCommand { MemoryId = memoryId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
