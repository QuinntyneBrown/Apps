// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FishingLogSpotTracker.Core;

/// <summary>
/// Event raised when a catch is logged.
/// </summary>
public record CatchLoggedEvent
{
    /// <summary>
    /// Gets the catch ID.
    /// </summary>
    public Guid CatchId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the trip ID.
    /// </summary>
    public Guid TripId { get; init; }

    /// <summary>
    /// Gets the fish species.
    /// </summary>
    public FishSpecies Species { get; init; }

    /// <summary>
    /// Gets the weight.
    /// </summary>
    public decimal? Weight { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
