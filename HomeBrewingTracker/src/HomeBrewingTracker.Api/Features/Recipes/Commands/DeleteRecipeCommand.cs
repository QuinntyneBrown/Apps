// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeBrewingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeBrewingTracker.Api.Features.Recipes.Commands;

/// <summary>
/// Command to delete a recipe.
/// </summary>
public class DeleteRecipeCommand : IRequest<Unit>
{
    /// <summary>
    /// Gets or sets the recipe ID.
    /// </summary>
    public Guid RecipeId { get; set; }
}

/// <summary>
/// Handler for DeleteRecipeCommand.
/// </summary>
public class DeleteRecipeCommandHandler : IRequestHandler<DeleteRecipeCommand, Unit>
{
    private readonly IHomeBrewingTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteRecipeCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public DeleteRecipeCommandHandler(IHomeBrewingTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<Unit> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = await _context.Recipes
            .FirstOrDefaultAsync(r => r.RecipeId == request.RecipeId, cancellationToken)
            ?? throw new InvalidOperationException($"Recipe with ID {request.RecipeId} not found.");

        _context.Recipes.Remove(recipe);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
