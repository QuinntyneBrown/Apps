using Activities.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Activities.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActivitiesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ActivitiesController> _logger;

    public ActivitiesController(IMediator mediator, ILogger<ActivitiesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ActivityDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ActivityDto>>> GetActivities(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all activities");
        var result = await _mediator.Send(new GetActivitiesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ActivityDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ActivityDto>> GetActivityById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting activity {ActivityId}", id);
        var result = await _mediator.Send(new GetActivityByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ActivityDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<ActivityDto>> CreateActivity(
        [FromBody] CreateActivityCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating activity {Name}", command.Name);
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetActivityById), new { id = result.ActivityId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ActivityDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ActivityDto>> UpdateActivity(
        Guid id,
        [FromBody] UpdateActivityCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.ActivityId) return BadRequest("ID mismatch");
        _logger.LogInformation("Updating activity {ActivityId}", id);
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteActivity(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting activity {ActivityId}", id);
        var result = await _mediator.Send(new DeleteActivityCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
