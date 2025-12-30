// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalLibraryLessonsLearned.Core;

/// <summary>
/// Event raised when a reminder is scheduled.
/// </summary>
public record ReminderScheduledEvent
{
    /// <summary>
    /// Gets the reminder ID.
    /// </summary>
    public Guid LessonReminderId { get; init; }

    /// <summary>
    /// Gets the lesson ID.
    /// </summary>
    public Guid LessonId { get; init; }

    /// <summary>
    /// Gets the reminder date and time.
    /// </summary>
    public DateTime ReminderDateTime { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
