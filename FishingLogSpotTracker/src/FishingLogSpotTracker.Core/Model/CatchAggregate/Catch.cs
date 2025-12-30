// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FishingLogSpotTracker.Core;

/// <summary>
/// Represents a fish catch.
/// </summary>
public class Catch
{
    /// <summary>
    /// Gets or sets the unique identifier for the catch.
    /// </summary>
    public Guid CatchId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who made this catch.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the trip ID associated with this catch.
    /// </summary>
    public Guid TripId { get; set; }

    /// <summary>
    /// Gets or sets the trip associated with this catch.
    /// </summary>
    public Trip? Trip { get; set; }

    /// <summary>
    /// Gets or sets the fish species.
    /// </summary>
    public FishSpecies Species { get; set; }

    /// <summary>
    /// Gets or sets the length of the fish in inches.
    /// </summary>
    public decimal? Length { get; set; }

    /// <summary>
    /// Gets or sets the weight of the fish in pounds.
    /// </summary>
    public decimal? Weight { get; set; }

    /// <summary>
    /// Gets or sets the time when the fish was caught.
    /// </summary>
    public DateTime CatchTime { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the bait or lure used.
    /// </summary>
    public string? BaitUsed { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the fish was released.
    /// </summary>
    public bool WasReleased { get; set; }

    /// <summary>
    /// Gets or sets optional notes about the catch.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the photo URL of the catch.
    /// </summary>
    public string? PhotoUrl { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Marks the fish as released.
    /// </summary>
    public void MarkAsReleased()
    {
        WasReleased = true;
    }
}
