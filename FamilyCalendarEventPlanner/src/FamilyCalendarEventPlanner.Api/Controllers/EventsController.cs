using FamilyCalendarEventPlanner.Api.Features.CalendarEvents;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FamilyCalendarEventPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<EventsController> _logger;

    public EventsController(IMediator mediator, ILogger<EventsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CalendarEventDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CalendarEventDto>>> GetEvents([FromQuery] Guid? familyId)
    {
        _logger.LogInformation("Getting calendar events for family {FamilyId}", familyId);

        var result = await _mediator.Send(new GetEventsQuery { FamilyId = familyId });

        return Ok(result);
    }

    [HttpGet("{eventId:guid}")]
    [ProducesResponseType(typeof(CalendarEventDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CalendarEventDto>> GetEventById(Guid eventId)
    {
        _logger.LogInformation("Getting calendar event {EventId}", eventId);

        var result = await _mediator.Send(new GetEventByIdQuery { EventId = eventId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CalendarEventDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CalendarEventDto>> CreateEvent([FromBody] CreateEventCommand command)
    {
        _logger.LogInformation("Creating calendar event for family {FamilyId}", command.FamilyId);

        var result = await _mediator.Send(command);

        return Created($"/api/events/{result.EventId}", result);
    }

    [HttpPut("{eventId:guid}")]
    [ProducesResponseType(typeof(CalendarEventDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CalendarEventDto>> UpdateEvent(Guid eventId, [FromBody] UpdateEventCommand command)
    {
        if (eventId != command.EventId)
        {
            return BadRequest("Event ID mismatch");
        }

        _logger.LogInformation("Updating calendar event {EventId}", eventId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost("{eventId:guid}/cancel")]
    [ProducesResponseType(typeof(CalendarEventDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CalendarEventDto>> CancelEvent(Guid eventId)
    {
        _logger.LogInformation("Cancelling calendar event {EventId}", eventId);

        var result = await _mediator.Send(new CancelEventCommand { EventId = eventId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost("{eventId:guid}/complete")]
    [ProducesResponseType(typeof(CalendarEventDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CalendarEventDto>> CompleteEvent(Guid eventId)
    {
        _logger.LogInformation("Completing calendar event {EventId}", eventId);

        var result = await _mediator.Send(new CompleteEventCommand { EventId = eventId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{eventId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteEvent(Guid eventId)
    {
        _logger.LogInformation("Deleting calendar event {EventId}", eventId);

        var result = await _mediator.Send(new DeleteEventCommand { EventId = eventId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
