// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VehicleMaintenanceLogger.Core;

/// <summary>
/// Event raised when a service is completed.
/// </summary>
public record ServiceCompletedEvent
{
    /// <summary>
    /// Gets the service record ID.
    /// </summary>
    public Guid ServiceRecordId { get; init; }

    /// <summary>
    /// Gets the vehicle ID.
    /// </summary>
    public Guid VehicleId { get; init; }

    /// <summary>
    /// Gets the service cost.
    /// </summary>
    public decimal Cost { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
