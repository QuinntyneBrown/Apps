// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WorkoutPlanBuilder.Core;

/// <summary>
/// Represents a workout plan or routine.
/// </summary>
public class Workout
{
    /// <summary>
    /// Gets or sets the unique identifier for the workout.
    /// </summary>
    public Guid WorkoutId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this workout.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the name of the workout.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the workout.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the target duration in minutes.
    /// </summary>
    public int TargetDurationMinutes { get; set; }

    /// <summary>
    /// Gets or sets the difficulty level (1-5).
    /// </summary>
    public int DifficultyLevel { get; set; }

    /// <summary>
    /// Gets or sets the primary goal (e.g., Strength, Weight Loss, Endurance).
    /// </summary>
    public string? Goal { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the workout is a template.
    /// </summary>
    public bool IsTemplate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the workout is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the scheduled days of the week (JSON array).
    /// </summary>
    public string? ScheduledDays { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the collection of exercises in this workout.
    /// </summary>
    public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();

    /// <summary>
    /// Gets or sets the collection of progress records for this workout.
    /// </summary>
    public ICollection<ProgressRecord> ProgressRecords { get; set; } = new List<ProgressRecord>();

    /// <summary>
    /// Calculates the total number of exercises in the workout.
    /// </summary>
    /// <returns>The number of exercises.</returns>
    public int GetExerciseCount()
    {
        return Exercises.Count;
    }

    /// <summary>
    /// Checks if the workout has been completed recently (within the last 7 days).
    /// </summary>
    /// <returns>True if recently completed; otherwise, false.</returns>
    public bool HasRecentCompletion()
    {
        return ProgressRecords.Any(pr => pr.CompletedAt >= DateTime.UtcNow.AddDays(-7));
    }

    /// <summary>
    /// Toggles the active status of the workout.
    /// </summary>
    public void ToggleActive()
    {
        IsActive = !IsActive;
    }
}
