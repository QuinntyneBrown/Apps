// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ResumeCareerAchievementTracker.Core;

/// <summary>
/// Event raised when a skill's proficiency level is updated.
/// </summary>
public record SkillProficiencyUpdatedEvent
{
    /// <summary>
    /// Gets the skill ID.
    /// </summary>
    public Guid SkillId { get; init; }

    /// <summary>
    /// Gets the old proficiency level.
    /// </summary>
    public string OldLevel { get; init; } = string.Empty;

    /// <summary>
    /// Gets the new proficiency level.
    /// </summary>
    public string NewLevel { get; init; } = string.Empty;

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
