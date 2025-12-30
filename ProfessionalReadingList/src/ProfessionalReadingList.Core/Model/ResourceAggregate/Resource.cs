// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalReadingList.Core;

/// <summary>
/// Represents a reading resource (book, article, video, etc.).
/// </summary>
public class Resource
{
    /// <summary>
    /// Gets or sets the unique identifier for the resource.
    /// </summary>
    public Guid ResourceId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this resource.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the title of the resource.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the resource type.
    /// </summary>
    public ResourceType ResourceType { get; set; }

    /// <summary>
    /// Gets or sets the author(s).
    /// </summary>
    public string? Author { get; set; }

    /// <summary>
    /// Gets or sets the publisher or platform.
    /// </summary>
    public string? Publisher { get; set; }

    /// <summary>
    /// Gets or sets the publication date.
    /// </summary>
    public DateTime? PublicationDate { get; set; }

    /// <summary>
    /// Gets or sets the URL to the resource.
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// Gets or sets the ISBN (for books).
    /// </summary>
    public string? Isbn { get; set; }

    /// <summary>
    /// Gets or sets the total pages or length.
    /// </summary>
    public int? TotalPages { get; set; }

    /// <summary>
    /// Gets or sets the topics or tags.
    /// </summary>
    public List<string> Topics { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the date added to the reading list.
    /// </summary>
    public DateTime DateAdded { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets optional notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the reading progress for this resource.
    /// </summary>
    public ReadingProgress? ReadingProgress { get; set; }

    /// <summary>
    /// Gets or sets the collection of notes for this resource.
    /// </summary>
    public ICollection<Note> ResourceNotes { get; set; } = new List<Note>();

    /// <summary>
    /// Adds a topic to this resource.
    /// </summary>
    /// <param name="topic">The topic to add.</param>
    public void AddTopic(string topic)
    {
        if (!Topics.Contains(topic, StringComparer.OrdinalIgnoreCase))
        {
            Topics.Add(topic);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
