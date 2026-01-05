// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FuelEconomyTracker.Core;

/// <summary>
/// Event raised when an efficiency report is generated.
/// </summary>
public record EfficiencyReportGeneratedEvent
{
    /// <summary>
    /// Gets the efficiency report ID.
    /// </summary>
    public Guid EfficiencyReportId { get; init; }

    /// <summary>
    /// Gets the vehicle ID.
    /// </summary>
    public Guid VehicleId { get; init; }

    /// <summary>
    /// Gets the start date.
    /// </summary>
    public DateTime StartDate { get; init; }

    /// <summary>
    /// Gets the end date.
    /// </summary>
    public DateTime EndDate { get; init; }

    /// <summary>
    /// Gets the average MPG.
    /// </summary>
    public decimal AverageMPG { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
