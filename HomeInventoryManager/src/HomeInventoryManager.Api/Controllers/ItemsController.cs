using HomeInventoryManager.Api.Features.Items;
using HomeInventoryManager.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeInventoryManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ItemsController> _logger;

    public ItemsController(IMediator mediator, ILogger<ItemsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ItemDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ItemDto>>> GetItems(
        [FromQuery] Guid? userId,
        [FromQuery] Category? category,
        [FromQuery] Room? room)
    {
        _logger.LogInformation("Getting items for user {UserId}", userId);

        var result = await _mediator.Send(new GetItemsQuery
        {
            UserId = userId,
            Category = category,
            Room = room,
        });

        return Ok(result);
    }

    [HttpGet("{itemId:guid}")]
    [ProducesResponseType(typeof(ItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ItemDto>> GetItemById(Guid itemId)
    {
        _logger.LogInformation("Getting item {ItemId}", itemId);

        var result = await _mediator.Send(new GetItemByIdQuery { ItemId = itemId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ItemDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ItemDto>> CreateItem([FromBody] CreateItemCommand command)
    {
        _logger.LogInformation("Creating item for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/items/{result.ItemId}", result);
    }

    [HttpPut("{itemId:guid}")]
    [ProducesResponseType(typeof(ItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ItemDto>> UpdateItem(Guid itemId, [FromBody] UpdateItemCommand command)
    {
        if (itemId != command.ItemId)
        {
            return BadRequest("Item ID mismatch");
        }

        _logger.LogInformation("Updating item {ItemId}", itemId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{itemId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteItem(Guid itemId)
    {
        _logger.LogInformation("Deleting item {ItemId}", itemId);

        var result = await _mediator.Send(new DeleteItemCommand { ItemId = itemId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
