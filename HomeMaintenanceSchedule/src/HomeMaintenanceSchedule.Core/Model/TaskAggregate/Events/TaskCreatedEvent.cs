// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeMaintenanceSchedule.Core;

/// <summary>
/// Event raised when a maintenance task is created.
/// </summary>
public record TaskCreatedEvent
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
    /// Gets the task name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the maintenance type.
    /// </summary>
    public MaintenanceType MaintenanceType { get; init; }

    /// <summary>
    /// Gets the due date.
    /// </summary>
    public DateTime? DueDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
