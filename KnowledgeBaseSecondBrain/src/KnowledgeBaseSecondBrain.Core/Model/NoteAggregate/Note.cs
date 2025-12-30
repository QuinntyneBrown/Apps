// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KnowledgeBaseSecondBrain.Core;

/// <summary>
/// Represents a note in the knowledge base.
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
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the note title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the note content.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the note type.
    /// </summary>
    public NoteType NoteType { get; set; }

    /// <summary>
    /// Gets or sets the parent note ID for hierarchical organization.
    /// </summary>
    public Guid? ParentNoteId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the note is favorited.
    /// </summary>
    public bool IsFavorite { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the note is archived.
    /// </summary>
    public bool IsArchived { get; set; }

    /// <summary>
    /// Gets or sets the last modified timestamp.
    /// </summary>
    public DateTime LastModifiedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the navigation property to the parent note.
    /// </summary>
    public Note? ParentNote { get; set; }

    /// <summary>
    /// Gets or sets the collection of child notes.
    /// </summary>
    public ICollection<Note> ChildNotes { get; set; } = new List<Note>();

    /// <summary>
    /// Gets or sets the collection of tags associated with this note.
    /// </summary>
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();

    /// <summary>
    /// Gets or sets the collection of outgoing links from this note.
    /// </summary>
    public ICollection<NoteLink> OutgoingLinks { get; set; } = new List<NoteLink>();

    /// <summary>
    /// Gets or sets the collection of incoming links to this note.
    /// </summary>
    public ICollection<NoteLink> IncomingLinks { get; set; } = new List<NoteLink>();

    /// <summary>
    /// Updates the note content and last modified timestamp.
    /// </summary>
    /// <param name="content">The new content.</param>
    public void UpdateContent(string content)
    {
        Content = content;
        LastModifiedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Archives the note.
    /// </summary>
    public void Archive()
    {
        IsArchived = true;
        LastModifiedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Unarchives the note.
    /// </summary>
    public void Unarchive()
    {
        IsArchived = false;
        LastModifiedAt = DateTime.UtcNow;
    }
}
