// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WorkoutPlanBuilder.Core;

/// <summary>
/// Represents the muscle groups targeted by exercises.
/// </summary>
public enum MuscleGroup
{
    /// <summary>
    /// Chest muscles.
    /// </summary>
    Chest = 0,

    /// <summary>
    /// Back muscles.
    /// </summary>
    Back = 1,

    /// <summary>
    /// Leg muscles.
    /// </summary>
    Legs = 2,

    /// <summary>
    /// Shoulder muscles.
    /// </summary>
    Shoulders = 3,

    /// <summary>
    /// Arm muscles (biceps, triceps).
    /// </summary>
    Arms = 4,

    /// <summary>
    /// Core/abdominal muscles.
    /// </summary>
    Core = 5,

    /// <summary>
    /// Glute muscles.
    /// </summary>
    Glutes = 6,

    /// <summary>
    /// Full body workout.
    /// </summary>
    FullBody = 7,
}
