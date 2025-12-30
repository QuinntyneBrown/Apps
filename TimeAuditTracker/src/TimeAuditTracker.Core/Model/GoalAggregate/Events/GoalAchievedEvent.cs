// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TimeAuditTracker.Core;

/// <summary>
/// Event raised when a goal is achieved.
/// </summary>
public record GoalAchievedEvent
{
    /// <summary>
    /// Gets the goal ID.
    /// </summary>
    public Guid GoalId { get; init; }

    /// <summary>
    /// Gets the actual hours achieved.
    /// </summary>
    public double ActualHours { get; init; }

    /// <summary>
    /// Gets the target hours.
    /// </summary>
    public double TargetHours { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
