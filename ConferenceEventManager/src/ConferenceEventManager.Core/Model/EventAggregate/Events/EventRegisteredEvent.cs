// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ConferenceEventManager.Core;

/// <summary>
/// Event raised when registering for an event.
/// </summary>
public record EventRegisteredEvent
{
    /// <summary>
    /// Gets the event ID.
    /// </summary>
    public Guid EventId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the event name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the event start date.
    /// </summary>
    public DateTime StartDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
