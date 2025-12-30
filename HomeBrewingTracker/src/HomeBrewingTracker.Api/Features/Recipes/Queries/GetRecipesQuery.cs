// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeBrewingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeBrewingTracker.Api.Features.Recipes.Queries;

/// <summary>
/// Query to get all recipes.
/// </summary>
public class GetRecipesQuery : IRequest<List<RecipeDto>>
{
    /// <summary>
    /// Gets or sets the optional user ID filter.
    /// </summary>
    public Guid? UserId { get; set; }
}

/// <summary>
/// Handler for GetRecipesQuery.
/// </summary>
public class GetRecipesQueryHandler : IRequestHandler<GetRecipesQuery, List<RecipeDto>>
{
    private readonly IHomeBrewingTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetRecipesQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetRecipesQueryHandler(IHomeBrewingTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<List<RecipeDto>> Handle(GetRecipesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Recipes.AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(r => r.UserId == request.UserId.Value);
        }

        var recipes = await query
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);

        return recipes.Select(r => new RecipeDto
        {
            RecipeId = r.RecipeId,
            UserId = r.UserId,
            Name = r.Name,
            BeerStyle = r.BeerStyle,
            Description = r.Description,
            OriginalGravity = r.OriginalGravity,
            FinalGravity = r.FinalGravity,
            ABV = r.ABV,
            IBU = r.IBU,
            BatchSize = r.BatchSize,
            Ingredients = r.Ingredients,
            Instructions = r.Instructions,
            Notes = r.Notes,
            IsFavorite = r.IsFavorite,
            CreatedAt = r.CreatedAt,
        }).ToList();
    }
}
