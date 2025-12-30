// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using WorkoutPlanBuilder.Core;

namespace WorkoutPlanBuilder.Api.Features.Exercises;

/// <summary>
/// Data transfer object for Exercise.
/// </summary>
public class ExerciseDto
{
    public Guid ExerciseId { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ExerciseType ExerciseType { get; set; }
    public MuscleGroup PrimaryMuscleGroup { get; set; }
    public string? SecondaryMuscleGroups { get; set; }
    public string? Equipment { get; set; }
    public int DifficultyLevel { get; set; }
    public string? VideoUrl { get; set; }
    public bool IsCustom { get; set; }
    public DateTime CreatedAt { get; set; }
}

/// <summary>
/// Extension methods for converting Exercise entities to DTOs.
/// </summary>
public static class ExerciseExtensions
{
    /// <summary>
    /// Converts an Exercise entity to an ExerciseDto.
    /// </summary>
    /// <param name="exercise">The exercise entity.</param>
    /// <returns>The exercise DTO.</returns>
    public static ExerciseDto ToDto(this Exercise exercise)
    {
        return new ExerciseDto
        {
            ExerciseId = exercise.ExerciseId,
            UserId = exercise.UserId,
            Name = exercise.Name,
            Description = exercise.Description,
            ExerciseType = exercise.ExerciseType,
            PrimaryMuscleGroup = exercise.PrimaryMuscleGroup,
            SecondaryMuscleGroups = exercise.SecondaryMuscleGroups,
            Equipment = exercise.Equipment,
            DifficultyLevel = exercise.DifficultyLevel,
            VideoUrl = exercise.VideoUrl,
            IsCustom = exercise.IsCustom,
            CreatedAt = exercise.CreatedAt
        };
    }
}
