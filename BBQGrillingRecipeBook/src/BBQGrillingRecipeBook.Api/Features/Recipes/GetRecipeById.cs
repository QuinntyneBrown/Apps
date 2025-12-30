// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BBQGrillingRecipeBook.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BBQGrillingRecipeBook.Api.Features.Recipes;

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
    private readonly IBBQGrillingRecipeBookContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetRecipeByIdQueryHandler"/> class.
    /// </summary>
    public GetRecipeByIdQueryHandler(IBBQGrillingRecipeBookContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<RecipeDto?> Handle(GetRecipeByIdQuery request, CancellationToken cancellationToken)
    {
        var recipe = await _context.Recipes
            .FirstOrDefaultAsync(r => r.RecipeId == request.RecipeId, cancellationToken);

        return recipe == null ? null : RecipeDto.FromEntity(recipe);
    }
}
