// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PerformanceReviewPrepTool.Core;

/// <summary>
/// Event raised when an achievement is added.
/// </summary>
public record AchievementAddedEvent
{
    /// <summary>
    /// Gets the achievement ID.
    /// </summary>
    public Guid AchievementId { get; init; }

    /// <summary>
    /// Gets the review period ID.
    /// </summary>
    public Guid ReviewPeriodId { get; init; }

    /// <summary>
    /// Gets the title.
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// Gets the achieved date.
    /// </summary>
    public DateTime AchievedDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
