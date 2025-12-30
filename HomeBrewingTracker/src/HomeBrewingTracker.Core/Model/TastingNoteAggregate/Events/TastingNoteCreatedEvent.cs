// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeBrewingTracker.Core;

/// <summary>
/// Event raised when a new tasting note is created.
/// </summary>
public record TastingNoteCreatedEvent
{
    /// <summary>
    /// Gets the tasting note ID.
    /// </summary>
    public Guid TastingNoteId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the batch ID.
    /// </summary>
    public Guid BatchId { get; init; }

    /// <summary>
    /// Gets the rating.
    /// </summary>
    public int Rating { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
