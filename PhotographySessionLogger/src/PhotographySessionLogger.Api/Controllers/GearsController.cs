using PhotographySessionLogger.Api.Features.Gears;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PhotographySessionLogger.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GearsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<GearsController> _logger;

    public GearsController(IMediator mediator, ILogger<GearsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GearDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GearDto>>> GetGears(
        [FromQuery] Guid? userId,
        [FromQuery] string? gearType)
    {
        _logger.LogInformation("Getting gears for user {UserId}", userId);

        var result = await _mediator.Send(new GetGearsQuery
        {
            UserId = userId,
            GearType = gearType,
        });

        return Ok(result);
    }

    [HttpGet("{gearId:guid}")]
    [ProducesResponseType(typeof(GearDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GearDto>> GetGearById(Guid gearId)
    {
        _logger.LogInformation("Getting gear {GearId}", gearId);

        var result = await _mediator.Send(new GetGearByIdQuery { GearId = gearId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(GearDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GearDto>> CreateGear([FromBody] CreateGearCommand command)
    {
        _logger.LogInformation("Creating gear for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/gears/{result.GearId}", result);
    }

    [HttpPut("{gearId:guid}")]
    [ProducesResponseType(typeof(GearDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GearDto>> UpdateGear(Guid gearId, [FromBody] UpdateGearCommand command)
    {
        if (gearId != command.GearId)
        {
            return BadRequest("Gear ID mismatch");
        }

        _logger.LogInformation("Updating gear {GearId}", gearId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{gearId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteGear(Guid gearId)
    {
        _logger.LogInformation("Deleting gear {GearId}", gearId);

        var result = await _mediator.Send(new DeleteGearCommand { GearId = gearId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
