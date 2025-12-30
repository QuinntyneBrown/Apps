// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ChoreAssignmentTracker.Core;

/// <summary>
/// Event raised when a chore assignment is created.
/// </summary>
public record AssignmentCreatedEvent
{
    /// <summary>
    /// Gets the assignment ID.
    /// </summary>
    public Guid AssignmentId { get; init; }

    /// <summary>
    /// Gets the chore ID.
    /// </summary>
    public Guid ChoreId { get; init; }

    /// <summary>
    /// Gets the family member ID.
    /// </summary>
    public Guid FamilyMemberId { get; init; }

    /// <summary>
    /// Gets the due date.
    /// </summary>
    public DateTime DueDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
