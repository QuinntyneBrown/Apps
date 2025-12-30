using MealPrepPlanner.Api.Features.GroceryLists;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MealPrepPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroceryListsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<GroceryListsController> _logger;

    public GroceryListsController(IMediator mediator, ILogger<GroceryListsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GroceryListDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GroceryListDto>>> GetGroceryLists(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? mealPlanId,
        [FromQuery] bool? isCompleted)
    {
        _logger.LogInformation("Getting grocery lists for user {UserId}", userId);

        var result = await _mediator.Send(new GetGroceryListsQuery
        {
            UserId = userId,
            MealPlanId = mealPlanId,
            IsCompleted = isCompleted,
        });

        return Ok(result);
    }

    [HttpGet("{groceryListId:guid}")]
    [ProducesResponseType(typeof(GroceryListDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GroceryListDto>> GetGroceryListById(Guid groceryListId)
    {
        _logger.LogInformation("Getting grocery list {GroceryListId}", groceryListId);

        var result = await _mediator.Send(new GetGroceryListByIdQuery { GroceryListId = groceryListId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(GroceryListDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GroceryListDto>> CreateGroceryList([FromBody] CreateGroceryListCommand command)
    {
        _logger.LogInformation("Creating grocery list for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/grocerylists/{result.GroceryListId}", result);
    }

    [HttpPut("{groceryListId:guid}")]
    [ProducesResponseType(typeof(GroceryListDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GroceryListDto>> UpdateGroceryList(Guid groceryListId, [FromBody] UpdateGroceryListCommand command)
    {
        if (groceryListId != command.GroceryListId)
        {
            return BadRequest("Grocery list ID mismatch");
        }

        _logger.LogInformation("Updating grocery list {GroceryListId}", groceryListId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{groceryListId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteGroceryList(Guid groceryListId)
    {
        _logger.LogInformation("Deleting grocery list {GroceryListId}", groceryListId);

        var result = await _mediator.Send(new DeleteGroceryListCommand { GroceryListId = groceryListId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
