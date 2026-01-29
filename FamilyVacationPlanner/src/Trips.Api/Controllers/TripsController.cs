using Trips.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Trips.Api.Controllers;

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
    public async Task<ActionResult<IEnumerable<TripDto>>> GetTrips(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all trips");
        var result = await _mediator.Send(new GetTripsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TripDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TripDto>> GetTripById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting trip {TripId}", id);
        var result = await _mediator.Send(new GetTripByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(TripDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<TripDto>> CreateTrip(
        [FromBody] CreateTripCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating trip for user {UserId}", command.UserId);
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetTripById), new { id = result.TripId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(TripDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TripDto>> UpdateTrip(
        Guid id,
        [FromBody] UpdateTripCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.TripId) return BadRequest("ID mismatch");
        _logger.LogInformation("Updating trip {TripId}", id);
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTrip(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting trip {TripId}", id);
        var result = await _mediator.Send(new DeleteTripCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
