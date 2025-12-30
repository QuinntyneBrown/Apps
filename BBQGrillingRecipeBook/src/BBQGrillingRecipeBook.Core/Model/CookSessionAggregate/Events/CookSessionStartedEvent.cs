// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BBQGrillingRecipeBook.Core;

/// <summary>
/// Event raised when a cook session is started.
/// </summary>
public record CookSessionStartedEvent
{
    /// <summary>
    /// Gets the cook session ID.
    /// </summary>
    public Guid CookSessionId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the recipe ID.
    /// </summary>
    public Guid RecipeId { get; init; }

    /// <summary>
    /// Gets the cook date.
    /// </summary>
    public DateTime CookDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
