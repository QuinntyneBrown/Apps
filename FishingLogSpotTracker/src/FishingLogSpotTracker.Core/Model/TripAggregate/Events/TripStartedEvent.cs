// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FishingLogSpotTracker.Core;

/// <summary>
/// Event raised when a fishing trip is started.
/// </summary>
public record TripStartedEvent
{
    /// <summary>
    /// Gets the trip ID.
    /// </summary>
    public Guid TripId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the spot ID.
    /// </summary>
    public Guid? SpotId { get; init; }

    /// <summary>
    /// Gets the trip date.
    /// </summary>
    public DateTime TripDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
