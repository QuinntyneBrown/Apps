using PerformanceReviewPrepTool.Api.Features.Achievements;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PerformanceReviewPrepTool.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AchievementsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AchievementsController> _logger;

    public AchievementsController(IMediator mediator, ILogger<AchievementsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AchievementDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AchievementDto>>> GetAchievements(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? reviewPeriodId,
        [FromQuery] bool? isKeyAchievement,
        [FromQuery] string? category)
    {
        _logger.LogInformation("Getting achievements for user {UserId}", userId);

        var result = await _mediator.Send(new GetAchievementsQuery
        {
            UserId = userId,
            ReviewPeriodId = reviewPeriodId,
            IsKeyAchievement = isKeyAchievement,
            Category = category,
        });

        return Ok(result);
    }

    [HttpGet("{achievementId:guid}")]
    [ProducesResponseType(typeof(AchievementDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AchievementDto>> GetAchievementById(Guid achievementId)
    {
        _logger.LogInformation("Getting achievement {AchievementId}", achievementId);

        var result = await _mediator.Send(new GetAchievementByIdQuery { AchievementId = achievementId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(AchievementDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AchievementDto>> CreateAchievement([FromBody] CreateAchievementCommand command)
    {
        _logger.LogInformation("Creating achievement for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/achievements/{result.AchievementId}", result);
    }

    [HttpPut("{achievementId:guid}")]
    [ProducesResponseType(typeof(AchievementDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AchievementDto>> UpdateAchievement(Guid achievementId, [FromBody] UpdateAchievementCommand command)
    {
        if (achievementId != command.AchievementId)
        {
            return BadRequest("Achievement ID mismatch");
        }

        _logger.LogInformation("Updating achievement {AchievementId}", achievementId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{achievementId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAchievement(Guid achievementId)
    {
        _logger.LogInformation("Deleting achievement {AchievementId}", achievementId);

        var result = await _mediator.Send(new DeleteAchievementCommand { AchievementId = achievementId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
