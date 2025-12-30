// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CouplesGoalTracker.Core;

/// <summary>
/// Event raised when a goal's status changes.
/// </summary>
public record GoalStatusChangedEvent
{
    /// <summary>
    /// Gets the goal ID.
    /// </summary>
    public Guid GoalId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the old status.
    /// </summary>
    public GoalStatus OldStatus { get; init; }

    /// <summary>
    /// Gets the new status.
    /// </summary>
    public GoalStatus NewStatus { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
