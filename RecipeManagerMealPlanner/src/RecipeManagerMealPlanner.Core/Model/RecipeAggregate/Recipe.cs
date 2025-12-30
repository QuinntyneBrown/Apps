// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RecipeManagerMealPlanner.Core;

/// <summary>
/// Represents a recipe.
/// </summary>
public class Recipe
{
    /// <summary>
    /// Gets or sets the unique identifier for the recipe.
    /// </summary>
    public Guid RecipeId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this recipe.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the name of the recipe.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the recipe.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the cuisine type.
    /// </summary>
    public Cuisine Cuisine { get; set; }

    /// <summary>
    /// Gets or sets the difficulty level.
    /// </summary>
    public DifficultyLevel DifficultyLevel { get; set; }

    /// <summary>
    /// Gets or sets the preparation time in minutes.
    /// </summary>
    public int PrepTimeMinutes { get; set; }

    /// <summary>
    /// Gets or sets the cooking time in minutes.
    /// </summary>
    public int CookTimeMinutes { get; set; }

    /// <summary>
    /// Gets or sets the number of servings.
    /// </summary>
    public int Servings { get; set; }

    /// <summary>
    /// Gets or sets the instructions.
    /// </summary>
    public string Instructions { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the photo URL.
    /// </summary>
    public string? PhotoUrl { get; set; }

    /// <summary>
    /// Gets or sets the source or attribution.
    /// </summary>
    public string? Source { get; set; }

    /// <summary>
    /// Gets or sets the rating (1-5 stars).
    /// </summary>
    public int? Rating { get; set; }

    /// <summary>
    /// Gets or sets notes about the recipe.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the recipe is a favorite.
    /// </summary>
    public bool IsFavorite { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the collection of ingredients for this recipe.
    /// </summary>
    public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

    /// <summary>
    /// Gets or sets the collection of meal plans that include this recipe.
    /// </summary>
    public ICollection<MealPlan> MealPlans { get; set; } = new List<MealPlan>();

    /// <summary>
    /// Calculates the total time for the recipe.
    /// </summary>
    /// <returns>Total time in minutes.</returns>
    public int GetTotalTime()
    {
        return PrepTimeMinutes + CookTimeMinutes;
    }
}
