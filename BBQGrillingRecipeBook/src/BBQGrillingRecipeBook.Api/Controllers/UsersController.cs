using BBQGrillingRecipeBook.Api.Features.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBQGrillingRecipeBook.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IMediator mediator, ILogger<UsersController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        _logger.LogInformation("Getting all users");
        var result = await _mediator.Send(new GetUsersQuery());
        return Ok(result);
    }

    [HttpGet("{userId:guid}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> GetUserById(Guid userId)
    {
        _logger.LogInformation("Getting user {UserId}", userId);
        var result = await _mediator.Send(new GetUserByIdQuery { UserId = userId });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserCommand command)
    {
        _logger.LogInformation("Creating user with username: {UserName}", command.UserName);
        try
        {
            var result = await _mediator.Send(command);
            return Created($"/api/users/{result.UserId}", result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{userId:guid}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDto>> UpdateUser(Guid userId, [FromBody] UpdateUserCommand command)
    {
        if (userId != command.UserId) return BadRequest("User ID mismatch");
        _logger.LogInformation("Updating user {UserId}", userId);
        try
        {
            var result = await _mediator.Send(command);
            if (result == null) return NotFound();
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("{userId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        _logger.LogInformation("Deleting user {UserId}", userId);
        var result = await _mediator.Send(new DeleteUserCommand { UserId = userId });
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpPost("{userId:guid}/roles")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDto>> AddRoleToUser(Guid userId, [FromBody] AddRoleRequest request)
    {
        _logger.LogInformation("Adding role {RoleId} to user {UserId}", request.RoleId, userId);
        try
        {
            var result = await _mediator.Send(new AddRoleToUserCommand { UserId = userId, RoleId = request.RoleId });
            if (result == null) return NotFound();
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("{userId:guid}/roles/{roleId:guid}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> RemoveRoleFromUser(Guid userId, Guid roleId)
    {
        _logger.LogInformation("Removing role {RoleId} from user {UserId}", roleId, userId);
        var result = await _mediator.Send(new RemoveRoleFromUserCommand { UserId = userId, RoleId = roleId });
        if (result == null) return NotFound();
        return Ok(result);
    }
}

public record AddRoleRequest
{
    public Guid RoleId { get; init; }
}
