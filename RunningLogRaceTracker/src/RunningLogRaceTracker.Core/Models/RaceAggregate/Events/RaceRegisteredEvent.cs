// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RunningLogRaceTracker.Core;

/// <summary>
/// Event raised when a race is registered.
/// </summary>
public record RaceRegisteredEvent
{
    /// <summary>
    /// Gets the race ID.
    /// </summary>
    public Guid RaceId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the race name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the race type.
    /// </summary>
    public RaceType RaceType { get; init; }

    /// <summary>
    /// Gets the race date.
    /// </summary>
    public DateTime RaceDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
