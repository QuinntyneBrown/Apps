using RecipeManagerMealPlanner.Api.Features.Recipes;
using RecipeManagerMealPlanner.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RecipeManagerMealPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RecipesController> _logger;

    public RecipesController(IMediator mediator, ILogger<RecipesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RecipeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> GetRecipes(
        [FromQuery] Guid? userId,
        [FromQuery] Cuisine? cuisine,
        [FromQuery] DifficultyLevel? difficultyLevel,
        [FromQuery] bool? favoritesOnly)
    {
        _logger.LogInformation("Getting recipes for user {UserId}", userId);

        var result = await _mediator.Send(new GetRecipesQuery
        {
            UserId = userId,
            Cuisine = cuisine,
            DifficultyLevel = difficultyLevel,
            FavoritesOnly = favoritesOnly,
        });

        return Ok(result);
    }

    [HttpGet("{recipeId:guid}")]
    [ProducesResponseType(typeof(RecipeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RecipeDto>> GetRecipeById(Guid recipeId)
    {
        _logger.LogInformation("Getting recipe {RecipeId}", recipeId);

        var result = await _mediator.Send(new GetRecipeByIdQuery { RecipeId = recipeId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(RecipeDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RecipeDto>> CreateRecipe([FromBody] CreateRecipeCommand command)
    {
        _logger.LogInformation("Creating recipe for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/recipes/{result.RecipeId}", result);
    }

    [HttpPut("{recipeId:guid}")]
    [ProducesResponseType(typeof(RecipeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RecipeDto>> UpdateRecipe(Guid recipeId, [FromBody] UpdateRecipeCommand command)
    {
        if (recipeId != command.RecipeId)
        {
            return BadRequest("Recipe ID mismatch");
        }

        _logger.LogInformation("Updating recipe {RecipeId}", recipeId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{recipeId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRecipe(Guid recipeId)
    {
        _logger.LogInformation("Deleting recipe {RecipeId}", recipeId);

        var result = await _mediator.Send(new DeleteRecipeCommand { RecipeId = recipeId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
