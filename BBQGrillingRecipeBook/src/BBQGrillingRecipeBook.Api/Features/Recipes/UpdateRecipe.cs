// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BBQGrillingRecipeBook.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BBQGrillingRecipeBook.Api.Features.Recipes;

/// <summary>
/// Command to update a recipe.
/// </summary>
public class UpdateRecipeCommand : IRequest<RecipeDto?>
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
    /// Gets or sets the recipe description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the meat type.
    /// </summary>
    public MeatType MeatType { get; set; }

    /// <summary>
    /// Gets or sets the cooking method.
    /// </summary>
    public CookingMethod CookingMethod { get; set; }

    /// <summary>
    /// Gets or sets the preparation time in minutes.
    /// </summary>
    public int PrepTimeMinutes { get; set; }

    /// <summary>
    /// Gets or sets the cooking time in minutes.
    /// </summary>
    public int CookTimeMinutes { get; set; }

    /// <summary>
    /// Gets or sets the ingredients list.
    /// </summary>
    public string Ingredients { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the cooking instructions.
    /// </summary>
    public string Instructions { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the target temperature.
    /// </summary>
    public int? TargetTemperature { get; set; }

    /// <summary>
    /// Gets or sets the number of servings.
    /// </summary>
    public int Servings { get; set; }

    /// <summary>
    /// Gets or sets optional notes.
    /// </summary>
    public string? Notes { get; set; }
}

/// <summary>
/// Handler for UpdateRecipeCommand.
/// </summary>
public class UpdateRecipeCommandHandler : IRequestHandler<UpdateRecipeCommand, RecipeDto?>
{
    private readonly IBBQGrillingRecipeBookContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateRecipeCommandHandler"/> class.
    /// </summary>
    public UpdateRecipeCommandHandler(IBBQGrillingRecipeBookContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<RecipeDto?> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = await _context.Recipes
            .FirstOrDefaultAsync(r => r.RecipeId == request.RecipeId, cancellationToken);

        if (recipe == null)
        {
            return null;
        }

        recipe.Name = request.Name;
        recipe.Description = request.Description;
        recipe.MeatType = request.MeatType;
        recipe.CookingMethod = request.CookingMethod;
        recipe.PrepTimeMinutes = request.PrepTimeMinutes;
        recipe.CookTimeMinutes = request.CookTimeMinutes;
        recipe.Ingredients = request.Ingredients;
        recipe.Instructions = request.Instructions;
        recipe.TargetTemperature = request.TargetTemperature;
        recipe.Servings = request.Servings;
        recipe.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return RecipeDto.FromEntity(recipe);
    }
}
