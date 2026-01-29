using Skills.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Skills.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SkillsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SkillsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SkillDto>>> GetSkills([FromQuery] Guid tenantId, [FromQuery] Guid userId)
    {
        var result = await _mediator.Send(new GetSkillsQuery(tenantId, userId));
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SkillDto>> GetSkill(Guid id, [FromQuery] Guid tenantId)
    {
        var result = await _mediator.Send(new GetSkillByIdQuery(id, tenantId));
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<SkillDto>> CreateSkill([FromBody] CreateSkillCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetSkill), new { id = result.SkillId, tenantId = command.TenantId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SkillDto>> UpdateSkill(Guid id, [FromBody] UpdateSkillCommand command)
    {
        if (id != command.SkillId) return BadRequest();
        var result = await _mediator.Send(command);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSkill(Guid id, [FromQuery] Guid tenantId)
    {
        var result = await _mediator.Send(new DeleteSkillCommand(id, tenantId));
        if (!result) return NotFound();
        return NoContent();
    }
}
