// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeMaintenanceSchedule.Core;

/// <summary>
/// Event raised when a maintenance task is completed.
/// </summary>
public record TaskCompletedEvent
{
    /// <summary>
    /// Gets the maintenance task ID.
    /// </summary>
    public Guid MaintenanceTaskId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the completion date.
    /// </summary>
    public DateTime CompletedDate { get; init; }

    /// <summary>
    /// Gets the actual cost.
    /// </summary>
    public decimal? ActualCost { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
