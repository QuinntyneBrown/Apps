using FamilyVacationPlanner.Api.Features.Trips;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FamilyVacationPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TripsController> _logger;

    public TripsController(IMediator mediator, ILogger<TripsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TripDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TripDto>>> GetTrips([FromQuery] Guid? userId)
    {
        _logger.LogInformation("Getting trips for user {UserId}", userId);

        var result = await _mediator.Send(new GetTripsQuery { UserId = userId });

        return Ok(result);
    }

    [HttpGet("{tripId:guid}")]
    [ProducesResponseType(typeof(TripDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TripDto>> GetTripById(Guid tripId)
    {
        _logger.LogInformation("Getting trip {TripId}", tripId);

        var result = await _mediator.Send(new GetTripByIdQuery { TripId = tripId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(TripDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TripDto>> CreateTrip([FromBody] CreateTripCommand command)
    {
        _logger.LogInformation("Creating trip for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/trips/{result.TripId}", result);
    }

    [HttpPut("{tripId:guid}")]
    [ProducesResponseType(typeof(TripDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TripDto>> UpdateTrip(Guid tripId, [FromBody] UpdateTripCommand command)
    {
        if (tripId != command.TripId)
        {
            return BadRequest("Trip ID mismatch");
        }

        _logger.LogInformation("Updating trip {TripId}", tripId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{tripId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTrip(Guid tripId)
    {
        _logger.LogInformation("Deleting trip {TripId}", tripId);

        var result = await _mediator.Send(new DeleteTripCommand { TripId = tripId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
