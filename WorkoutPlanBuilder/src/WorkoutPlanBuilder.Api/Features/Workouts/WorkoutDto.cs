// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using WorkoutPlanBuilder.Core;

namespace WorkoutPlanBuilder.Api.Features.Workouts;

/// <summary>
/// Data transfer object for Workout.
/// </summary>
public class WorkoutDto
{
    public Guid WorkoutId { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int TargetDurationMinutes { get; set; }
    public int DifficultyLevel { get; set; }
    public string? Goal { get; set; }
    public bool IsTemplate { get; set; }
    public bool IsActive { get; set; }
    public string? ScheduledDays { get; set; }
    public DateTime CreatedAt { get; set; }
}

/// <summary>
/// Extension methods for converting Workout entities to DTOs.
/// </summary>
public static class WorkoutExtensions
{
    /// <summary>
    /// Converts a Workout entity to a WorkoutDto.
    /// </summary>
    /// <param name="workout">The workout entity.</param>
    /// <returns>The workout DTO.</returns>
    public static WorkoutDto ToDto(this Workout workout)
    {
        return new WorkoutDto
        {
            WorkoutId = workout.WorkoutId,
            UserId = workout.UserId,
            Name = workout.Name,
            Description = workout.Description,
            TargetDurationMinutes = workout.TargetDurationMinutes,
            DifficultyLevel = workout.DifficultyLevel,
            Goal = workout.Goal,
            IsTemplate = workout.IsTemplate,
            IsActive = workout.IsActive,
            ScheduledDays = workout.ScheduledDays,
            CreatedAt = workout.CreatedAt
        };
    }
}
