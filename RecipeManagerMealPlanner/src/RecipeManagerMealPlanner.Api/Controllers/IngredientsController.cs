using RecipeManagerMealPlanner.Api.Features.Ingredients;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RecipeManagerMealPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<IngredientsController> _logger;

    public IngredientsController(IMediator mediator, ILogger<IngredientsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<IngredientDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<IngredientDto>>> GetIngredients([FromQuery] Guid? recipeId)
    {
        _logger.LogInformation("Getting ingredients for recipe {RecipeId}", recipeId);

        var result = await _mediator.Send(new GetIngredientsQuery { RecipeId = recipeId });

        return Ok(result);
    }

    [HttpGet("{ingredientId:guid}")]
    [ProducesResponseType(typeof(IngredientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IngredientDto>> GetIngredientById(Guid ingredientId)
    {
        _logger.LogInformation("Getting ingredient {IngredientId}", ingredientId);

        var result = await _mediator.Send(new GetIngredientByIdQuery { IngredientId = ingredientId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(IngredientDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IngredientDto>> CreateIngredient([FromBody] CreateIngredientCommand command)
    {
        _logger.LogInformation("Creating ingredient for recipe {RecipeId}", command.RecipeId);

        var result = await _mediator.Send(command);

        return Created($"/api/ingredients/{result.IngredientId}", result);
    }

    [HttpPut("{ingredientId:guid}")]
    [ProducesResponseType(typeof(IngredientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IngredientDto>> UpdateIngredient(Guid ingredientId, [FromBody] UpdateIngredientCommand command)
    {
        if (ingredientId != command.IngredientId)
        {
            return BadRequest("Ingredient ID mismatch");
        }

        _logger.LogInformation("Updating ingredient {IngredientId}", ingredientId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{ingredientId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteIngredient(Guid ingredientId)
    {
        _logger.LogInformation("Deleting ingredient {IngredientId}", ingredientId);

        var result = await _mediator.Send(new DeleteIngredientCommand { IngredientId = ingredientId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
