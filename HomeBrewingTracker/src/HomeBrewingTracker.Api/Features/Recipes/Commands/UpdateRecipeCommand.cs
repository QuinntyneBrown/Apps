// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeBrewingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeBrewingTracker.Api.Features.Recipes.Commands;

/// <summary>
/// Command to update an existing recipe.
/// </summary>
public class UpdateRecipeCommand : IRequest<RecipeDto>
{
    /// <summary>
    /// Gets or sets the recipe ID.
    /// </summary>
    public Guid RecipeId { get; set; }

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
    public decimal BatchSize { get; set; }

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
/// Handler for UpdateRecipeCommand.
/// </summary>
public class UpdateRecipeCommandHandler : IRequestHandler<UpdateRecipeCommand, RecipeDto>
{
    private readonly IHomeBrewingTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateRecipeCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UpdateRecipeCommandHandler(IHomeBrewingTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<RecipeDto> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = await _context.Recipes
            .FirstOrDefaultAsync(r => r.RecipeId == request.RecipeId, cancellationToken)
            ?? throw new InvalidOperationException($"Recipe with ID {request.RecipeId} not found.");

        recipe.Name = request.Name;
        recipe.BeerStyle = request.BeerStyle;
        recipe.Description = request.Description;
        recipe.OriginalGravity = request.OriginalGravity;
        recipe.FinalGravity = request.FinalGravity;
        recipe.ABV = request.ABV;
        recipe.IBU = request.IBU;
        recipe.BatchSize = request.BatchSize;
        recipe.Ingredients = request.Ingredients;
        recipe.Instructions = request.Instructions;
        recipe.Notes = request.Notes;

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
