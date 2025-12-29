using FamilyCalendarEventPlanner.Api.Features.Attendees;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FamilyCalendarEventPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttendeesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AttendeesController> _logger;

    public AttendeesController(IMediator mediator, ILogger<AttendeesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<EventAttendeeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<EventAttendeeDto>>> GetAttendees([FromQuery] Guid eventId)
    {
        _logger.LogInformation("Getting attendees for event {EventId}", eventId);

        var result = await _mediator.Send(new GetAttendeesQuery { EventId = eventId });

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(EventAttendeeDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EventAttendeeDto>> AddAttendee([FromBody] AddAttendeeCommand command)
    {
        _logger.LogInformation(
            "Adding attendee {FamilyMemberId} to event {EventId}",
            command.FamilyMemberId,
            command.EventId);

        var result = await _mediator.Send(command);

        return Created(string.Empty, result);
    }

    [HttpPut("{attendeeId:guid}/respond")]
    [ProducesResponseType(typeof(EventAttendeeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EventAttendeeDto>> RespondToEvent(Guid attendeeId, [FromBody] RespondToEventCommand command)
    {
        if (attendeeId != command.AttendeeId)
        {
            return BadRequest("Attendee ID mismatch");
        }

        _logger.LogInformation(
            "Updating RSVP for attendee {AttendeeId} to {RsvpStatus}",
            attendeeId,
            command.RsvpStatus);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
