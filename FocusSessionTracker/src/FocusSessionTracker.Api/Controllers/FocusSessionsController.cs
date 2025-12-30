// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FocusSessionTracker.Api.Features.FocusSession;
using FocusSessionTracker.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FocusSessionTracker.Api.Controllers;

/// <summary>
/// Controller for managing focus sessions.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class FocusSessionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<FocusSessionsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="FocusSessionsController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public FocusSessionsController(IMediator mediator, ILogger<FocusSessionsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all focus sessions with optional filters.
    /// </summary>
    /// <param name="userId">Optional user ID filter.</param>
    /// <param name="sessionType">Optional session type filter.</param>
    /// <param name="startDate">Optional start date filter.</param>
    /// <param name="endDate">Optional end date filter.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The list of focus sessions.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<FocusSessionDto>>> GetFocusSessions(
        [FromQuery] Guid? userId,
        [FromQuery] SessionType? sessionType,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        CancellationToken cancellationToken)
    {
        var query = new GetFocusSessionsQuery
        {
            UserId = userId,
            SessionType = sessionType,
            StartDate = startDate,
            EndDate = endDate
        };

        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets a focus session by ID.
    /// </summary>
    /// <param name="id">The focus session ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The focus session.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FocusSessionDto>> GetFocusSessionById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetFocusSessionByIdQuery { FocusSessionId = id };
        var result = await _mediator.Send(query, cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new focus session.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created focus session.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FocusSessionDto>> CreateFocusSession(
        [FromBody] CreateFocusSessionCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetFocusSessionById), new { id = result.FocusSessionId }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating focus session");
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Updates a focus session.
    /// </summary>
    /// <param name="id">The focus session ID.</param>
    /// <param name="command">The update command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated focus session.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FocusSessionDto>> UpdateFocusSession(
        Guid id,
        [FromBody] UpdateFocusSessionCommand command,
        CancellationToken cancellationToken)
    {
        command.FocusSessionId = id;

        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating focus session {Id}", id);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Completes a focus session.
    /// </summary>
    /// <param name="id">The focus session ID.</param>
    /// <param name="command">The complete command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The completed focus session.</returns>
    [HttpPost("{id}/complete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FocusSessionDto>> CompleteFocusSession(
        Guid id,
        [FromBody] CompleteFocusSessionCommand command,
        CancellationToken cancellationToken)
    {
        command.FocusSessionId = id;

        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error completing focus session {Id}", id);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Deletes a focus session.
    /// </summary>
    /// <param name="id">The focus session ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteFocusSession(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteFocusSessionCommand { FocusSessionId = id };
        var result = await _mediator.Send(command, cancellationToken);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
