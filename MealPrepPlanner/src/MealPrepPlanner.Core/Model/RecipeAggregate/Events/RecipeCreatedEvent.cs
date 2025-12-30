// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MealPrepPlanner.Core;

/// <summary>
/// Event raised when a new recipe is created.
/// </summary>
public record RecipeCreatedEvent
{
    /// <summary>
    /// Gets the recipe ID.
    /// </summary>
    public Guid RecipeId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the recipe name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the meal type.
    /// </summary>
    public string MealType { get; init; } = string.Empty;

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
