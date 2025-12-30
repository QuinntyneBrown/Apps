// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InjuryPreventionRecoveryTracker.Core;

namespace InjuryPreventionRecoveryTracker.Api;

/// <summary>
/// Data transfer object for RecoveryExercise.
/// </summary>
public record RecoveryExerciseDto
{
    /// <summary>
    /// Gets or sets the recovery exercise ID.
    /// </summary>
    public Guid RecoveryExerciseId { get; init; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets or sets the injury ID.
    /// </summary>
    public Guid InjuryId { get; init; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Gets or sets the frequency.
    /// </summary>
    public string Frequency { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the sets and reps.
    /// </summary>
    public string? SetsAndReps { get; init; }

    /// <summary>
    /// Gets or sets the duration minutes.
    /// </summary>
    public int? DurationMinutes { get; init; }

    /// <summary>
    /// Gets or sets the instructions.
    /// </summary>
    public string? Instructions { get; init; }

    /// <summary>
    /// Gets or sets the last completed date.
    /// </summary>
    public DateTime? LastCompleted { get; init; }

    /// <summary>
    /// Gets or sets a value indicating whether the exercise is active.
    /// </summary>
    public bool IsActive { get; init; }

    /// <summary>
    /// Gets or sets the created timestamp.
    /// </summary>
    public DateTime CreatedAt { get; init; }
}

/// <summary>
/// Extension methods for RecoveryExercise.
/// </summary>
public static class RecoveryExerciseExtensions
{
    /// <summary>
    /// Converts a RecoveryExercise to a DTO.
    /// </summary>
    /// <param name="exercise">The recovery exercise.</param>
    /// <returns>The DTO.</returns>
    public static RecoveryExerciseDto ToDto(this RecoveryExercise exercise)
    {
        return new RecoveryExerciseDto
        {
            RecoveryExerciseId = exercise.RecoveryExerciseId,
            UserId = exercise.UserId,
            InjuryId = exercise.InjuryId,
            Name = exercise.Name,
            Description = exercise.Description,
            Frequency = exercise.Frequency,
            SetsAndReps = exercise.SetsAndReps,
            DurationMinutes = exercise.DurationMinutes,
            Instructions = exercise.Instructions,
            LastCompleted = exercise.LastCompleted,
            IsActive = exercise.IsActive,
            CreatedAt = exercise.CreatedAt,
        };
    }
}
