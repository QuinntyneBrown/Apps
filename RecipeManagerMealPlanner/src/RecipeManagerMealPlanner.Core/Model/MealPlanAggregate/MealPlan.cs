// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RecipeManagerMealPlanner.Core;

/// <summary>
/// Represents a meal plan.
/// </summary>
public class MealPlan
{
    /// <summary>
    /// Gets or sets the unique identifier for the meal plan.
    /// </summary>
    public Guid MealPlanId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this meal plan.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the name of the meal plan.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date for the meal.
    /// </summary>
    public DateTime MealDate { get; set; }

    /// <summary>
    /// Gets or sets the meal type (e.g., Breakfast, Lunch, Dinner, Snack).
    /// </summary>
    public string MealType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the recipe ID.
    /// </summary>
    public Guid? RecipeId { get; set; }

    /// <summary>
    /// Gets or sets the recipe.
    /// </summary>
    public Recipe? Recipe { get; set; }

    /// <summary>
    /// Gets or sets notes about the meal plan.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the meal has been prepared.
    /// </summary>
    public bool IsPrepared { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
