using Itineraries.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Itineraries.Api.Controllers;

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
    public async Task<ActionResult<IEnumerable<ItineraryDto>>> GetItineraries(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetItinerariesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ItineraryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ItineraryDto>> GetItineraryById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetItineraryByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ItineraryDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<ItineraryDto>> CreateItinerary(
        [FromBody] CreateItineraryCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetItineraryById), new { id = result.ItineraryId }, result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteItinerary(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteItineraryCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
