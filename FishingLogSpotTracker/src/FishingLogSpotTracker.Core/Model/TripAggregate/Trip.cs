// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FishingLogSpotTracker.Core;

/// <summary>
/// Represents a fishing trip.
/// </summary>
public class Trip
{
    /// <summary>
    /// Gets or sets the unique identifier for the trip.
    /// </summary>
    public Guid TripId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this trip.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the spot ID where the trip took place.
    /// </summary>
    public Guid? SpotId { get; set; }

    /// <summary>
    /// Gets or sets the spot where the trip took place.
    /// </summary>
    public Spot? Spot { get; set; }

    /// <summary>
    /// Gets or sets the trip date.
    /// </summary>
    public DateTime TripDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the start time of the trip.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Gets or sets the end time of the trip.
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Gets or sets the weather conditions.
    /// </summary>
    public string? WeatherConditions { get; set; }

    /// <summary>
    /// Gets or sets the water temperature.
    /// </summary>
    public decimal? WaterTemperature { get; set; }

    /// <summary>
    /// Gets or sets the air temperature.
    /// </summary>
    public decimal? AirTemperature { get; set; }

    /// <summary>
    /// Gets or sets optional notes about the trip.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the collection of catches from this trip.
    /// </summary>
    public ICollection<Catch> Catches { get; set; } = new List<Catch>();

    /// <summary>
    /// Calculates the duration of the trip in hours.
    /// </summary>
    /// <returns>The duration in hours.</returns>
    public decimal? GetDurationInHours()
    {
        if (!EndTime.HasValue) return null;
        return (decimal)(EndTime.Value - StartTime).TotalHours;
    }

    /// <summary>
    /// Gets the total number of fish caught.
    /// </summary>
    /// <returns>The total catch count.</returns>
    public int GetTotalCatchCount()
    {
        return Catches?.Count ?? 0;
    }
}
