// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeBrewingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeBrewingTracker.Api.Features.Recipes.Queries;

/// <summary>
/// Query to get a recipe by ID.
/// </summary>
public class GetRecipeByIdQuery : IRequest<RecipeDto?>
{
    /// <summary>
    /// Gets or sets the recipe ID.
    /// </summary>
    public Guid RecipeId { get; set; }
}

/// <summary>
/// Handler for GetRecipeByIdQuery.
/// </summary>
public class GetRecipeByIdQueryHandler : IRequestHandler<GetRecipeByIdQuery, RecipeDto?>
{
    private readonly IHomeBrewingTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetRecipeByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetRecipeByIdQueryHandler(IHomeBrewingTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<RecipeDto?> Handle(GetRecipeByIdQuery request, CancellationToken cancellationToken)
    {
        var recipe = await _context.Recipes
            .FirstOrDefaultAsync(r => r.RecipeId == request.RecipeId, cancellationToken);

        if (recipe == null)
        {
            return null;
        }

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
