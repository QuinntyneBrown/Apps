// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VehicleMaintenanceLogger.Core;

/// <summary>
/// Event raised when a vehicle's mileage is updated.
/// </summary>
public record VehicleMileageUpdatedEvent
{
    /// <summary>
    /// Gets the vehicle ID.
    /// </summary>
    public Guid VehicleId { get; init; }

    /// <summary>
    /// Gets the previous mileage.
    /// </summary>
    public decimal PreviousMileage { get; init; }

    /// <summary>
    /// Gets the new mileage.
    /// </summary>
    public decimal NewMileage { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
