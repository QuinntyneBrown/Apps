// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FishingLogSpotTracker.Api.Features.Trips;

/// <summary>
/// Data transfer object for Trip.
/// </summary>
public class TripDto
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
    /// Gets or sets the spot name.
    /// </summary>
    public string? SpotName { get; set; }

    /// <summary>
    /// Gets or sets the trip date.
    /// </summary>
    public DateTime TripDate { get; set; }

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
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the total catch count.
    /// </summary>
    public int CatchCount { get; set; }

    /// <summary>
    /// Gets or sets the duration in hours.
    /// </summary>
    public decimal? DurationInHours { get; set; }
}
