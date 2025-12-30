// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalLibraryLessonsLearned.Core;

/// <summary>
/// Event raised when a lesson is created.
/// </summary>
public record LessonCreatedEvent
{
    /// <summary>
    /// Gets the lesson ID.
    /// </summary>
    public Guid LessonId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the lesson title.
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// Gets the category.
    /// </summary>
    public LessonCategory Category { get; init; }

    /// <summary>
    /// Gets the date learned.
    /// </summary>
    public DateTime DateLearned { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
