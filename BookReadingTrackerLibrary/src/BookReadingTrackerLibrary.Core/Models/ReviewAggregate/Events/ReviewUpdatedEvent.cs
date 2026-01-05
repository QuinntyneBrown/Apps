// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BookReadingTrackerLibrary.Core;

/// <summary>
/// Event raised when a review is updated.
/// </summary>
public record ReviewUpdatedEvent
{
    /// <summary>
    /// Gets the review ID.
    /// </summary>
    public Guid ReviewId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the book ID.
    /// </summary>
    public Guid BookId { get; init; }

    /// <summary>
    /// Gets the new rating.
    /// </summary>
    public int NewRating { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
