using WineCellarInventory.Api.Features.Wines;
using WineCellarInventory.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WineCellarInventory.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WinesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<WinesController> _logger;

    public WinesController(IMediator mediator, ILogger<WinesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<WineDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<WineDto>>> GetWines(
        [FromQuery] Guid? userId,
        [FromQuery] WineType? wineType,
        [FromQuery] Region? region,
        [FromQuery] int? vintage,
        [FromQuery] string? producer)
    {
        _logger.LogInformation("Getting wines for user {UserId}", userId);

        var result = await _mediator.Send(new GetWinesQuery
        {
            UserId = userId,
            WineType = wineType,
            Region = region,
            Vintage = vintage,
            Producer = producer,
        });

        return Ok(result);
    }

    [HttpGet("{wineId:guid}")]
    [ProducesResponseType(typeof(WineDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WineDto>> GetWineById(Guid wineId)
    {
        _logger.LogInformation("Getting wine {WineId}", wineId);

        var result = await _mediator.Send(new GetWineByIdQuery { WineId = wineId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(WineDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<WineDto>> CreateWine([FromBody] CreateWineCommand command)
    {
        _logger.LogInformation("Creating wine for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/wines/{result.WineId}", result);
    }

    [HttpPut("{wineId:guid}")]
    [ProducesResponseType(typeof(WineDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WineDto>> UpdateWine(Guid wineId, [FromBody] UpdateWineCommand command)
    {
        if (wineId != command.WineId)
        {
            return BadRequest("Wine ID mismatch");
        }

        _logger.LogInformation("Updating wine {WineId}", wineId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{wineId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteWine(Guid wineId)
    {
        _logger.LogInformation("Deleting wine {WineId}", wineId);

        var result = await _mediator.Send(new DeleteWineCommand { WineId = wineId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
