// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BloodPressureMonitor.Core;

/// <summary>
/// Event raised when a blood pressure trend is calculated.
/// </summary>
public record TrendCalculatedEvent
{
    /// <summary>
    /// Gets the trend ID.
    /// </summary>
    public Guid TrendId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the average systolic pressure.
    /// </summary>
    public decimal AverageSystolic { get; init; }

    /// <summary>
    /// Gets the average diastolic pressure.
    /// </summary>
    public decimal AverageDiastolic { get; init; }

    /// <summary>
    /// Gets the trend direction.
    /// </summary>
    public string TrendDirection { get; init; } = string.Empty;

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
