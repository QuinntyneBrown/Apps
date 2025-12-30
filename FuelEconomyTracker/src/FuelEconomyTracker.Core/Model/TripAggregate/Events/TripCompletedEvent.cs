// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FuelEconomyTracker.Core;

/// <summary>
/// Event raised when a trip is completed.
/// </summary>
public record TripCompletedEvent
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
    /// Gets the total distance traveled.
    /// </summary>
    public decimal Distance { get; init; }

    /// <summary>
    /// Gets the average MPG.
    /// </summary>
    public decimal? AverageMPG { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
