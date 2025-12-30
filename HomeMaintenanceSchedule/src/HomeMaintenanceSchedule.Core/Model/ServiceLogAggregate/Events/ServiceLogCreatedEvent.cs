// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeMaintenanceSchedule.Core;

/// <summary>
/// Event raised when a service log is created.
/// </summary>
public record ServiceLogCreatedEvent
{
    /// <summary>
    /// Gets the service log ID.
    /// </summary>
    public Guid ServiceLogId { get; init; }

    /// <summary>
    /// Gets the maintenance task ID.
    /// </summary>
    public Guid MaintenanceTaskId { get; init; }

    /// <summary>
    /// Gets the service date.
    /// </summary>
    public DateTime ServiceDate { get; init; }

    /// <summary>
    /// Gets the cost.
    /// </summary>
    public decimal? Cost { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
