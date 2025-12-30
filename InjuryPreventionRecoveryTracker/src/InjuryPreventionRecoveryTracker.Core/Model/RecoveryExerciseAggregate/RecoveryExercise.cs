// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InjuryPreventionRecoveryTracker.Core;

/// <summary>
/// Represents a recovery exercise prescribed for an injury.
/// </summary>
public class RecoveryExercise
{
    /// <summary>
    /// Gets or sets the unique identifier for the recovery exercise.
    /// </summary>
    public Guid RecoveryExerciseId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this recovery exercise.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the injury ID this exercise is for.
    /// </summary>
    public Guid InjuryId { get; set; }

    /// <summary>
    /// Gets or sets the name of the exercise.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description and instructions.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the prescribed frequency (e.g., 3 times daily).
    /// </summary>
    public string Frequency { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sets and repetitions.
    /// </summary>
    public string? SetsAndReps { get; set; }

    /// <summary>
    /// Gets or sets the duration in minutes if applicable.
    /// </summary>
    public int? DurationMinutes { get; set; }

    /// <summary>
    /// Gets or sets special instructions or precautions.
    /// </summary>
    public string? Instructions { get; set; }

    /// <summary>
    /// Gets or sets the last time the exercise was completed.
    /// </summary>
    public DateTime? LastCompleted { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the exercise is currently active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the associated injury.
    /// </summary>
    public Injury? Injury { get; set; }

    /// <summary>
    /// Marks the exercise as completed now.
    /// </summary>
    public void MarkCompleted()
    {
        LastCompleted = DateTime.UtcNow;
    }

    /// <summary>
    /// Checks if the exercise was completed today.
    /// </summary>
    /// <returns>True if completed today; otherwise, false.</returns>
    public bool WasCompletedToday()
    {
        return LastCompleted.HasValue && LastCompleted.Value.Date == DateTime.UtcNow.Date;
    }
}
