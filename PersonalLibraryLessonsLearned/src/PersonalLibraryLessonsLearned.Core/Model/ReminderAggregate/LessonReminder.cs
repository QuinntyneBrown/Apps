// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalLibraryLessonsLearned.Core;

/// <summary>
/// Represents a reminder to review a lesson.
/// </summary>
public class LessonReminder
{
    /// <summary>
    /// Gets or sets the unique identifier for the reminder.
    /// </summary>
    public Guid LessonReminderId { get; set; }

    /// <summary>
    /// Gets or sets the lesson ID this reminder belongs to.
    /// </summary>
    public Guid LessonId { get; set; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the reminder date and time.
    /// </summary>
    public DateTime ReminderDateTime { get; set; }

    /// <summary>
    /// Gets or sets the reminder message.
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the reminder has been sent.
    /// </summary>
    public bool IsSent { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the reminder has been dismissed.
    /// </summary>
    public bool IsDismissed { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the navigation property to the lesson.
    /// </summary>
    public Lesson? Lesson { get; set; }

    /// <summary>
    /// Marks the reminder as sent.
    /// </summary>
    public void MarkAsSent()
    {
        IsSent = true;
    }

    /// <summary>
    /// Dismisses the reminder.
    /// </summary>
    public void Dismiss()
    {
        IsDismissed = true;
    }

    /// <summary>
    /// Checks if the reminder is due.
    /// </summary>
    /// <returns>True if due; otherwise, false.</returns>
    public bool IsDue()
    {
        return !IsSent && !IsDismissed && ReminderDateTime <= DateTime.UtcNow;
    }
}
