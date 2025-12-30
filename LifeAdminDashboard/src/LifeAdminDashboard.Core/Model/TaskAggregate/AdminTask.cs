// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace LifeAdminDashboard.Core;

/// <summary>
/// Represents an administrative task.
/// </summary>
public class AdminTask
{
    /// <summary>
    /// Gets or sets the unique identifier for the task.
    /// </summary>
    public Guid AdminTaskId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this task.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the task title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the task description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the task category.
    /// </summary>
    public TaskCategory Category { get; set; }

    /// <summary>
    /// Gets or sets the task priority.
    /// </summary>
    public TaskPriority Priority { get; set; }

    /// <summary>
    /// Gets or sets the due date.
    /// </summary>
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the task is completed.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// Gets or sets the completion date.
    /// </summary>
    public DateTime? CompletionDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the task is recurring.
    /// </summary>
    public bool IsRecurring { get; set; }

    /// <summary>
    /// Gets or sets the recurrence pattern (daily, weekly, monthly, yearly).
    /// </summary>
    public string? RecurrencePattern { get; set; }

    /// <summary>
    /// Gets or sets optional notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Marks the task as completed.
    /// </summary>
    public void Complete()
    {
        IsCompleted = true;
        CompletionDate = DateTime.UtcNow;
    }

    /// <summary>
    /// Checks if the task is overdue.
    /// </summary>
    /// <returns>True if overdue; otherwise, false.</returns>
    public bool IsOverdue()
    {
        return !IsCompleted && DueDate.HasValue && DueDate.Value < DateTime.UtcNow;
    }

    /// <summary>
    /// Snoozes the task by adding days to the due date.
    /// </summary>
    /// <param name="days">The number of days to snooze.</param>
    public void Snooze(int days)
    {
        if (DueDate.HasValue)
        {
            DueDate = DueDate.Value.AddDays(days);
        }
    }
}
