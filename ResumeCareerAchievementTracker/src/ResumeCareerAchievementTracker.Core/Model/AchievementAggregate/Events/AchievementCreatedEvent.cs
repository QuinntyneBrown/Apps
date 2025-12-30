// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ResumeCareerAchievementTracker.Core;

/// <summary>
/// Event raised when a new achievement is created.
/// </summary>
public record AchievementCreatedEvent
{
    /// <summary>
    /// Gets the achievement ID.
    /// </summary>
    public Guid AchievementId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the achievement title.
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// Gets the achievement type.
    /// </summary>
    public AchievementType AchievementType { get; init; }

    /// <summary>
    /// Gets the achieved date.
    /// </summary>
    public DateTime AchievedDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
