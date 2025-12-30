// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CollegeSavingsPlanner.Core;

/// <summary>
/// Event raised when a 529 plan is created.
/// </summary>
public record PlanCreatedEvent
{
    /// <summary>
    /// Gets the plan ID.
    /// </summary>
    public Guid PlanId { get; init; }

    /// <summary>
    /// Gets the plan name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the state.
    /// </summary>
    public string State { get; init; } = string.Empty;

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
