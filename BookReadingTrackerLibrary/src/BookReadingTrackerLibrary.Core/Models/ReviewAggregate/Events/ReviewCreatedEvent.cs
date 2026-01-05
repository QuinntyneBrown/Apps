// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BookReadingTrackerLibrary.Core;

/// <summary>
/// Event raised when a new review is created.
/// </summary>
public record ReviewCreatedEvent
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
    /// Gets the rating.
    /// </summary>
    public int Rating { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
