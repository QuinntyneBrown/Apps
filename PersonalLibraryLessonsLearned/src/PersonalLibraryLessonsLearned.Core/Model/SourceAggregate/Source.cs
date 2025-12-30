// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalLibraryLessonsLearned.Core;

/// <summary>
/// Represents a source from which lessons are learned (book, article, video, etc.).
/// </summary>
public class Source
{
    /// <summary>
    /// Gets or sets the unique identifier for the source.
    /// </summary>
    public Guid SourceId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this source.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the source title or name.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the author or creator.
    /// </summary>
    public string? Author { get; set; }

    /// <summary>
    /// Gets or sets the type of source (Book, Article, Video, Podcast, etc.).
    /// </summary>
    public string SourceType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the URL or reference link.
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// Gets or sets the date when the source was consumed.
    /// </summary>
    public DateTime? DateConsumed { get; set; }

    /// <summary>
    /// Gets or sets optional notes about the source.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the collection of lessons from this source.
    /// </summary>
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    /// <summary>
    /// Gets the count of lessons from this source.
    /// </summary>
    /// <returns>The number of lessons.</returns>
    public int GetLessonCount()
    {
        return Lessons.Count;
    }
}
