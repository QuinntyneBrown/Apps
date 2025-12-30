// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ChoreAssignmentTracker.Core;

/// <summary>
/// Event raised when a chore assignment is completed.
/// </summary>
public record AssignmentCompletedEvent
{
    /// <summary>
    /// Gets the assignment ID.
    /// </summary>
    public Guid AssignmentId { get; init; }

    /// <summary>
    /// Gets the family member ID.
    /// </summary>
    public Guid FamilyMemberId { get; init; }

    /// <summary>
    /// Gets the completion date.
    /// </summary>
    public DateTime CompletedDate { get; init; }

    /// <summary>
    /// Gets the points earned.
    /// </summary>
    public int PointsEarned { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
