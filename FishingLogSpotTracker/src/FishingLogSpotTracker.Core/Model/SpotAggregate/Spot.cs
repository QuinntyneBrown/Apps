// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FishingLogSpotTracker.Core;

/// <summary>
/// Represents a fishing spot.
/// </summary>
public class Spot
{
    /// <summary>
    /// Gets or sets the unique identifier for the spot.
    /// </summary>
    public Guid SpotId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this spot.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the name of the spot.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the location type.
    /// </summary>
    public LocationType LocationType { get; set; }

    /// <summary>
    /// Gets or sets the latitude coordinate.
    /// </summary>
    public decimal? Latitude { get; set; }

    /// <summary>
    /// Gets or sets the longitude coordinate.
    /// </summary>
    public decimal? Longitude { get; set; }

    /// <summary>
    /// Gets or sets the description of the spot.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the water body name (lake, river, etc.).
    /// </summary>
    public string? WaterBodyName { get; set; }

    /// <summary>
    /// Gets or sets optional directions to the spot.
    /// </summary>
    public string? Directions { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is a favorite spot.
    /// </summary>
    public bool IsFavorite { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the collection of trips to this spot.
    /// </summary>
    public ICollection<Trip> Trips { get; set; } = new List<Trip>();

    /// <summary>
    /// Toggles the favorite status of this spot.
    /// </summary>
    public void ToggleFavorite()
    {
        IsFavorite = !IsFavorite;
    }

    /// <summary>
    /// Gets the total number of trips to this spot.
    /// </summary>
    /// <returns>The trip count.</returns>
    public int GetTripCount()
    {
        return Trips?.Count ?? 0;
    }
}
