// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MeetingNotesActionItemTracker.Core;

/// <summary>
/// Event raised when a new meeting is created.
/// </summary>
public record MeetingCreatedEvent
{
    /// <summary>
    /// Gets the meeting ID.
    /// </summary>
    public Guid MeetingId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the meeting title.
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// Gets the meeting date and time.
    /// </summary>
    public DateTime MeetingDateTime { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
