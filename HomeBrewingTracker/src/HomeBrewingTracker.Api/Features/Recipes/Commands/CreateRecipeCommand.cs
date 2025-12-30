// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeBrewingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeBrewingTracker.Api.Features.Recipes.Commands;

/// <summary>
/// Command to create a new recipe.
/// </summary>
public class CreateRecipeCommand : IRequest<RecipeDto>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the recipe name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the beer style.
    /// </summary>
    public BeerStyle BeerStyle { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the original gravity.
    /// </summary>
    public decimal? OriginalGravity { get; set; }

    /// <summary>
    /// Gets or sets the final gravity.
    /// </summary>
    public decimal? FinalGravity { get; set; }

    /// <summary>
    /// Gets or sets the ABV.
    /// </summary>
    public decimal? ABV { get; set; }

    /// <summary>
    /// Gets or sets the IBU.
    /// </summary>
    public int? IBU { get; set; }

    /// <summary>
    /// Gets or sets the batch size.
    /// </summary>
    public decimal BatchSize { get; set; } = 5.0m;

    /// <summary>
    /// Gets or sets the ingredients.
    /// </summary>
    public string Ingredients { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the instructions.
    /// </summary>
    public string Instructions { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    public string? Notes { get; set; }
}

/// <summary>
/// Handler for CreateRecipeCommand.
/// </summary>
public class CreateRecipeCommandHandler : IRequestHandler<CreateRecipeCommand, RecipeDto>
{
    private readonly IHomeBrewingTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateRecipeCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CreateRecipeCommandHandler(IHomeBrewingTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<RecipeDto> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = new Recipe
        {
            RecipeId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            BeerStyle = request.BeerStyle,
            Description = request.Description,
            OriginalGravity = request.OriginalGravity,
            FinalGravity = request.FinalGravity,
            ABV = request.ABV,
            IBU = request.IBU,
            BatchSize = request.BatchSize,
            Ingredients = request.Ingredients,
            Instructions = request.Instructions,
            Notes = request.Notes,
            IsFavorite = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync(cancellationToken);

        return new RecipeDto
        {
            RecipeId = recipe.RecipeId,
            UserId = recipe.UserId,
            Name = recipe.Name,
            BeerStyle = recipe.BeerStyle,
            Description = recipe.Description,
            OriginalGravity = recipe.OriginalGravity,
            FinalGravity = recipe.FinalGravity,
            ABV = recipe.ABV,
            IBU = recipe.IBU,
            BatchSize = recipe.BatchSize,
            Ingredients = recipe.Ingredients,
            Instructions = recipe.Instructions,
            Notes = recipe.Notes,
            IsFavorite = recipe.IsFavorite,
            CreatedAt = recipe.CreatedAt,
        };
    }
}
