// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ResumeCareerAchievementTracker.Core;

/// <summary>
/// Event raised when an achievement is featured or unfeatured.
/// </summary>
public record AchievementFeaturedEvent
{
    /// <summary>
    /// Gets the achievement ID.
    /// </summary>
    public Guid AchievementId { get; init; }

    /// <summary>
    /// Gets a value indicating whether the achievement is now featured.
    /// </summary>
    public bool IsFeatured { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
