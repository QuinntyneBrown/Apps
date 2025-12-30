using RecipeManagerMealPlanner.Api.Features.ShoppingLists;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RecipeManagerMealPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShoppingListsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ShoppingListsController> _logger;

    public ShoppingListsController(IMediator mediator, ILogger<ShoppingListsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ShoppingListDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ShoppingListDto>>> GetShoppingLists(
        [FromQuery] Guid? userId,
        [FromQuery] bool? completedOnly)
    {
        _logger.LogInformation("Getting shopping lists for user {UserId}", userId);

        var result = await _mediator.Send(new GetShoppingListsQuery
        {
            UserId = userId,
            CompletedOnly = completedOnly,
        });

        return Ok(result);
    }

    [HttpGet("{shoppingListId:guid}")]
    [ProducesResponseType(typeof(ShoppingListDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ShoppingListDto>> GetShoppingListById(Guid shoppingListId)
    {
        _logger.LogInformation("Getting shopping list {ShoppingListId}", shoppingListId);

        var result = await _mediator.Send(new GetShoppingListByIdQuery { ShoppingListId = shoppingListId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ShoppingListDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ShoppingListDto>> CreateShoppingList([FromBody] CreateShoppingListCommand command)
    {
        _logger.LogInformation("Creating shopping list for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/shoppinglists/{result.ShoppingListId}", result);
    }

    [HttpPut("{shoppingListId:guid}")]
    [ProducesResponseType(typeof(ShoppingListDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ShoppingListDto>> UpdateShoppingList(Guid shoppingListId, [FromBody] UpdateShoppingListCommand command)
    {
        if (shoppingListId != command.ShoppingListId)
        {
            return BadRequest("Shopping list ID mismatch");
        }

        _logger.LogInformation("Updating shopping list {ShoppingListId}", shoppingListId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{shoppingListId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteShoppingList(Guid shoppingListId)
    {
        _logger.LogInformation("Deleting shopping list {ShoppingListId}", shoppingListId);

        var result = await _mediator.Send(new DeleteShoppingListCommand { ShoppingListId = shoppingListId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
