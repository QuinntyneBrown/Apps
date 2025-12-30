using LifeAdminDashboard.Api.Features.Deadlines;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LifeAdminDashboard.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeadlinesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<DeadlinesController> _logger;

    public DeadlinesController(IMediator mediator, ILogger<DeadlinesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DeadlineDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<DeadlineDto>>> GetDeadlines(
        [FromQuery] Guid? userId,
        [FromQuery] string? category,
        [FromQuery] bool? isCompleted,
        [FromQuery] bool? isOverdue,
        [FromQuery] bool? requiresReminder)
    {
        _logger.LogInformation("Getting deadlines for user {UserId}", userId);

        var result = await _mediator.Send(new GetDeadlinesQuery
        {
            UserId = userId,
            Category = category,
            IsCompleted = isCompleted,
            IsOverdue = isOverdue,
            RequiresReminder = requiresReminder,
        });

        return Ok(result);
    }

    [HttpGet("{deadlineId:guid}")]
    [ProducesResponseType(typeof(DeadlineDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeadlineDto>> GetDeadlineById(Guid deadlineId)
    {
        _logger.LogInformation("Getting deadline {DeadlineId}", deadlineId);

        var result = await _mediator.Send(new GetDeadlineByIdQuery { DeadlineId = deadlineId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(DeadlineDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DeadlineDto>> CreateDeadline([FromBody] CreateDeadlineCommand command)
    {
        _logger.LogInformation("Creating deadline for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/deadlines/{result.DeadlineId}", result);
    }

    [HttpPut("{deadlineId:guid}")]
    [ProducesResponseType(typeof(DeadlineDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeadlineDto>> UpdateDeadline(Guid deadlineId, [FromBody] UpdateDeadlineCommand command)
    {
        if (deadlineId != command.DeadlineId)
        {
            return BadRequest("Deadline ID mismatch");
        }

        _logger.LogInformation("Updating deadline {DeadlineId}", deadlineId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{deadlineId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDeadline(Guid deadlineId)
    {
        _logger.LogInformation("Deleting deadline {DeadlineId}", deadlineId);

        var result = await _mediator.Send(new DeleteDeadlineCommand { DeadlineId = deadlineId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
