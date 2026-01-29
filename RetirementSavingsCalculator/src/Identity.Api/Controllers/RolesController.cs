using Identity.Api.Features.Roles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class RolesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RolesController> _logger;

    public RolesController(IMediator mediator, ILogger<RolesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RoleDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RoleDto>>> GetRoles()
    {
        _logger.LogInformation("Getting all roles");
        var result = await _mediator.Send(new GetRolesQuery());
        return Ok(result);
    }

    [HttpGet("{roleId:guid}")]
    [ProducesResponseType(typeof(RoleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RoleDto>> GetRoleById(Guid roleId)
    {
        _logger.LogInformation("Getting role {RoleId}", roleId);
        var result = await _mediator.Send(new GetRoleByIdQuery(roleId));
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(RoleDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<RoleDto>> CreateRole([FromBody] CreateRoleCommand command)
    {
        _logger.LogInformation("Creating role {RoleName}", command.Name);
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetRoleById), new { roleId = result.RoleId }, result);
    }

    [HttpDelete("{roleId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRole(Guid roleId)
    {
        _logger.LogInformation("Deleting role {RoleId}", roleId);
        var result = await _mediator.Send(new DeleteRoleCommand(roleId));
        if (!result) return NotFound();
        return NoContent();
    }
}
