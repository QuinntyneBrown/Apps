using Milestones.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Milestones.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MilestonesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<MilestonesController> _logger;

    public MilestonesController(IMediator mediator, ILogger<MilestonesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MilestoneDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MilestoneDto>>> GetMilestones([FromQuery] Guid? goalId = null)
    {
        _logger.LogInformation("Getting milestones");
        var result = await _mediator.Send(new GetMilestonesQuery { GoalId = goalId });
        return Ok(result);
    }

    [HttpGet("{milestoneId:guid}")]
    [ProducesResponseType(typeof(MilestoneDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MilestoneDto>> GetMilestoneById(Guid milestoneId)
    {
        _logger.LogInformation("Getting milestone {MilestoneId}", milestoneId);
        var result = await _mediator.Send(new GetMilestoneByIdQuery { MilestoneId = milestoneId });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(MilestoneDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MilestoneDto>> CreateMilestone([FromBody] CreateMilestoneCommand command)
    {
        _logger.LogInformation("Creating milestone: {Name}", command.Name);
        var result = await _mediator.Send(command);
        return Created($"/api/milestones/{result.MilestoneId}", result);
    }

    [HttpPut("{milestoneId:guid}")]
    [ProducesResponseType(typeof(MilestoneDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MilestoneDto>> UpdateMilestone(Guid milestoneId, [FromBody] UpdateMilestoneCommand command)
    {
        if (milestoneId != command.MilestoneId) return BadRequest("Milestone ID mismatch");
        _logger.LogInformation("Updating milestone {MilestoneId}", milestoneId);
        var result = await _mediator.Send(command);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{milestoneId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMilestone(Guid milestoneId)
    {
        _logger.LogInformation("Deleting milestone {MilestoneId}", milestoneId);
        var result = await _mediator.Send(new DeleteMilestoneCommand { MilestoneId = milestoneId });
        if (!result) return NotFound();
        return NoContent();
    }
}
