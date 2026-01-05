// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RecipeManagerMealPlanner.Core;

/// <summary>
/// Event raised when a meal plan is created.
/// </summary>
public record MealPlanCreatedEvent
{
    /// <summary>
    /// Gets the meal plan ID.
    /// </summary>
    public Guid MealPlanId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the meal date.
    /// </summary>
    public DateTime MealDate { get; init; }

    /// <summary>
    /// Gets the meal type.
    /// </summary>
    public string MealType { get; init; } = string.Empty;

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
