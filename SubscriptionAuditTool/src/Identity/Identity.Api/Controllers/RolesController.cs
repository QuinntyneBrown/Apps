using Identity.Api.Features.Roles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RolesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoleDto>>> GetRoles([FromQuery] Guid tenantId)
    {
        var query = new GetRolesQuery(tenantId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoleDto>> GetRole(Guid id, [FromQuery] Guid tenantId)
    {
        var query = new GetRoleByIdQuery(id, tenantId);
        var result = await _mediator.Send(query);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<RoleDto>> CreateRole([FromBody] CreateRoleCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetRole), new { id = result.RoleId, tenantId = command.TenantId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<RoleDto>> UpdateRole(Guid id, [FromBody] UpdateRoleCommand command)
    {
        if (id != command.RoleId) return BadRequest();
        var result = await _mediator.Send(command);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(Guid id, [FromQuery] Guid tenantId)
    {
        var command = new DeleteRoleCommand(id, tenantId);
        var result = await _mediator.Send(command);
        if (!result) return NotFound();
        return NoContent();
    }
}
