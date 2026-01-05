// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MorningRoutineBuilder.Core;

/// <summary>
/// Event raised when a streak is extended.
/// </summary>
public record StreakExtendedEvent
{
    /// <summary>
    /// Gets the streak ID.
    /// </summary>
    public Guid StreakId { get; init; }

    /// <summary>
    /// Gets the routine ID.
    /// </summary>
    public Guid RoutineId { get; init; }

    /// <summary>
    /// Gets the current streak count.
    /// </summary>
    public int CurrentStreak { get; init; }

    /// <summary>
    /// Gets the longest streak.
    /// </summary>
    public int LongestStreak { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
