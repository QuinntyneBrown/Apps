// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KnowledgeBaseSecondBrain.Core;

/// <summary>
/// Represents a tag for categorizing notes.
/// </summary>
public class Tag
{
    /// <summary>
    /// Gets or sets the unique identifier for the tag.
    /// </summary>
    public Guid TagId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this tag.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tag name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the tag color (hex code).
    /// </summary>
    public string? Color { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the collection of notes with this tag.
    /// </summary>
    public ICollection<Note> Notes { get; set; } = new List<Note>();

    /// <summary>
    /// Gets the count of notes with this tag.
    /// </summary>
    /// <returns>The number of notes.</returns>
    public int GetNoteCount()
    {
        return Notes.Count;
    }
}
