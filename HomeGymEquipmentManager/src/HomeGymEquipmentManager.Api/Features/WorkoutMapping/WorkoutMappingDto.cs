// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeGymEquipmentManager.Api.Features.WorkoutMapping;

public class WorkoutMappingDto
{
    public Guid WorkoutMappingId { get; set; }
    public Guid UserId { get; set; }
    public Guid EquipmentId { get; set; }
    public string ExerciseName { get; set; } = string.Empty;
    public string? MuscleGroup { get; set; }
    public string? Instructions { get; set; }
    public bool IsFavorite { get; set; }
    public DateTime CreatedAt { get; set; }

    public static WorkoutMappingDto FromEntity(Core.WorkoutMapping workoutMapping)
    {
        return new WorkoutMappingDto
        {
            WorkoutMappingId = workoutMapping.WorkoutMappingId,
            UserId = workoutMapping.UserId,
            EquipmentId = workoutMapping.EquipmentId,
            ExerciseName = workoutMapping.ExerciseName,
            MuscleGroup = workoutMapping.MuscleGroup,
            Instructions = workoutMapping.Instructions,
            IsFavorite = workoutMapping.IsFavorite,
            CreatedAt = workoutMapping.CreatedAt
        };
    }
}
