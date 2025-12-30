// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MorningRoutineBuilder.Core;

/// <summary>
/// Represents the type of a routine task.
/// </summary>
public enum TaskType
{
    /// <summary>
    /// Physical exercise or workout.
    /// </summary>
    Exercise = 0,

    /// <summary>
    /// Meditation or mindfulness practice.
    /// </summary>
    Meditation = 1,

    /// <summary>
    /// Journaling or writing.
    /// </summary>
    Journaling = 2,

    /// <summary>
    /// Reading activity.
    /// </summary>
    Reading = 3,

    /// <summary>
    /// Breakfast or meal preparation.
    /// </summary>
    Breakfast = 4,

    /// <summary>
    /// Hygiene and grooming.
    /// </summary>
    Hygiene = 5,

    /// <summary>
    /// Planning and organization.
    /// </summary>
    Planning = 6,

    /// <summary>
    /// Gratitude practice.
    /// </summary>
    Gratitude = 7,

    /// <summary>
    /// Learning or skill development.
    /// </summary>
    Learning = 8,

    /// <summary>
    /// Other custom task type.
    /// </summary>
    Other = 9,
}
