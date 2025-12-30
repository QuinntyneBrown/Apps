// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MorningRoutineBuilder.Core;

/// <summary>
/// Represents a log of routine completion.
/// </summary>
public class CompletionLog
{
    /// <summary>
    /// Gets or sets the unique identifier for the completion log.
    /// </summary>
    public Guid CompletionLogId { get; set; }

    /// <summary>
    /// Gets or sets the routine ID this log belongs to.
    /// </summary>
    public Guid RoutineId { get; set; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the completion date.
    /// </summary>
    public DateTime CompletionDate { get; set; }

    /// <summary>
    /// Gets or sets the actual start time.
    /// </summary>
    public DateTime? ActualStartTime { get; set; }

    /// <summary>
    /// Gets or sets the actual end time.
    /// </summary>
    public DateTime? ActualEndTime { get; set; }

    /// <summary>
    /// Gets or sets the number of tasks completed.
    /// </summary>
    public int TasksCompleted { get; set; }

    /// <summary>
    /// Gets or sets the total number of tasks.
    /// </summary>
    public int TotalTasks { get; set; }

    /// <summary>
    /// Gets or sets optional notes about the completion.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the mood or feeling rating (1-10).
    /// </summary>
    public int? MoodRating { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the navigation property to the routine.
    /// </summary>
    public Routine? Routine { get; set; }

    /// <summary>
    /// Calculates the completion percentage.
    /// </summary>
    /// <returns>The completion percentage.</returns>
    public double GetCompletionPercentage()
    {
        if (TotalTasks == 0)
        {
            return 0;
        }

        return (double)TasksCompleted / TotalTasks * 100;
    }

    /// <summary>
    /// Checks if the routine was fully completed.
    /// </summary>
    /// <returns>True if fully completed; otherwise, false.</returns>
    public bool IsFullyCompleted()
    {
        return TasksCompleted == TotalTasks;
    }

    /// <summary>
    /// Calculates the actual duration in minutes.
    /// </summary>
    /// <returns>The duration in minutes, or null if times not set.</returns>
    public double? GetActualDurationMinutes()
    {
        if (ActualStartTime == null || ActualEndTime == null)
        {
            return null;
        }

        return (ActualEndTime.Value - ActualStartTime.Value).TotalMinutes;
    }
}
