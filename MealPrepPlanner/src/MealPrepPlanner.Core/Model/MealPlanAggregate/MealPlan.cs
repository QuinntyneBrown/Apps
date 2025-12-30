// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MealPrepPlanner.Core;

/// <summary>
/// Represents a meal plan for a specific time period.
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
    /// Gets or sets the start date of the meal plan.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the meal plan.
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Gets or sets the daily calorie target.
    /// </summary>
    public int? DailyCalorieTarget { get; set; }

    /// <summary>
    /// Gets or sets the dietary restrictions or preferences.
    /// </summary>
    public string? DietaryPreferences { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the plan is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets optional notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the collection of recipes in this meal plan.
    /// </summary>
    public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

    /// <summary>
    /// Gets or sets the collection of grocery lists for this meal plan.
    /// </summary>
    public ICollection<GroceryList> GroceryLists { get; set; } = new List<GroceryList>();

    /// <summary>
    /// Gets the duration of the meal plan in days.
    /// </summary>
    /// <returns>The duration in days.</returns>
    public int GetDuration()
    {
        return (EndDate - StartDate).Days + 1;
    }

    /// <summary>
    /// Checks if the meal plan is currently active (current date is within plan dates).
    /// </summary>
    /// <returns>True if currently active; otherwise, false.</returns>
    public bool IsCurrentlyActive()
    {
        var today = DateTime.UtcNow.Date;
        return IsActive && today >= StartDate.Date && today <= EndDate.Date;
    }
}
