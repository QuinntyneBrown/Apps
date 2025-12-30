using TravelDestinationWishlist.Api.Features.Destinations;
using TravelDestinationWishlist.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TravelDestinationWishlist.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DestinationsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<DestinationsController> _logger;

    public DestinationsController(IMediator mediator, ILogger<DestinationsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DestinationDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<DestinationDto>>> GetDestinations(
        [FromQuery] Guid? userId,
        [FromQuery] DestinationType? destinationType,
        [FromQuery] bool? isVisited,
        [FromQuery] string? country)
    {
        _logger.LogInformation("Getting destinations for user {UserId}", userId);

        var result = await _mediator.Send(new GetDestinationsQuery
        {
            UserId = userId,
            DestinationType = destinationType,
            IsVisited = isVisited,
            Country = country,
        });

        return Ok(result);
    }

    [HttpGet("{destinationId:guid}")]
    [ProducesResponseType(typeof(DestinationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DestinationDto>> GetDestinationById(Guid destinationId)
    {
        _logger.LogInformation("Getting destination {DestinationId}", destinationId);

        var result = await _mediator.Send(new GetDestinationByIdQuery { DestinationId = destinationId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(DestinationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DestinationDto>> CreateDestination([FromBody] CreateDestinationCommand command)
    {
        _logger.LogInformation("Creating destination for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/destinations/{result.DestinationId}", result);
    }

    [HttpPut("{destinationId:guid}")]
    [ProducesResponseType(typeof(DestinationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DestinationDto>> UpdateDestination(Guid destinationId, [FromBody] UpdateDestinationCommand command)
    {
        if (destinationId != command.DestinationId)
        {
            return BadRequest("Destination ID mismatch");
        }

        _logger.LogInformation("Updating destination {DestinationId}", destinationId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{destinationId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDestination(Guid destinationId)
    {
        _logger.LogInformation("Deleting destination {DestinationId}", destinationId);

        var result = await _mediator.Send(new DeleteDestinationCommand { DestinationId = destinationId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut("{destinationId:guid}/visited")]
    [ProducesResponseType(typeof(DestinationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DestinationDto>> MarkDestinationVisited(Guid destinationId, [FromBody] MarkDestinationVisitedCommand command)
    {
        if (destinationId != command.DestinationId)
        {
            return BadRequest("Destination ID mismatch");
        }

        _logger.LogInformation("Marking destination {DestinationId} as visited", destinationId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
