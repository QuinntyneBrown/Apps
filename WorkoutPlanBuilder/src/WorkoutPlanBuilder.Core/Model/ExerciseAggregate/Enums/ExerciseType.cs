// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WorkoutPlanBuilder.Core;

/// <summary>
/// Represents the type of exercise.
/// </summary>
public enum ExerciseType
{
    /// <summary>
    /// Strength training exercise.
    /// </summary>
    Strength = 0,

    /// <summary>
    /// Cardio exercise.
    /// </summary>
    Cardio = 1,

    /// <summary>
    /// Flexibility/stretching exercise.
    /// </summary>
    Flexibility = 2,

    /// <summary>
    /// Balance exercise.
    /// </summary>
    Balance = 3,

    /// <summary>
    /// Plyometric exercise.
    /// </summary>
    Plyometric = 4,

    /// <summary>
    /// High-intensity interval training.
    /// </summary>
    HIIT = 5,
}
