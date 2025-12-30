using NeighborhoodSocialNetwork.Api.Features.Neighbors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NeighborhoodSocialNetwork.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NeighborsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<NeighborsController> _logger;

    public NeighborsController(IMediator mediator, ILogger<NeighborsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<NeighborDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<NeighborDto>>> GetNeighbors(
        [FromQuery] Guid? userId,
        [FromQuery] bool? isVerified,
        [FromQuery] string? searchTerm)
    {
        _logger.LogInformation("Getting neighbors");

        var result = await _mediator.Send(new GetNeighborsQuery
        {
            UserId = userId,
            IsVerified = isVerified,
            SearchTerm = searchTerm,
        });

        return Ok(result);
    }

    [HttpGet("{neighborId:guid}")]
    [ProducesResponseType(typeof(NeighborDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<NeighborDto>> GetNeighborById(Guid neighborId)
    {
        _logger.LogInformation("Getting neighbor {NeighborId}", neighborId);

        var result = await _mediator.Send(new GetNeighborByIdQuery { NeighborId = neighborId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(NeighborDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<NeighborDto>> CreateNeighbor([FromBody] CreateNeighborCommand command)
    {
        _logger.LogInformation("Creating neighbor for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/neighbors/{result.NeighborId}", result);
    }

    [HttpPut("{neighborId:guid}")]
    [ProducesResponseType(typeof(NeighborDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<NeighborDto>> UpdateNeighbor(Guid neighborId, [FromBody] UpdateNeighborCommand command)
    {
        if (neighborId != command.NeighborId)
        {
            return BadRequest("Neighbor ID mismatch");
        }

        _logger.LogInformation("Updating neighbor {NeighborId}", neighborId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{neighborId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteNeighbor(Guid neighborId)
    {
        _logger.LogInformation("Deleting neighbor {NeighborId}", neighborId);

        var result = await _mediator.Send(new DeleteNeighborCommand { NeighborId = neighborId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
