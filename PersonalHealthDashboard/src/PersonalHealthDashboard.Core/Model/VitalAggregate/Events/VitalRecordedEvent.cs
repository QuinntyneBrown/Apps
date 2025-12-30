// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalHealthDashboard.Core;

/// <summary>
/// Event raised when a new vital sign measurement is recorded.
/// </summary>
public record VitalRecordedEvent
{
    /// <summary>
    /// Gets the vital ID.
    /// </summary>
    public Guid VitalId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the vital type.
    /// </summary>
    public VitalType VitalType { get; init; }

    /// <summary>
    /// Gets the measured value.
    /// </summary>
    public double Value { get; init; }

    /// <summary>
    /// Gets the unit of measurement.
    /// </summary>
    public string Unit { get; init; } = string.Empty;

    /// <summary>
    /// Gets the measurement timestamp.
    /// </summary>
    public DateTime MeasuredAt { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
