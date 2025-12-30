using ResumeCareerAchievementTracker.Api.Features.Skills;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ResumeCareerAchievementTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SkillsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<SkillsController> _logger;

    public SkillsController(IMediator mediator, ILogger<SkillsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SkillDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SkillDto>>> GetSkills(
        [FromQuery] Guid? userId,
        [FromQuery] string? category,
        [FromQuery] bool? featuredOnly)
    {
        _logger.LogInformation("Getting skills for user {UserId}", userId);

        var result = await _mediator.Send(new GetSkillsQuery
        {
            UserId = userId,
            Category = category,
            FeaturedOnly = featuredOnly,
        });

        return Ok(result);
    }

    [HttpGet("{skillId:guid}")]
    [ProducesResponseType(typeof(SkillDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SkillDto>> GetSkillById(Guid skillId)
    {
        _logger.LogInformation("Getting skill {SkillId}", skillId);

        var result = await _mediator.Send(new GetSkillByIdQuery { SkillId = skillId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(SkillDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SkillDto>> CreateSkill([FromBody] CreateSkillCommand command)
    {
        _logger.LogInformation("Creating skill for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/skills/{result.SkillId}", result);
    }

    [HttpPut("{skillId:guid}")]
    [ProducesResponseType(typeof(SkillDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SkillDto>> UpdateSkill(Guid skillId, [FromBody] UpdateSkillCommand command)
    {
        if (skillId != command.SkillId)
        {
            return BadRequest("Skill ID mismatch");
        }

        _logger.LogInformation("Updating skill {SkillId}", skillId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{skillId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSkill(Guid skillId)
    {
        _logger.LogInformation("Deleting skill {SkillId}", skillId);

        var result = await _mediator.Send(new DeleteSkillCommand { SkillId = skillId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut("{skillId:guid}/featured")]
    [ProducesResponseType(typeof(SkillDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SkillDto>> ToggleFeatured(Guid skillId)
    {
        _logger.LogInformation("Toggling featured for skill {SkillId}", skillId);

        var result = await _mediator.Send(new ToggleSkillFeaturedCommand { SkillId = skillId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPut("{skillId:guid}/proficiency")]
    [ProducesResponseType(typeof(SkillDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SkillDto>> UpdateProficiency(Guid skillId, [FromBody] UpdateProficiencyCommand command)
    {
        if (skillId != command.SkillId)
        {
            return BadRequest("Skill ID mismatch");
        }

        _logger.LogInformation("Updating proficiency for skill {SkillId}", skillId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
