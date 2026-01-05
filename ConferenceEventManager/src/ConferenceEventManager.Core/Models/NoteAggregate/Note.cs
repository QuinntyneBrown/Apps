// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ConferenceEventManager.Core;

/// <summary>
/// Represents a note from an event.
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
    /// Gets or sets optional tags.
    /// </summary>
    public List<string> Tags { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the event.
    /// </summary>
    public Event? Event { get; set; }

    /// <summary>
    /// Adds a tag to this note.
    /// </summary>
    /// <param name="tag">The tag to add.</param>
    public void AddTag(string tag)
    {
        if (!Tags.Contains(tag, StringComparer.OrdinalIgnoreCase))
        {
            Tags.Add(tag);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
