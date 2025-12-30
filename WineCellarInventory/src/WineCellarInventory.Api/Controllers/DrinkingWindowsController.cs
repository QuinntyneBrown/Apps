using WineCellarInventory.Api.Features.DrinkingWindows;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WineCellarInventory.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DrinkingWindowsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<DrinkingWindowsController> _logger;

    public DrinkingWindowsController(IMediator mediator, ILogger<DrinkingWindowsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DrinkingWindowDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<DrinkingWindowDto>>> GetDrinkingWindows(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? wineId,
        [FromQuery] bool? isCurrent)
    {
        _logger.LogInformation("Getting drinking windows for user {UserId}", userId);

        var result = await _mediator.Send(new GetDrinkingWindowsQuery
        {
            UserId = userId,
            WineId = wineId,
            IsCurrent = isCurrent,
        });

        return Ok(result);
    }

    [HttpGet("{drinkingWindowId:guid}")]
    [ProducesResponseType(typeof(DrinkingWindowDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DrinkingWindowDto>> GetDrinkingWindowById(Guid drinkingWindowId)
    {
        _logger.LogInformation("Getting drinking window {DrinkingWindowId}", drinkingWindowId);

        var result = await _mediator.Send(new GetDrinkingWindowByIdQuery { DrinkingWindowId = drinkingWindowId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(DrinkingWindowDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DrinkingWindowDto>> CreateDrinkingWindow([FromBody] CreateDrinkingWindowCommand command)
    {
        _logger.LogInformation("Creating drinking window for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/drinkingwindows/{result.DrinkingWindowId}", result);
    }

    [HttpPut("{drinkingWindowId:guid}")]
    [ProducesResponseType(typeof(DrinkingWindowDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DrinkingWindowDto>> UpdateDrinkingWindow(Guid drinkingWindowId, [FromBody] UpdateDrinkingWindowCommand command)
    {
        if (drinkingWindowId != command.DrinkingWindowId)
        {
            return BadRequest("Drinking window ID mismatch");
        }

        _logger.LogInformation("Updating drinking window {DrinkingWindowId}", drinkingWindowId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{drinkingWindowId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDrinkingWindow(Guid drinkingWindowId)
    {
        _logger.LogInformation("Deleting drinking window {DrinkingWindowId}", drinkingWindowId);

        var result = await _mediator.Send(new DeleteDrinkingWindowCommand { DrinkingWindowId = drinkingWindowId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
