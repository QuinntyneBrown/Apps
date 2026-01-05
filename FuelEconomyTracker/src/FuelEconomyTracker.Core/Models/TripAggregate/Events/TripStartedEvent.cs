// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FuelEconomyTracker.Core;

/// <summary>
/// Event raised when a new trip is started.
/// </summary>
public record TripStartedEvent
{
    /// <summary>
    /// Gets the trip ID.
    /// </summary>
    public Guid TripId { get; init; }

    /// <summary>
    /// Gets the vehicle ID.
    /// </summary>
    public Guid VehicleId { get; init; }

    /// <summary>
    /// Gets the start date.
    /// </summary>
    public DateTime StartDate { get; init; }

    /// <summary>
    /// Gets the start odometer reading.
    /// </summary>
    public decimal StartOdometer { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
