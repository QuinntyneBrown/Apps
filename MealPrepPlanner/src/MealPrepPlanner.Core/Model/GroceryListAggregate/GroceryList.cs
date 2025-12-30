// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MealPrepPlanner.Core;

/// <summary>
/// Represents a grocery list for meal preparation.
/// </summary>
public class GroceryList
{
    /// <summary>
    /// Gets or sets the unique identifier for the grocery list.
    /// </summary>
    public Guid GroceryListId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this grocery list.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the meal plan ID this grocery list belongs to.
    /// </summary>
    public Guid? MealPlanId { get; set; }

    /// <summary>
    /// Gets or sets the name of the grocery list.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the items in the grocery list (JSON format).
    /// </summary>
    public string Items { get; set; } = "[]";

    /// <summary>
    /// Gets or sets the shopping date.
    /// </summary>
    public DateTime? ShoppingDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the list is completed.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// Gets or sets the estimated total cost.
    /// </summary>
    public decimal? EstimatedCost { get; set; }

    /// <summary>
    /// Gets or sets optional notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the associated meal plan.
    /// </summary>
    public MealPlan? MealPlan { get; set; }

    /// <summary>
    /// Marks the grocery list as completed.
    /// </summary>
    public void Complete()
    {
        IsCompleted = true;
    }

    /// <summary>
    /// Checks if shopping is scheduled for today.
    /// </summary>
    /// <returns>True if scheduled for today; otherwise, false.</returns>
    public bool IsScheduledForToday()
    {
        return ShoppingDate.HasValue && ShoppingDate.Value.Date == DateTime.UtcNow.Date;
    }
}
