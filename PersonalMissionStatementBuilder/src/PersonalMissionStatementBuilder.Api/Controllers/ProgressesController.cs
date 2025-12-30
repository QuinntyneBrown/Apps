using PersonalMissionStatementBuilder.Api.Features.Progresses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PersonalMissionStatementBuilder.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProgressesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProgressesController> _logger;

    public ProgressesController(IMediator mediator, ILogger<ProgressesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProgressDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProgressDto>>> GetProgresses(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? goalId)
    {
        _logger.LogInformation("Getting progresses for user {UserId}", userId);

        var result = await _mediator.Send(new GetProgressesQuery
        {
            UserId = userId,
            GoalId = goalId,
        });

        return Ok(result);
    }

    [HttpGet("{progressId:guid}")]
    [ProducesResponseType(typeof(ProgressDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProgressDto>> GetProgressById(Guid progressId)
    {
        _logger.LogInformation("Getting progress {ProgressId}", progressId);

        var result = await _mediator.Send(new GetProgressByIdQuery { ProgressId = progressId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProgressDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProgressDto>> CreateProgress([FromBody] CreateProgressCommand command)
    {
        _logger.LogInformation("Creating progress for goal {GoalId}", command.GoalId);

        var result = await _mediator.Send(command);

        return Created($"/api/progresses/{result.ProgressId}", result);
    }

    [HttpPut("{progressId:guid}")]
    [ProducesResponseType(typeof(ProgressDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProgressDto>> UpdateProgress(Guid progressId, [FromBody] UpdateProgressCommand command)
    {
        if (progressId != command.ProgressId)
        {
            return BadRequest("Progress ID mismatch");
        }

        _logger.LogInformation("Updating progress {ProgressId}", progressId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{progressId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProgress(Guid progressId)
    {
        _logger.LogInformation("Deleting progress {ProgressId}", progressId);

        var result = await _mediator.Send(new DeleteProgressCommand { ProgressId = progressId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
