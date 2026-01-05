// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InjuryPreventionRecoveryTracker.Core;

/// <summary>
/// Event raised when a recovery milestone is achieved.
/// </summary>
public record MilestoneAchievedEvent
{
    /// <summary>
    /// Gets the milestone ID.
    /// </summary>
    public Guid MilestoneId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the injury ID.
    /// </summary>
    public Guid InjuryId { get; init; }

    /// <summary>
    /// Gets the milestone name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the achievement date.
    /// </summary>
    public DateTime AchievedDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
