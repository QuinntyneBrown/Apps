using Goals.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Goals.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GoalsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<GoalsController> _logger;

    public GoalsController(IMediator mediator, ILogger<GoalsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GoalDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GoalDto>>> GetGoals(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all goals");
        var result = await _mediator.Send(new GetGoalsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GoalDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GoalDto>> GetGoalById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting goal {GoalId}", id);
        var result = await _mediator.Send(new GetGoalByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(GoalDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<GoalDto>> CreateGoal(
        [FromBody] CreateGoalCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating goal for user {UserId}", command.UserId);
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetGoalById), new { id = result.GoalId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(GoalDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GoalDto>> UpdateGoal(
        Guid id,
        [FromBody] UpdateGoalCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.GoalId) return BadRequest("ID mismatch");
        _logger.LogInformation("Updating goal {GoalId}", id);
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteGoal(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting goal {GoalId}", id);
        var result = await _mediator.Send(new DeleteGoalCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
