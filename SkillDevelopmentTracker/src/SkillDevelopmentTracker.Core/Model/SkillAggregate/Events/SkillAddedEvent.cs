// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SkillDevelopmentTracker.Core;

/// <summary>
/// Event raised when a new skill is added.
/// </summary>
public record SkillAddedEvent
{
    /// <summary>
    /// Gets the skill ID.
    /// </summary>
    public Guid SkillId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the skill name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the proficiency level.
    /// </summary>
    public ProficiencyLevel ProficiencyLevel { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
