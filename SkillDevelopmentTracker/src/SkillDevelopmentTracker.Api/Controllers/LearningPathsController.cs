using SkillDevelopmentTracker.Api.Features.LearningPaths;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SkillDevelopmentTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LearningPathsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<LearningPathsController> _logger;

    public LearningPathsController(IMediator mediator, ILogger<LearningPathsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<LearningPathDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LearningPathDto>>> GetLearningPaths(
        [FromQuery] Guid? userId,
        [FromQuery] bool? isCompleted)
    {
        _logger.LogInformation("Getting learning paths for user {UserId}", userId);

        var result = await _mediator.Send(new GetLearningPathsQuery
        {
            UserId = userId,
            IsCompleted = isCompleted,
        });

        return Ok(result);
    }

    [HttpGet("{learningPathId:guid}")]
    [ProducesResponseType(typeof(LearningPathDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LearningPathDto>> GetLearningPathById(Guid learningPathId)
    {
        _logger.LogInformation("Getting learning path {LearningPathId}", learningPathId);

        var result = await _mediator.Send(new GetLearningPathByIdQuery { LearningPathId = learningPathId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(LearningPathDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LearningPathDto>> CreateLearningPath([FromBody] CreateLearningPathCommand command)
    {
        _logger.LogInformation("Creating learning path for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/learningpaths/{result.LearningPathId}", result);
    }

    [HttpPut("{learningPathId:guid}")]
    [ProducesResponseType(typeof(LearningPathDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LearningPathDto>> UpdateLearningPath(Guid learningPathId, [FromBody] UpdateLearningPathCommand command)
    {
        if (learningPathId != command.LearningPathId)
        {
            return BadRequest("Learning path ID mismatch");
        }

        _logger.LogInformation("Updating learning path {LearningPathId}", learningPathId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{learningPathId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteLearningPath(Guid learningPathId)
    {
        _logger.LogInformation("Deleting learning path {LearningPathId}", learningPathId);

        var result = await _mediator.Send(new DeleteLearningPathCommand { LearningPathId = learningPathId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
