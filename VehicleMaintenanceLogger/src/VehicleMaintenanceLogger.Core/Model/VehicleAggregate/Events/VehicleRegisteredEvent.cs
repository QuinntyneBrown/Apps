// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VehicleMaintenanceLogger.Core;

/// <summary>
/// Event raised when a new vehicle is registered.
/// </summary>
public record VehicleRegisteredEvent
{
    /// <summary>
    /// Gets the vehicle ID.
    /// </summary>
    public Guid VehicleId { get; init; }

    /// <summary>
    /// Gets the vehicle make.
    /// </summary>
    public string Make { get; init; } = string.Empty;

    /// <summary>
    /// Gets the vehicle model.
    /// </summary>
    public string Model { get; init; } = string.Empty;

    /// <summary>
    /// Gets the vehicle year.
    /// </summary>
    public int Year { get; init; }

    /// <summary>
    /// Gets the VIN.
    /// </summary>
    public string? VIN { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
