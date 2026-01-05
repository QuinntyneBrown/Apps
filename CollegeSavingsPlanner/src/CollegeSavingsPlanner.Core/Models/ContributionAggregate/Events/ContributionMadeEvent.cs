// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CollegeSavingsPlanner.Core;

/// <summary>
/// Event raised when a contribution is made.
/// </summary>
public record ContributionMadeEvent
{
    /// <summary>
    /// Gets the contribution ID.
    /// </summary>
    public Guid ContributionId { get; init; }

    /// <summary>
    /// Gets the plan ID.
    /// </summary>
    public Guid PlanId { get; init; }

    /// <summary>
    /// Gets the contribution amount.
    /// </summary>
    public decimal Amount { get; init; }

    /// <summary>
    /// Gets the contribution date.
    /// </summary>
    public DateTime ContributionDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
