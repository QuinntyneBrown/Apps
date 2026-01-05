// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CollegeSavingsPlanner.Core;

/// <summary>
/// Event raised when a projection is created.
/// </summary>
public record ProjectionCreatedEvent
{
    /// <summary>
    /// Gets the projection ID.
    /// </summary>
    public Guid ProjectionId { get; init; }

    /// <summary>
    /// Gets the plan ID.
    /// </summary>
    public Guid PlanId { get; init; }

    /// <summary>
    /// Gets the target goal.
    /// </summary>
    public decimal TargetGoal { get; init; }

    /// <summary>
    /// Gets the years until college.
    /// </summary>
    public int YearsUntilCollege { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
