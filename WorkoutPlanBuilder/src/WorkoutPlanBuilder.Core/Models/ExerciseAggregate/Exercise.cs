// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WorkoutPlanBuilder.Core;

/// <summary>
/// Represents an exercise in the workout plan.
/// </summary>
public class Exercise
{
    /// <summary>
    /// Gets or sets the unique identifier for the exercise.
    /// </summary>
    public Guid ExerciseId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this exercise.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the name of the exercise.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the exercise.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the exercise type.
    /// </summary>
    public ExerciseType ExerciseType { get; set; }

    /// <summary>
    /// Gets or sets the primary muscle group targeted.
    /// </summary>
    public MuscleGroup PrimaryMuscleGroup { get; set; }

    /// <summary>
    /// Gets or sets the secondary muscle groups targeted.
    /// </summary>
    public string? SecondaryMuscleGroups { get; set; }

    /// <summary>
    /// Gets or sets the equipment needed.
    /// </summary>
    public string? Equipment { get; set; }

    /// <summary>
    /// Gets or sets the difficulty level (1-5).
    /// </summary>
    public int DifficultyLevel { get; set; }

    /// <summary>
    /// Gets or sets optional video URL for demonstration.
    /// </summary>
    public string? VideoUrl { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the exercise is custom created by user.
    /// </summary>
    public bool IsCustom { get; set; } = true;

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Checks if the exercise requires equipment.
    /// </summary>
    /// <returns>True if equipment is required; otherwise, false.</returns>
    public bool RequiresEquipment()
    {
        return !string.IsNullOrWhiteSpace(Equipment) &&
               !Equipment.Equals("None", StringComparison.OrdinalIgnoreCase) &&
               !Equipment.Equals("Bodyweight", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Checks if the exercise is suitable for beginners.
    /// </summary>
    /// <returns>True if suitable for beginners; otherwise, false.</returns>
    public bool IsBeginner()
    {
        return DifficultyLevel <= 2;
    }
}
