using Achievements.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Achievements.Api.Controllers;

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
    public async Task<ActionResult<IEnumerable<AchievementDto>>> GetAchievements(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all achievements");
        var result = await _mediator.Send(new GetAchievementsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AchievementDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AchievementDto>> GetAchievementById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting achievement {AchievementId}", id);
        var result = await _mediator.Send(new GetAchievementByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(AchievementDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<AchievementDto>> CreateAchievement(
        [FromBody] CreateAchievementCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating achievement for user {UserId}", command.UserId);
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetAchievementById), new { id = result.AchievementId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(AchievementDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AchievementDto>> UpdateAchievement(
        Guid id,
        [FromBody] UpdateAchievementCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.AchievementId) return BadRequest("ID mismatch");
        _logger.LogInformation("Updating achievement {AchievementId}", id);
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAchievement(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting achievement {AchievementId}", id);
        var result = await _mediator.Send(new DeleteAchievementCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
