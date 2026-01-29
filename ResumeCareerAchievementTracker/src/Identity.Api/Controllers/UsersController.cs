using Identity.Api.Features.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
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
        var result = await _mediator.Send(new GetUserByIdQuery(userId));
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserCommand command)
    {
        _logger.LogInformation("Creating user {Username}", command.UserName);
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetUserById), new { userId = result.UserId }, result);
    }

    [HttpPut("{userId:guid}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> UpdateUser(Guid userId, [FromBody] UpdateUserCommand command)
    {
        if (userId != command.UserId) return BadRequest("ID mismatch");
        _logger.LogInformation("Updating user {UserId}", userId);
        var result = await _mediator.Send(command);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{userId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        _logger.LogInformation("Deleting user {UserId}", userId);
        var result = await _mediator.Send(new DeleteUserCommand(userId));
        if (!result) return NotFound();
        return NoContent();
    }
}
