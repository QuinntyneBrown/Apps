// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeBrewingTracker.Api.Features.Recipes;
using HomeBrewingTracker.Api.Features.Recipes.Commands;
using HomeBrewingTracker.Api.Features.Recipes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeBrewingTracker.Api.Controllers;

/// <summary>
/// Controller for managing recipes.
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
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public RecipesController(IMediator mediator, ILogger<RecipesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all recipes.
    /// </summary>
    /// <param name="userId">Optional user ID filter.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
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
    /// <param name="id">The recipe ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The recipe.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RecipeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RecipeDto>> GetRecipe(Guid id, CancellationToken cancellationToken)
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
    /// <param name="command">The create command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created recipe.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(RecipeDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RecipeDto>> CreateRecipe(
        [FromBody] CreateRecipeCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new recipe: {Name}", command.Name);
        var recipe = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetRecipe), new { id = recipe.RecipeId }, recipe);
    }

    /// <summary>
    /// Updates an existing recipe.
    /// </summary>
    /// <param name="id">The recipe ID.</param>
    /// <param name="command">The update command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated recipe.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(RecipeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RecipeDto>> UpdateRecipe(
        Guid id,
        [FromBody] UpdateRecipeCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.RecipeId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating recipe with ID: {RecipeId}", id);

        try
        {
            var recipe = await _mediator.Send(command, cancellationToken);
            return Ok(recipe);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Deletes a recipe.
    /// </summary>
    /// <param name="id">The recipe ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRecipe(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting recipe with ID: {RecipeId}", id);

        try
        {
            var command = new DeleteRecipeCommand { RecipeId = id };
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}
