using CampingTripPlanner.Api.Features.Campsites;
using CampingTripPlanner.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CampingTripPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CampsitesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CampsitesController> _logger;

    public CampsitesController(IMediator mediator, ILogger<CampsitesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CampsiteDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CampsiteDto>>> GetCampsites(
        [FromQuery] Guid? userId,
        [FromQuery] CampsiteType? campsiteType,
        [FromQuery] bool? isFavorite)
    {
        _logger.LogInformation("Getting campsites for user {UserId}", userId);

        var result = await _mediator.Send(new GetCampsitesQuery
        {
            UserId = userId,
            CampsiteType = campsiteType,
            IsFavorite = isFavorite,
        });

        return Ok(result);
    }

    [HttpGet("{campsiteId:guid}")]
    [ProducesResponseType(typeof(CampsiteDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CampsiteDto>> GetCampsiteById(Guid campsiteId)
    {
        _logger.LogInformation("Getting campsite {CampsiteId}", campsiteId);

        var result = await _mediator.Send(new GetCampsiteByIdQuery { CampsiteId = campsiteId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CampsiteDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CampsiteDto>> CreateCampsite([FromBody] CreateCampsiteCommand command)
    {
        _logger.LogInformation("Creating campsite for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/campsites/{result.CampsiteId}", result);
    }

    [HttpPut("{campsiteId:guid}")]
    [ProducesResponseType(typeof(CampsiteDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CampsiteDto>> UpdateCampsite(Guid campsiteId, [FromBody] UpdateCampsiteCommand command)
    {
        if (campsiteId != command.CampsiteId)
        {
            return BadRequest("Campsite ID mismatch");
        }

        _logger.LogInformation("Updating campsite {CampsiteId}", campsiteId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{campsiteId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCampsite(Guid campsiteId)
    {
        _logger.LogInformation("Deleting campsite {CampsiteId}", campsiteId);

        var result = await _mediator.Send(new DeleteCampsiteCommand { CampsiteId = campsiteId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
