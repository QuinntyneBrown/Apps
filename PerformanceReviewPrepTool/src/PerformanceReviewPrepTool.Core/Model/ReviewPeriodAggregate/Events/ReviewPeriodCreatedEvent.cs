// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PerformanceReviewPrepTool.Core;

/// <summary>
/// Event raised when a review period is created.
/// </summary>
public record ReviewPeriodCreatedEvent
{
    /// <summary>
    /// Gets the review period ID.
    /// </summary>
    public Guid ReviewPeriodId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the title.
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// Gets the start date.
    /// </summary>
    public DateTime StartDate { get; init; }

    /// <summary>
    /// Gets the end date.
    /// </summary>
    public DateTime EndDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
