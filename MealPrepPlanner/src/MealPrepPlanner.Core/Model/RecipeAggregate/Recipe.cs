// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MealPrepPlanner.Core;

/// <summary>
/// Represents a recipe in the meal prep planner.
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
    /// Gets or sets the meal plan ID this recipe belongs to.
    /// </summary>
    public Guid? MealPlanId { get; set; }

    /// <summary>
    /// Gets or sets the name of the recipe.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the recipe.
    /// </summary>
    public string? Description { get; set; }

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
    /// Gets or sets the ingredients list (JSON format).
    /// </summary>
    public string Ingredients { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the cooking instructions.
    /// </summary>
    public string Instructions { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the meal type (e.g., Breakfast, Lunch, Dinner, Snack).
    /// </summary>
    public string MealType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets optional tags (e.g., Vegetarian, Gluten-Free).
    /// </summary>
    public string? Tags { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the recipe is a favorite.
    /// </summary>
    public bool IsFavorite { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the associated meal plan.
    /// </summary>
    public MealPlan? MealPlan { get; set; }

    /// <summary>
    /// Gets the total cooking time in minutes.
    /// </summary>
    /// <returns>The total time.</returns>
    public int GetTotalTime()
    {
        return PrepTimeMinutes + CookTimeMinutes;
    }

    /// <summary>
    /// Checks if the recipe is quick to prepare (total time under 30 minutes).
    /// </summary>
    /// <returns>True if quick; otherwise, false.</returns>
    public bool IsQuickMeal()
    {
        return GetTotalTime() < 30;
    }

    /// <summary>
    /// Toggles the favorite status of the recipe.
    /// </summary>
    public void ToggleFavorite()
    {
        IsFavorite = !IsFavorite;
    }
}
