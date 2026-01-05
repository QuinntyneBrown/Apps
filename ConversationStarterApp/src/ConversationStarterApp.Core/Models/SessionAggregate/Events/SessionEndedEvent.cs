// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ConversationStarterApp.Core;

/// <summary>
/// Event raised when a conversation session is ended.
/// </summary>
public record SessionEndedEvent
{
    /// <summary>
    /// Gets the session ID.
    /// </summary>
    public Guid SessionId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the end time.
    /// </summary>
    public DateTime EndTime { get; init; }

    /// <summary>
    /// Gets the duration.
    /// </summary>
    public TimeSpan? Duration { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
