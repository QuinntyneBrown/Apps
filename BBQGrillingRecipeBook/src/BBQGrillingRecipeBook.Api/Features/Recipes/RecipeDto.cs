// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BBQGrillingRecipeBook.Core;

namespace BBQGrillingRecipeBook.Api.Features.Recipes;

/// <summary>
/// Data transfer object for Recipe.
/// </summary>
public class RecipeDto
{
    /// <summary>
    /// Gets or sets the recipe ID.
    /// </summary>
    public Guid RecipeId { get; set; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }

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

    /// <summary>
    /// Gets or sets whether the recipe is a favorite.
    /// </summary>
    public bool IsFavorite { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Maps a Recipe entity to RecipeDto.
    /// </summary>
    public static RecipeDto FromEntity(Recipe recipe)
    {
        return new RecipeDto
        {
            RecipeId = recipe.RecipeId,
            UserId = recipe.UserId,
            Name = recipe.Name,
            Description = recipe.Description,
            MeatType = recipe.MeatType,
            CookingMethod = recipe.CookingMethod,
            PrepTimeMinutes = recipe.PrepTimeMinutes,
            CookTimeMinutes = recipe.CookTimeMinutes,
            Ingredients = recipe.Ingredients,
            Instructions = recipe.Instructions,
            TargetTemperature = recipe.TargetTemperature,
            Servings = recipe.Servings,
            Notes = recipe.Notes,
            IsFavorite = recipe.IsFavorite,
            CreatedAt = recipe.CreatedAt
        };
    }
}
