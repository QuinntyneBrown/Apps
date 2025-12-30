// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalLibraryLessonsLearned.Core;

/// <summary>
/// Represents a lesson learned from various sources.
/// </summary>
public class Lesson
{
    /// <summary>
    /// Gets or sets the unique identifier for the lesson.
    /// </summary>
    public Guid LessonId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this lesson.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the source ID.
    /// </summary>
    public Guid? SourceId { get; set; }

    /// <summary>
    /// Gets or sets the lesson title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the lesson content/description.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category of the lesson.
    /// </summary>
    public LessonCategory Category { get; set; }

    /// <summary>
    /// Gets or sets optional tags for categorization.
    /// </summary>
    public string? Tags { get; set; }

    /// <summary>
    /// Gets or sets the date when the lesson was learned.
    /// </summary>
    public DateTime DateLearned { get; set; }

    /// <summary>
    /// Gets or sets the application or action items.
    /// </summary>
    public string? Application { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the lesson has been applied.
    /// </summary>
    public bool IsApplied { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the navigation property to the source.
    /// </summary>
    public Source? Source { get; set; }

    /// <summary>
    /// Gets or sets the collection of reminders for this lesson.
    /// </summary>
    public ICollection<LessonReminder> Reminders { get; set; } = new List<LessonReminder>();

    /// <summary>
    /// Marks the lesson as applied.
    /// </summary>
    public void MarkAsApplied()
    {
        IsApplied = true;
    }

    /// <summary>
    /// Checks if the lesson has reminders.
    /// </summary>
    /// <returns>True if there are reminders; otherwise, false.</returns>
    public bool HasReminders()
    {
        return Reminders.Any();
    }
}
