// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace LifeAdminDashboard.Core;

/// <summary>
/// Represents an important deadline.
/// </summary>
public class Deadline
{
    /// <summary>
    /// Gets or sets the unique identifier for the deadline.
    /// </summary>
    public Guid DeadlineId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this deadline.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the deadline title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the deadline description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the deadline date and time.
    /// </summary>
    public DateTime DeadlineDateTime { get; set; }

    /// <summary>
    /// Gets or sets the category or type.
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the deadline is completed.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// Gets or sets the completion date.
    /// </summary>
    public DateTime? CompletionDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether reminders are enabled.
    /// </summary>
    public bool RemindersEnabled { get; set; } = true;

    /// <summary>
    /// Gets or sets the reminder advance days.
    /// </summary>
    public int ReminderDaysAdvance { get; set; } = 7;

    /// <summary>
    /// Gets or sets optional notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Marks the deadline as completed.
    /// </summary>
    public void Complete()
    {
        IsCompleted = true;
        CompletionDate = DateTime.UtcNow;
    }

    /// <summary>
    /// Checks if the deadline is overdue.
    /// </summary>
    /// <returns>True if overdue; otherwise, false.</returns>
    public bool IsOverdue()
    {
        return !IsCompleted && DeadlineDateTime < DateTime.UtcNow;
    }

    /// <summary>
    /// Checks if a reminder should be sent.
    /// </summary>
    /// <returns>True if reminder is due; otherwise, false.</returns>
    public bool ShouldRemind()
    {
        if (!RemindersEnabled || IsCompleted)
        {
            return false;
        }

        var daysUntilDeadline = (DeadlineDateTime - DateTime.UtcNow).Days;
        return daysUntilDeadline <= ReminderDaysAdvance && daysUntilDeadline >= 0;
    }
}
