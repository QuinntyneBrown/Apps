// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SleepQualityTracker.Core;

/// <summary>
/// Event raised when a new sleep session is logged.
/// </summary>
public record SleepSessionLoggedEvent
{
    /// <summary>
    /// Gets the sleep session ID.
    /// </summary>
    public Guid SleepSessionId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the bedtime.
    /// </summary>
    public DateTime Bedtime { get; init; }

    /// <summary>
    /// Gets the wake time.
    /// </summary>
    public DateTime WakeTime { get; init; }

    /// <summary>
    /// Gets the sleep quality.
    /// </summary>
    public SleepQuality SleepQuality { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
