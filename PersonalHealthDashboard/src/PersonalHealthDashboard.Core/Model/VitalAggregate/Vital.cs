// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalHealthDashboard.Core;

/// <summary>
/// Represents a vital sign measurement in the health dashboard.
/// </summary>
public class Vital
{
    /// <summary>
    /// Gets or sets the unique identifier for the vital.
    /// </summary>
    public Guid VitalId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this vital.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the type of vital sign.
    /// </summary>
    public VitalType VitalType { get; set; }

    /// <summary>
    /// Gets or sets the measured value.
    /// </summary>
    public double Value { get; set; }

    /// <summary>
    /// Gets or sets the unit of measurement (e.g., mmHg, bpm, °F).
    /// </summary>
    public string Unit { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the timestamp when the measurement was taken.
    /// </summary>
    public DateTime MeasuredAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets optional notes about the measurement.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the source of the measurement (e.g., Manual, Wearable, Medical Device).
    /// </summary>
    public string? Source { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Checks if the vital value is within a normal range.
    /// </summary>
    /// <param name="minNormal">The minimum normal value.</param>
    /// <param name="maxNormal">The maximum normal value.</param>
    /// <returns>True if within normal range; otherwise, false.</returns>
    public bool IsWithinNormalRange(double minNormal, double maxNormal)
    {
        return Value >= minNormal && Value <= maxNormal;
    }

    /// <summary>
    /// Checks if this vital is a recent measurement (within the last 24 hours).
    /// </summary>
    /// <returns>True if recent; otherwise, false.</returns>
    public bool IsRecent()
    {
        return MeasuredAt >= DateTime.UtcNow.AddHours(-24);
    }
}
