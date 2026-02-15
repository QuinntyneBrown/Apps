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
    public async Task<ActionResult<IEnumerable<GoalDto>>> GetGoals()
    {
        _logger.LogInformation("Getting all goals");
        var result = await _mediator.Send(new GetGoalsQuery());
        return Ok(result);
    }

    [HttpGet("{goalId:guid}")]
    [ProducesResponseType(typeof(GoalDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GoalDto>> GetGoalById(Guid goalId)
    {
        _logger.LogInformation("Getting goal {GoalId}", goalId);
        var result = await _mediator.Send(new GetGoalByIdQuery { GoalId = goalId });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(GoalDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GoalDto>> CreateGoal([FromBody] CreateGoalCommand command)
    {
        _logger.LogInformation("Creating goal: {Name}", command.Name);
        var result = await _mediator.Send(command);
        return Created($"/api/goals/{result.GoalId}", result);
    }

    [HttpPut("{goalId:guid}")]
    [ProducesResponseType(typeof(GoalDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GoalDto>> UpdateGoal(Guid goalId, [FromBody] UpdateGoalCommand command)
    {
        if (goalId != command.GoalId) return BadRequest("Goal ID mismatch");
        _logger.LogInformation("Updating goal {GoalId}", goalId);
        var result = await _mediator.Send(command);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{goalId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteGoal(Guid goalId)
    {
        _logger.LogInformation("Deleting goal {GoalId}", goalId);
        var result = await _mediator.Send(new DeleteGoalCommand { GoalId = goalId });
        if (!result) return NotFound();
        return NoContent();
    }
}
