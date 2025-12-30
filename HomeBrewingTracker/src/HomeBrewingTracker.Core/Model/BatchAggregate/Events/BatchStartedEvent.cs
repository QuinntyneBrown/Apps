// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeBrewingTracker.Core;

/// <summary>
/// Event raised when a new batch is started.
/// </summary>
public record BatchStartedEvent
{
    /// <summary>
    /// Gets the batch ID.
    /// </summary>
    public Guid BatchId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the recipe ID.
    /// </summary>
    public Guid RecipeId { get; init; }

    /// <summary>
    /// Gets the batch number.
    /// </summary>
    public string BatchNumber { get; init; } = string.Empty;

    /// <summary>
    /// Gets the brew date.
    /// </summary>
    public DateTime BrewDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
