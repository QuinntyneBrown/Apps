// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FocusSessionTracker.Core;

/// <summary>
/// Event raised when a focus session is completed.
/// </summary>
public record SessionCompletedEvent
{
    /// <summary>
    /// Gets the session ID.
    /// </summary>
    public Guid FocusSessionId { get; init; }

    /// <summary>
    /// Gets the actual duration in minutes.
    /// </summary>
    public double ActualDurationMinutes { get; init; }

    /// <summary>
    /// Gets the distraction count.
    /// </summary>
    public int DistractionCount { get; init; }

    /// <summary>
    /// Gets the focus score.
    /// </summary>
    public int? FocusScore { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
