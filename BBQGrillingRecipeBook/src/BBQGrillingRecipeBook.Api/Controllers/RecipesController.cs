// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BBQGrillingRecipeBook.Api.Features.Recipes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BBQGrillingRecipeBook.Api.Controllers;

/// <summary>
/// Controller for managing BBQ grilling recipes.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RecipesController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="RecipesController"/> class.
    /// </summary>
    public RecipesController(IMediator mediator, ILogger<RecipesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all recipes.
    /// </summary>
    /// <param name="userId">Optional user ID filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of recipes.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<RecipeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<RecipeDto>>> GetRecipes(
        [FromQuery] Guid? userId,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all recipes");
        var query = new GetRecipesQuery { UserId = userId };
        var recipes = await _mediator.Send(query, cancellationToken);
        return Ok(recipes);
    }

    /// <summary>
    /// Gets a recipe by ID.
    /// </summary>
    /// <param name="id">Recipe ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Recipe details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RecipeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RecipeDto>> GetRecipeById(
        Guid id,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting recipe with ID: {RecipeId}", id);
        var query = new GetRecipeByIdQuery { RecipeId = id };
        var recipe = await _mediator.Send(query, cancellationToken);

        if (recipe == null)
        {
            return NotFound();
        }

        return Ok(recipe);
    }

    /// <summary>
    /// Creates a new recipe.
    /// </summary>
    /// <param name="command">Create recipe command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Created recipe.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(RecipeDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RecipeDto>> CreateRecipe(
        [FromBody] CreateRecipeCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new recipe: {RecipeName}", command.Name);
        var recipe = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetRecipeById), new { id = recipe.RecipeId }, recipe);
    }

    /// <summary>
    /// Updates an existing recipe.
    /// </summary>
    /// <param name="id">Recipe ID.</param>
    /// <param name="command">Update recipe command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Updated recipe.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(RecipeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RecipeDto>> UpdateRecipe(
        Guid id,
        [FromBody] UpdateRecipeCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.RecipeId)
        {
            return BadRequest("Recipe ID mismatch");
        }

        _logger.LogInformation("Updating recipe with ID: {RecipeId}", id);
        var recipe = await _mediator.Send(command, cancellationToken);

        if (recipe == null)
        {
            return NotFound();
        }

        return Ok(recipe);
    }

    /// <summary>
    /// Deletes a recipe.
    /// </summary>
    /// <param name="id">Recipe ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRecipe(
        Guid id,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting recipe with ID: {RecipeId}", id);
        var command = new DeleteRecipeCommand { RecipeId = id };
        var result = await _mediator.Send(command, cancellationToken);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
