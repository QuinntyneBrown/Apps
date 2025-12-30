// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RecipeManagerMealPlanner.Core;

/// <summary>
/// Represents an ingredient in a recipe.
/// </summary>
public class Ingredient
{
    /// <summary>
    /// Gets or sets the unique identifier for the ingredient.
    /// </summary>
    public Guid IngredientId { get; set; }

    /// <summary>
    /// Gets or sets the recipe ID.
    /// </summary>
    public Guid RecipeId { get; set; }

    /// <summary>
    /// Gets or sets the recipe.
    /// </summary>
    public Recipe? Recipe { get; set; }

    /// <summary>
    /// Gets or sets the name of the ingredient.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the quantity.
    /// </summary>
    public string Quantity { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unit of measurement.
    /// </summary>
    public string? Unit { get; set; }

    /// <summary>
    /// Gets or sets notes about the ingredient.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
