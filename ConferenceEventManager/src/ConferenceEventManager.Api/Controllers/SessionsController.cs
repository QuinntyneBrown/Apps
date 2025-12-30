// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Api.Features.Sessions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceEventManager.Api.Controllers;

/// <summary>
/// Controller for managing sessions.
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
    public SessionsController(IMediator mediator, ILogger<SessionsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all sessions, optionally filtered by event ID or user ID.
    /// </summary>
    /// <param name="eventId">Optional event ID to filter sessions.</param>
    /// <param name="userId">Optional user ID to filter sessions.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of sessions.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<SessionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<SessionDto>>> GetSessions([FromQuery] Guid? eventId, [FromQuery] Guid? userId, CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetSessions.Query { EventId = eventId, UserId = userId };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving sessions");
            return StatusCode(500, "An error occurred while retrieving sessions");
        }
    }

    /// <summary>
    /// Gets a session by ID.
    /// </summary>
    /// <param name="id">Session ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Session details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SessionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SessionDto>> GetSession(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetSessionById.Query { SessionId = id };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Session with ID {id} not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving session {SessionId}", id);
            return StatusCode(500, "An error occurred while retrieving the session");
        }
    }

    /// <summary>
    /// Creates a new session.
    /// </summary>
    /// <param name="command">Create session command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Created session.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(SessionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SessionDto>> CreateSession([FromBody] CreateSession.Command command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetSession), new { id = result.SessionId }, result);
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating session");
            return StatusCode(500, "An error occurred while creating the session");
        }
    }

    /// <summary>
    /// Updates an existing session.
    /// </summary>
    /// <param name="id">Session ID.</param>
    /// <param name="command">Update session command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Updated session.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(SessionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SessionDto>> UpdateSession(Guid id, [FromBody] UpdateSession.Command command, CancellationToken cancellationToken)
    {
        if (id != command.SessionId)
        {
            return BadRequest("Session ID mismatch");
        }

        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Session with ID {id} not found");
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating session {SessionId}", id);
            return StatusCode(500, "An error occurred while updating the session");
        }
    }

    /// <summary>
    /// Deletes a session.
    /// </summary>
    /// <param name="id">Session ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSession(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeleteSession.Command { SessionId = id };
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Session with ID {id} not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting session {SessionId}", id);
            return StatusCode(500, "An error occurred while deleting the session");
        }
    }
}
