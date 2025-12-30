using TimeAuditTracker.Api.Features.TimeBlocks;
using TimeAuditTracker.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TimeAuditTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimeBlocksController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TimeBlocksController> _logger;

    public TimeBlocksController(IMediator mediator, ILogger<TimeBlocksController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TimeBlockDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TimeBlockDto>>> GetTimeBlocks(
        [FromQuery] Guid? userId,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] ActivityCategory? category,
        [FromQuery] bool? isProductive,
        [FromQuery] bool? isActive)
    {
        _logger.LogInformation("Getting time blocks for user {UserId}", userId);

        var result = await _mediator.Send(new GetTimeBlocksQuery
        {
            UserId = userId,
            StartDate = startDate,
            EndDate = endDate,
            Category = category,
            IsProductive = isProductive,
            IsActive = isActive,
        });

        return Ok(result);
    }

    [HttpGet("{timeBlockId:guid}")]
    [ProducesResponseType(typeof(TimeBlockDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TimeBlockDto>> GetTimeBlockById(Guid timeBlockId)
    {
        _logger.LogInformation("Getting time block {TimeBlockId}", timeBlockId);

        var result = await _mediator.Send(new GetTimeBlockByIdQuery { TimeBlockId = timeBlockId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(TimeBlockDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TimeBlockDto>> CreateTimeBlock([FromBody] CreateTimeBlockCommand command)
    {
        _logger.LogInformation("Creating time block for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/timeblocks/{result.TimeBlockId}", result);
    }

    [HttpPut("{timeBlockId:guid}")]
    [ProducesResponseType(typeof(TimeBlockDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TimeBlockDto>> UpdateTimeBlock(Guid timeBlockId, [FromBody] UpdateTimeBlockCommand command)
    {
        if (timeBlockId != command.TimeBlockId)
        {
            return BadRequest("Time block ID mismatch");
        }

        _logger.LogInformation("Updating time block {TimeBlockId}", timeBlockId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{timeBlockId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTimeBlock(Guid timeBlockId)
    {
        _logger.LogInformation("Deleting time block {TimeBlockId}", timeBlockId);

        var result = await _mediator.Send(new DeleteTimeBlockCommand { TimeBlockId = timeBlockId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
