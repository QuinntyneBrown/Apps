using MortgagePayoffOptimizer.Api.Features.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MortgagePayoffOptimizer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IMediator mediator, ILogger<AuthController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(LoginResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginResult>> Login([FromBody] LoginCommand command)
    {
        _logger.LogInformation("Login attempt for username: {Username}", command.Username);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return Unauthorized(new { error = "Invalid username or password" });
        }

        return Ok(result);
    }
}
