using FamilyCalendarEventPlanner.Api.Features.Conflicts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FamilyCalendarEventPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConflictsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ConflictsController> _logger;

    public ConflictsController(IMediator mediator, ILogger<ConflictsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ScheduleConflictDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ScheduleConflictDto>>> GetConflicts(
        [FromQuery] Guid? memberId,
        [FromQuery] bool? isResolved)
    {
        _logger.LogInformation(
            "Getting conflicts for member {MemberId}, isResolved: {IsResolved}",
            memberId,
            isResolved);

        var result = await _mediator.Send(new GetConflictsQuery
        {
            MemberId = memberId,
            IsResolved = isResolved,
        });

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ScheduleConflictDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ScheduleConflictDto>> CreateConflict([FromBody] CreateConflictCommand command)
    {
        _logger.LogInformation(
            "Creating schedule conflict with {EventCount} events",
            command.ConflictingEventIds.Count);

        var result = await _mediator.Send(command);

        return Created(string.Empty, result);
    }

    [HttpPost("{conflictId:guid}/resolve")]
    [ProducesResponseType(typeof(ScheduleConflictDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ScheduleConflictDto>> ResolveConflict(Guid conflictId)
    {
        _logger.LogInformation("Resolving conflict {ConflictId}", conflictId);

        var result = await _mediator.Send(new ResolveConflictCommand { ConflictId = conflictId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
