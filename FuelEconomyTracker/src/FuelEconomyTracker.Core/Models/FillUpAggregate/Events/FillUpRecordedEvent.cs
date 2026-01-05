// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FuelEconomyTracker.Core;

/// <summary>
/// Event raised when a new fill-up is recorded.
/// </summary>
public record FillUpRecordedEvent
{
    /// <summary>
    /// Gets the fill-up ID.
    /// </summary>
    public Guid FillUpId { get; init; }

    /// <summary>
    /// Gets the vehicle ID.
    /// </summary>
    public Guid VehicleId { get; init; }

    /// <summary>
    /// Gets the fill-up date.
    /// </summary>
    public DateTime FillUpDate { get; init; }

    /// <summary>
    /// Gets the gallons filled.
    /// </summary>
    public decimal Gallons { get; init; }

    /// <summary>
    /// Gets the total cost.
    /// </summary>
    public decimal TotalCost { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
