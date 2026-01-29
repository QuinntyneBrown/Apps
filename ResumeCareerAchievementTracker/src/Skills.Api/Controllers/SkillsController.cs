using Skills.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Skills.Api.Controllers;

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
    public async Task<ActionResult<IEnumerable<SkillDto>>> GetSkills(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all skills");
        var result = await _mediator.Send(new GetSkillsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SkillDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SkillDto>> GetSkillById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting skill {SkillId}", id);
        var result = await _mediator.Send(new GetSkillByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(SkillDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<SkillDto>> CreateSkill(
        [FromBody] CreateSkillCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating skill for user {UserId}", command.UserId);
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetSkillById), new { id = result.SkillId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(SkillDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SkillDto>> UpdateSkill(
        Guid id,
        [FromBody] UpdateSkillCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.SkillId) return BadRequest("ID mismatch");
        _logger.LogInformation("Updating skill {SkillId}", id);
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSkill(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting skill {SkillId}", id);
        var result = await _mediator.Send(new DeleteSkillCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
