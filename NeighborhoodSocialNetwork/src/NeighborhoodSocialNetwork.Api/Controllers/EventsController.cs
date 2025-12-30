using NeighborhoodSocialNetwork.Api.Features.Events;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NeighborhoodSocialNetwork.Api.Controllers;

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
    [ProducesResponseType(typeof(IEnumerable<EventDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<EventDto>>> GetEvents(
        [FromQuery] Guid? createdByNeighborId,
        [FromQuery] bool? isPublic,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] string? searchTerm)
    {
        _logger.LogInformation("Getting events");

        var result = await _mediator.Send(new GetEventsQuery
        {
            CreatedByNeighborId = createdByNeighborId,
            IsPublic = isPublic,
            StartDate = startDate,
            EndDate = endDate,
            SearchTerm = searchTerm,
        });

        return Ok(result);
    }

    [HttpGet("{eventId:guid}")]
    [ProducesResponseType(typeof(EventDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EventDto>> GetEventById(Guid eventId)
    {
        _logger.LogInformation("Getting event {EventId}", eventId);

        var result = await _mediator.Send(new GetEventByIdQuery { EventId = eventId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(EventDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EventDto>> CreateEvent([FromBody] CreateEventCommand command)
    {
        _logger.LogInformation("Creating event");

        var result = await _mediator.Send(command);

        return Created($"/api/events/{result.EventId}", result);
    }

    [HttpPut("{eventId:guid}")]
    [ProducesResponseType(typeof(EventDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EventDto>> UpdateEvent(Guid eventId, [FromBody] UpdateEventCommand command)
    {
        if (eventId != command.EventId)
        {
            return BadRequest("Event ID mismatch");
        }

        _logger.LogInformation("Updating event {EventId}", eventId);

        var result = await _mediator.Send(command);

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
        _logger.LogInformation("Deleting event {EventId}", eventId);

        var result = await _mediator.Send(new DeleteEventCommand { EventId = eventId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
