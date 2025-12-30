using FamilyVacationPlanner.Api.Features.Itineraries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FamilyVacationPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItinerariesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ItinerariesController> _logger;

    public ItinerariesController(IMediator mediator, ILogger<ItinerariesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ItineraryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ItineraryDto>>> GetItineraries([FromQuery] Guid? tripId)
    {
        _logger.LogInformation("Getting itineraries for trip {TripId}", tripId);

        var result = await _mediator.Send(new GetItinerariesQuery { TripId = tripId });

        return Ok(result);
    }

    [HttpGet("{itineraryId:guid}")]
    [ProducesResponseType(typeof(ItineraryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ItineraryDto>> GetItineraryById(Guid itineraryId)
    {
        _logger.LogInformation("Getting itinerary {ItineraryId}", itineraryId);

        var result = await _mediator.Send(new GetItineraryByIdQuery { ItineraryId = itineraryId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ItineraryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ItineraryDto>> CreateItinerary([FromBody] CreateItineraryCommand command)
    {
        _logger.LogInformation("Creating itinerary for trip {TripId}", command.TripId);

        var result = await _mediator.Send(command);

        return Created($"/api/itineraries/{result.ItineraryId}", result);
    }

    [HttpPut("{itineraryId:guid}")]
    [ProducesResponseType(typeof(ItineraryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ItineraryDto>> UpdateItinerary(Guid itineraryId, [FromBody] UpdateItineraryCommand command)
    {
        if (itineraryId != command.ItineraryId)
        {
            return BadRequest("Itinerary ID mismatch");
        }

        _logger.LogInformation("Updating itinerary {ItineraryId}", itineraryId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{itineraryId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteItinerary(Guid itineraryId)
    {
        _logger.LogInformation("Deleting itinerary {ItineraryId}", itineraryId);

        var result = await _mediator.Send(new DeleteItineraryCommand { ItineraryId = itineraryId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
