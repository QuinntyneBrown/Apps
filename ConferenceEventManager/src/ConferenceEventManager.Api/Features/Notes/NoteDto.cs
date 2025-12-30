// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Core;

namespace ConferenceEventManager.Api.Features.Notes;

/// <summary>
/// Data transfer object for Note.
/// </summary>
public class NoteDto
{
    /// <summary>
    /// Gets or sets the note ID.
    /// </summary>
    public Guid NoteId { get; set; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the event ID.
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Gets or sets the note content.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category or topic.
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Gets or sets tags.
    /// </summary>
    public List<string> Tags { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Maps a Note entity to NoteDto.
    /// </summary>
    public static NoteDto FromNote(Note note)
    {
        return new NoteDto
        {
            NoteId = note.NoteId,
            UserId = note.UserId,
            EventId = note.EventId,
            Content = note.Content,
            Category = note.Category,
            Tags = note.Tags.ToList(),
            CreatedAt = note.CreatedAt,
            UpdatedAt = note.UpdatedAt
        };
    }
}
