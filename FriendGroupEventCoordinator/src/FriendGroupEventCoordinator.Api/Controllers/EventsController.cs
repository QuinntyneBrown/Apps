// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Api.Features.Events;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FriendGroupEventCoordinator.Api.Controllers;

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
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public EventsController(IMediator mediator, ILogger<EventsController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Gets all events.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of events.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<EventDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<EventDto>>> GetEvents(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all events");
        var events = await _mediator.Send(new GetEventsQuery(), cancellationToken);
        return Ok(events);
    }

    /// <summary>
    /// Gets an event by ID.
    /// </summary>
    /// <param name="id">The event ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The event.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EventDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EventDto>> GetEvent(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting event with ID: {EventId}", id);
        var eventDto = await _mediator.Send(new GetEventQuery(id), cancellationToken);

        if (eventDto == null)
        {
            _logger.LogWarning("Event with ID {EventId} not found", id);
            return NotFound();
        }

        return Ok(eventDto);
    }

    /// <summary>
    /// Gets all events for a specific group.
    /// </summary>
    /// <param name="groupId">The group ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of events for the group.</returns>
    [HttpGet("group/{groupId}")]
    [ProducesResponseType(typeof(List<EventDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<EventDto>>> GetEventsByGroup(Guid groupId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting events for group with ID: {GroupId}", groupId);
        var events = await _mediator.Send(new GetEventsByGroupQuery(groupId), cancellationToken);
        return Ok(events);
    }

    /// <summary>
    /// Creates a new event.
    /// </summary>
    /// <param name="createEventDto">The event to create.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created event.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(EventDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EventDto>> CreateEvent(CreateEventDto createEventDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new event: {Title}", createEventDto.Title);
        var eventDto = await _mediator.Send(new CreateEventCommand(createEventDto), cancellationToken);
        return CreatedAtAction(nameof(GetEvent), new { id = eventDto.EventId }, eventDto);
    }

    /// <summary>
    /// Updates an existing event.
    /// </summary>
    /// <param name="id">The event ID.</param>
    /// <param name="updateEventDto">The updated event data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated event.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(EventDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EventDto>> UpdateEvent(Guid id, UpdateEventDto updateEventDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating event with ID: {EventId}", id);
        var eventDto = await _mediator.Send(new UpdateEventCommand(id, updateEventDto), cancellationToken);

        if (eventDto == null)
        {
            _logger.LogWarning("Event with ID {EventId} not found", id);
            return NotFound();
        }

        return Ok(eventDto);
    }

    /// <summary>
    /// Cancels an event.
    /// </summary>
    /// <param name="id">The event ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The cancelled event.</returns>
    [HttpPost("{id}/cancel")]
    [ProducesResponseType(typeof(EventDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EventDto>> CancelEvent(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Cancelling event with ID: {EventId}", id);
        var eventDto = await _mediator.Send(new CancelEventCommand(id), cancellationToken);

        if (eventDto == null)
        {
            _logger.LogWarning("Event with ID {EventId} not found", id);
            return NotFound();
        }

        return Ok(eventDto);
    }

    /// <summary>
    /// Deletes an event.
    /// </summary>
    /// <param name="id">The event ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteEvent(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting event with ID: {EventId}", id);
        var result = await _mediator.Send(new DeleteEventCommand(id), cancellationToken);

        if (!result)
        {
            _logger.LogWarning("Event with ID {EventId} not found", id);
            return NotFound();
        }

        return NoContent();
    }
}
