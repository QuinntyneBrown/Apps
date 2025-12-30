// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MeetingNotesActionItemTracker.Core;

/// <summary>
/// Event raised when an action item is created.
/// </summary>
public record ActionItemCreatedEvent
{
    /// <summary>
    /// Gets the action item ID.
    /// </summary>
    public Guid ActionItemId { get; init; }

    /// <summary>
    /// Gets the meeting ID.
    /// </summary>
    public Guid MeetingId { get; init; }

    /// <summary>
    /// Gets the description.
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// Gets the priority.
    /// </summary>
    public Priority Priority { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
