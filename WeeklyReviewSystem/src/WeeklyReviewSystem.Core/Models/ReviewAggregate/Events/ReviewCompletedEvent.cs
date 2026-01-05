// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WeeklyReviewSystem.Core;

/// <summary>
/// Event raised when a weekly review is completed.
/// </summary>
public record ReviewCompletedEvent
{
    /// <summary>
    /// Gets the review ID.
    /// </summary>
    public Guid WeeklyReviewId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the week start date.
    /// </summary>
    public DateTime WeekStartDate { get; init; }

    /// <summary>
    /// Gets the overall rating.
    /// </summary>
    public int? OverallRating { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
