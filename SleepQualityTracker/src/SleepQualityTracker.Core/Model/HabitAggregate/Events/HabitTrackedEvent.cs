// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SleepQualityTracker.Core;

/// <summary>
/// Event raised when a habit is tracked.
/// </summary>
public record HabitTrackedEvent
{
    /// <summary>
    /// Gets the habit ID.
    /// </summary>
    public Guid HabitId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the habit name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the habit type.
    /// </summary>
    public string HabitType { get; init; } = string.Empty;

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
