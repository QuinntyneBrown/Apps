// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BBQGrillingRecipeBook.Core;

/// <summary>
/// Event raised when a recipe is updated.
/// </summary>
public record RecipeUpdatedEvent
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
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
