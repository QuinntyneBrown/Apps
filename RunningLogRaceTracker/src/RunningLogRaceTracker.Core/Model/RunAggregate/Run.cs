// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RunningLogRaceTracker.Core;

/// <summary>
/// Represents a running activity or workout.
/// </summary>
public class Run
{
    /// <summary>
    /// Gets or sets the unique identifier for the run.
    /// </summary>
    public Guid RunId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who completed this run.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the distance in kilometers.
    /// </summary>
    public decimal Distance { get; set; }

    /// <summary>
    /// Gets or sets the duration in minutes.
    /// </summary>
    public int DurationMinutes { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the run was completed.
    /// </summary>
    public DateTime CompletedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the average pace (minutes per kilometer).
    /// </summary>
    public decimal? AveragePace { get; set; }

    /// <summary>
    /// Gets or sets the average heart rate.
    /// </summary>
    public int? AverageHeartRate { get; set; }

    /// <summary>
    /// Gets or sets the total elevation gain in meters.
    /// </summary>
    public int? ElevationGain { get; set; }

    /// <summary>
    /// Gets or sets the calories burned.
    /// </summary>
    public int? CaloriesBurned { get; set; }

    /// <summary>
    /// Gets or sets the route or location.
    /// </summary>
    public string? Route { get; set; }

    /// <summary>
    /// Gets or sets the weather conditions.
    /// </summary>
    public string? Weather { get; set; }

    /// <summary>
    /// Gets or sets optional notes about the run.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the perceived effort rating (1-10).
    /// </summary>
    public int? EffortRating { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Calculates the pace per kilometer.
    /// </summary>
    /// <returns>The pace in minutes per kilometer.</returns>
    public decimal CalculatePace()
    {
        if (Distance == 0) return 0;
        return DurationMinutes / Distance;
    }

    /// <summary>
    /// Checks if the run was completed today.
    /// </summary>
    /// <returns>True if completed today; otherwise, false.</returns>
    public bool IsToday()
    {
        return CompletedAt.Date == DateTime.UtcNow.Date;
    }
}
