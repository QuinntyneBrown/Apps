// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WorkoutPlanBuilder.Core;

/// <summary>
/// Represents a progress record for a completed workout.
/// </summary>
public class ProgressRecord
{
    /// <summary>
    /// Gets or sets the unique identifier for the progress record.
    /// </summary>
    public Guid ProgressRecordId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this record.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the workout ID.
    /// </summary>
    public Guid WorkoutId { get; set; }

    /// <summary>
    /// Gets or sets the actual duration in minutes.
    /// </summary>
    public int ActualDurationMinutes { get; set; }

    /// <summary>
    /// Gets or sets the total calories burned.
    /// </summary>
    public int? CaloriesBurned { get; set; }

    /// <summary>
    /// Gets or sets the performance rating (1-5).
    /// </summary>
    public int? PerformanceRating { get; set; }

    /// <summary>
    /// Gets or sets optional notes about the workout session.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the exercise details (sets, reps, weights in JSON format).
    /// </summary>
    public string? ExerciseDetails { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the workout was completed.
    /// </summary>
    public DateTime CompletedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the associated workout.
    /// </summary>
    public Workout? Workout { get; set; }

    /// <summary>
    /// Checks if the workout session was completed recently (today).
    /// </summary>
    /// <returns>True if completed today; otherwise, false.</returns>
    public bool IsCompletedToday()
    {
        return CompletedAt.Date == DateTime.UtcNow.Date;
    }

    /// <summary>
    /// Checks if the workout performance was excellent (rating 4 or 5).
    /// </summary>
    /// <returns>True if excellent; otherwise, false.</returns>
    public bool WasExcellent()
    {
        return PerformanceRating.HasValue && PerformanceRating.Value >= 4;
    }
}
