// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BBQGrillingRecipeBook.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BBQGrillingRecipeBook.Api.Features.Recipes;

/// <summary>
/// Command to delete a recipe.
/// </summary>
public class DeleteRecipeCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the recipe ID.
    /// </summary>
    public Guid RecipeId { get; set; }
}

/// <summary>
/// Handler for DeleteRecipeCommand.
/// </summary>
public class DeleteRecipeCommandHandler : IRequestHandler<DeleteRecipeCommand, bool>
{
    private readonly IBBQGrillingRecipeBookContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteRecipeCommandHandler"/> class.
    /// </summary>
    public DeleteRecipeCommandHandler(IBBQGrillingRecipeBookContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = await _context.Recipes
            .FirstOrDefaultAsync(r => r.RecipeId == request.RecipeId, cancellationToken);

        if (recipe == null)
        {
            return false;
        }

        _context.Recipes.Remove(recipe);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
