// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FocusSessionTracker.Core;

/// <summary>
/// Event raised when a focus session is started.
/// </summary>
public record SessionStartedEvent
{
    /// <summary>
    /// Gets the session ID.
    /// </summary>
    public Guid FocusSessionId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the session type.
    /// </summary>
    public SessionType SessionType { get; init; }

    /// <summary>
    /// Gets the session name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the planned duration in minutes.
    /// </summary>
    public int PlannedDurationMinutes { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
