using Identity.Api.Features.Auth;
using Identity.Api.Features.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IdentityController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<IdentityController> _logger;

    public IdentityController(IMediator mediator, ILogger<IdentityController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDto>> Register(
        [FromBody] RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Registering user {UserName}", command.UserName);
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetUser), new { id = result.UserId }, result);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginResponse>> Login(
        [FromBody] LoginCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Login attempt for {Email}", command.Email);
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return Unauthorized();
        return Ok(result);
    }

    [HttpGet("users/{id:guid}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> GetUser(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting user {UserId}", id);
        var result = await _mediator.Send(new GetUserByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("users")]
    [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all users");
        var result = await _mediator.Send(new GetUsersQuery(), cancellationToken);
        return Ok(result);
    }
}
