// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RunningLogRaceTracker.Core;

/// <summary>
/// Event raised when a new run is logged.
/// </summary>
public record RunLoggedEvent
{
    /// <summary>
    /// Gets the run ID.
    /// </summary>
    public Guid RunId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the distance in kilometers.
    /// </summary>
    public decimal Distance { get; init; }

    /// <summary>
    /// Gets the duration in minutes.
    /// </summary>
    public int DurationMinutes { get; init; }

    /// <summary>
    /// Gets the completed timestamp.
    /// </summary>
    public DateTime CompletedAt { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
