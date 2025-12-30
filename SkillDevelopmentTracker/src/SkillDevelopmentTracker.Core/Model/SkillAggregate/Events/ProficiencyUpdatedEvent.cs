// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SkillDevelopmentTracker.Core;

/// <summary>
/// Event raised when a skill's proficiency is updated.
/// </summary>
public record ProficiencyUpdatedEvent
{
    /// <summary>
    /// Gets the skill ID.
    /// </summary>
    public Guid SkillId { get; init; }

    /// <summary>
    /// Gets the old proficiency level.
    /// </summary>
    public ProficiencyLevel OldLevel { get; init; }

    /// <summary>
    /// Gets the new proficiency level.
    /// </summary>
    public ProficiencyLevel NewLevel { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
