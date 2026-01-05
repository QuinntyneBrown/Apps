// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WorkoutPlanBuilder.Core;

/// <summary>
/// Event raised when a new exercise is created.
/// </summary>
public record ExerciseCreatedEvent
{
    /// <summary>
    /// Gets the exercise ID.
    /// </summary>
    public Guid ExerciseId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the exercise name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the exercise type.
    /// </summary>
    public ExerciseType ExerciseType { get; init; }

    /// <summary>
    /// Gets the primary muscle group.
    /// </summary>
    public MuscleGroup PrimaryMuscleGroup { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
