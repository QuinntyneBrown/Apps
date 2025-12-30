// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ChoreAssignmentTracker.Core;

/// <summary>
/// Event raised when points are awarded to a family member.
/// </summary>
public record PointsAwardedEvent
{
    /// <summary>
    /// Gets the family member ID.
    /// </summary>
    public Guid FamilyMemberId { get; init; }

    /// <summary>
    /// Gets the points awarded.
    /// </summary>
    public int Points { get; init; }

    /// <summary>
    /// Gets the reason for the award.
    /// </summary>
    public string? Reason { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
