// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VehicleMaintenanceLogger.Core;

/// <summary>
/// Event raised when scheduled maintenance becomes due.
/// </summary>
public record MaintenanceDueEvent
{
    /// <summary>
    /// Gets the maintenance schedule ID.
    /// </summary>
    public Guid MaintenanceScheduleId { get; init; }

    /// <summary>
    /// Gets the vehicle ID.
    /// </summary>
    public Guid VehicleId { get; init; }

    /// <summary>
    /// Gets the service type.
    /// </summary>
    public ServiceType ServiceType { get; init; }

    /// <summary>
    /// Gets the current vehicle mileage.
    /// </summary>
    public decimal CurrentMileage { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
