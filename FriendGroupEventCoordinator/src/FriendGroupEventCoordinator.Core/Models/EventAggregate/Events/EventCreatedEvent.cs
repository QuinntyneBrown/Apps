// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FriendGroupEventCoordinator.Core;

/// <summary>
/// Event raised when a new event is created.
/// </summary>
public record EventCreatedEvent
{
    /// <summary>
    /// Gets the event ID.
    /// </summary>
    public Guid EventId { get; init; }

    /// <summary>
    /// Gets the group ID.
    /// </summary>
    public Guid GroupId { get; init; }

    /// <summary>
    /// Gets the user ID who created the event.
    /// </summary>
    public Guid CreatedByUserId { get; init; }

    /// <summary>
    /// Gets the title.
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// Gets the event type.
    /// </summary>
    public EventType EventType { get; init; }

    /// <summary>
    /// Gets the start date and time.
    /// </summary>
    public DateTime StartDateTime { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
