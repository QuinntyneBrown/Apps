using PhotographySessionLogger.Api.Features.Sessions;
using PhotographySessionLogger.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PhotographySessionLogger.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SessionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<SessionsController> _logger;

    public SessionsController(IMediator mediator, ILogger<SessionsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SessionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SessionDto>>> GetSessions(
        [FromQuery] Guid? userId,
        [FromQuery] SessionType? sessionType,
        [FromQuery] string? location)
    {
        _logger.LogInformation("Getting sessions for user {UserId}", userId);

        var result = await _mediator.Send(new GetSessionsQuery
        {
            UserId = userId,
            SessionType = sessionType,
            Location = location,
        });

        return Ok(result);
    }

    [HttpGet("{sessionId:guid}")]
    [ProducesResponseType(typeof(SessionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SessionDto>> GetSessionById(Guid sessionId)
    {
        _logger.LogInformation("Getting session {SessionId}", sessionId);

        var result = await _mediator.Send(new GetSessionByIdQuery { SessionId = sessionId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(SessionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SessionDto>> CreateSession([FromBody] CreateSessionCommand command)
    {
        _logger.LogInformation("Creating session for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/sessions/{result.SessionId}", result);
    }

    [HttpPut("{sessionId:guid}")]
    [ProducesResponseType(typeof(SessionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SessionDto>> UpdateSession(Guid sessionId, [FromBody] UpdateSessionCommand command)
    {
        if (sessionId != command.SessionId)
        {
            return BadRequest("Session ID mismatch");
        }

        _logger.LogInformation("Updating session {SessionId}", sessionId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{sessionId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSession(Guid sessionId)
    {
        _logger.LogInformation("Deleting session {SessionId}", sessionId);

        var result = await _mediator.Send(new DeleteSessionCommand { SessionId = sessionId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
