// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MeetingNotesActionItemTracker.Core;

/// <summary>
/// Event raised when a note is added.
/// </summary>
public record NoteAddedEvent
{
    /// <summary>
    /// Gets the note ID.
    /// </summary>
    public Guid NoteId { get; init; }

    /// <summary>
    /// Gets the meeting ID.
    /// </summary>
    public Guid MeetingId { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
