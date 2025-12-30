// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MeetingNotesActionItemTracker.Core;

/// <summary>
/// Represents a note from a meeting.
/// </summary>
public class Note
{
    /// <summary>
    /// Gets or sets the unique identifier for the note.
    /// </summary>
    public Guid NoteId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this note.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the meeting ID.
    /// </summary>
    public Guid MeetingId { get; set; }

    /// <summary>
    /// Gets or sets the note content.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the note category or topic.
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this note is important.
    /// </summary>
    public bool IsImportant { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the meeting.
    /// </summary>
    public Meeting? Meeting { get; set; }

    /// <summary>
    /// Toggles the important flag.
    /// </summary>
    public void ToggleImportant()
    {
        IsImportant = !IsImportant;
        UpdatedAt = DateTime.UtcNow;
    }
}
