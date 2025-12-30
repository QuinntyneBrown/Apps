// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Api.Features.Events;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceEventManager.Api.Controllers;

/// <summary>
/// Controller for managing events.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<EventsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="EventsController"/> class.
    /// </summary>
    public EventsController(IMediator mediator, ILogger<EventsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all events, optionally filtered by user ID.
    /// </summary>
    /// <param name="userId">Optional user ID to filter events.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of events.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<EventDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<EventDto>>> GetEvents([FromQuery] Guid? userId, CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetEvents.Query { UserId = userId };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving events");
            return StatusCode(500, "An error occurred while retrieving events");
        }
    }

    /// <summary>
    /// Gets an event by ID.
    /// </summary>
    /// <param name="id">Event ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Event details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EventDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EventDto>> GetEvent(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetEventById.Query { EventId = id };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Event with ID {id} not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving event {EventId}", id);
            return StatusCode(500, "An error occurred while retrieving the event");
        }
    }

    /// <summary>
    /// Creates a new event.
    /// </summary>
    /// <param name="command">Create event command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Created event.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(EventDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EventDto>> CreateEvent([FromBody] CreateEvent.Command command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetEvent), new { id = result.EventId }, result);
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating event");
            return StatusCode(500, "An error occurred while creating the event");
        }
    }

    /// <summary>
    /// Updates an existing event.
    /// </summary>
    /// <param name="id">Event ID.</param>
    /// <param name="command">Update event command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Updated event.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(EventDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EventDto>> UpdateEvent(Guid id, [FromBody] UpdateEvent.Command command, CancellationToken cancellationToken)
    {
        if (id != command.EventId)
        {
            return BadRequest("Event ID mismatch");
        }

        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Event with ID {id} not found");
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating event {EventId}", id);
            return StatusCode(500, "An error occurred while updating the event");
        }
    }

    /// <summary>
    /// Deletes an event.
    /// </summary>
    /// <param name="id">Event ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteEvent(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeleteEvent.Command { EventId = id };
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Event with ID {id} not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting event {EventId}", id);
            return StatusCode(500, "An error occurred while deleting the event");
        }
    }
}
