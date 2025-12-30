using CampingTripPlanner.Api.Features.GearChecklists;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CampingTripPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GearChecklistsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<GearChecklistsController> _logger;

    public GearChecklistsController(IMediator mediator, ILogger<GearChecklistsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GearChecklistDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GearChecklistDto>>> GetGearChecklists(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? tripId,
        [FromQuery] bool? isPacked)
    {
        _logger.LogInformation("Getting gear checklists for user {UserId}", userId);

        var result = await _mediator.Send(new GetGearChecklistsQuery
        {
            UserId = userId,
            TripId = tripId,
            IsPacked = isPacked,
        });

        return Ok(result);
    }

    [HttpGet("{gearChecklistId:guid}")]
    [ProducesResponseType(typeof(GearChecklistDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GearChecklistDto>> GetGearChecklistById(Guid gearChecklistId)
    {
        _logger.LogInformation("Getting gear checklist {GearChecklistId}", gearChecklistId);

        var result = await _mediator.Send(new GetGearChecklistByIdQuery { GearChecklistId = gearChecklistId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(GearChecklistDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GearChecklistDto>> CreateGearChecklist([FromBody] CreateGearChecklistCommand command)
    {
        _logger.LogInformation("Creating gear checklist for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/gearchecklists/{result.GearChecklistId}", result);
    }

    [HttpPut("{gearChecklistId:guid}")]
    [ProducesResponseType(typeof(GearChecklistDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GearChecklistDto>> UpdateGearChecklist(Guid gearChecklistId, [FromBody] UpdateGearChecklistCommand command)
    {
        if (gearChecklistId != command.GearChecklistId)
        {
            return BadRequest("Gear checklist ID mismatch");
        }

        _logger.LogInformation("Updating gear checklist {GearChecklistId}", gearChecklistId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{gearChecklistId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteGearChecklist(Guid gearChecklistId)
    {
        _logger.LogInformation("Deleting gear checklist {GearChecklistId}", gearChecklistId);

        var result = await _mediator.Send(new DeleteGearChecklistCommand { GearChecklistId = gearChecklistId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
