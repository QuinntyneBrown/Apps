// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalHealthDashboard.Core;

/// <summary>
/// Event raised when a health trend is calculated.
/// </summary>
public record HealthTrendCalculatedEvent
{
    /// <summary>
    /// Gets the health trend ID.
    /// </summary>
    public Guid HealthTrendId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the metric name.
    /// </summary>
    public string MetricName { get; init; } = string.Empty;

    /// <summary>
    /// Gets the trend direction.
    /// </summary>
    public string TrendDirection { get; init; } = string.Empty;

    /// <summary>
    /// Gets the percentage change.
    /// </summary>
    public double PercentageChange { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
