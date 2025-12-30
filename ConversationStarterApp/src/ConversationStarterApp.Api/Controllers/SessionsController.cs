// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConversationStarterApp.Api;

/// <summary>
/// API controller for managing conversation sessions.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SessionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<SessionsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="SessionsController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public SessionsController(IMediator mediator, ILogger<SessionsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets a session by ID.
    /// </summary>
    /// <param name="id">The session ID.</param>
    /// <returns>The session.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SessionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SessionDto>> GetById(Guid id)
    {
        _logger.LogInformation("Getting session {SessionId}", id);

        var result = await _mediator.Send(new GetSessionByIdQuery { SessionId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Gets all sessions for a user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <returns>The list of sessions.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SessionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SessionDto>>> GetByUserId([FromQuery] Guid userId)
    {
        _logger.LogInformation("Getting sessions for user {UserId}", userId);

        var result = await _mediator.Send(new GetSessionsByUserIdQuery { UserId = userId });

        return Ok(result);
    }

    /// <summary>
    /// Gets recent sessions for a user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="count">The number of recent sessions to retrieve (default 10).</param>
    /// <returns>The list of recent sessions.</returns>
    [HttpGet("recent")]
    [ProducesResponseType(typeof(IEnumerable<SessionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SessionDto>>> GetRecent(
        [FromQuery] Guid userId,
        [FromQuery] int count = 10)
    {
        _logger.LogInformation("Getting {Count} recent sessions for user {UserId}", count, userId);

        var result = await _mediator.Send(new GetRecentSessionsQuery
        {
            UserId = userId,
            Count = count,
        });

        return Ok(result);
    }

    /// <summary>
    /// Creates a new session.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <returns>The created session.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(SessionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SessionDto>> Create([FromBody] CreateSessionCommand command)
    {
        _logger.LogInformation("Creating session for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.SessionId },
            result);
    }

    /// <summary>
    /// Updates an existing session.
    /// </summary>
    /// <param name="id">The session ID.</param>
    /// <param name="command">The update command.</param>
    /// <returns>The updated session.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(SessionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SessionDto>> Update(Guid id, [FromBody] UpdateSessionCommand command)
    {
        if (id != command.SessionId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating session {SessionId}", id);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes a session.
    /// </summary>
    /// <param name="id">The session ID.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting session {SessionId}", id);

        var result = await _mediator.Send(new DeleteSessionCommand { SessionId = id });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Ends a session.
    /// </summary>
    /// <param name="id">The session ID.</param>
    /// <returns>The updated session.</returns>
    [HttpPost("{id:guid}/end")]
    [ProducesResponseType(typeof(SessionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SessionDto>> EndSession(Guid id)
    {
        _logger.LogInformation("Ending session {SessionId}", id);

        var result = await _mediator.Send(new EndSessionCommand { SessionId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
