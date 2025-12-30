// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BookReadingTrackerLibrary.Core;

/// <summary>
/// Event raised when a reading session is logged.
/// </summary>
public record ReadingSessionLoggedEvent
{
    /// <summary>
    /// Gets the reading log ID.
    /// </summary>
    public Guid ReadingLogId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the book ID.
    /// </summary>
    public Guid BookId { get; init; }

    /// <summary>
    /// Gets the start page.
    /// </summary>
    public int StartPage { get; init; }

    /// <summary>
    /// Gets the end page.
    /// </summary>
    public int EndPage { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
