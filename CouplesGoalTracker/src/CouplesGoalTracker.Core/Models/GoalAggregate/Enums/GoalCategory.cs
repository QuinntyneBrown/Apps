// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CouplesGoalTracker.Core;

/// <summary>
/// Represents the category of a relationship goal.
/// </summary>
public enum GoalCategory
{
    /// <summary>
    /// Communication goals.
    /// </summary>
    Communication = 0,

    /// <summary>
    /// Intimacy goals.
    /// </summary>
    Intimacy = 1,

    /// <summary>
    /// Financial goals.
    /// </summary>
    Financial = 2,

    /// <summary>
    /// Health and wellness goals.
    /// </summary>
    HealthAndWellness = 3,

    /// <summary>
    /// Adventure and travel goals.
    /// </summary>
    AdventureAndTravel = 4,

    /// <summary>
    /// Personal growth goals.
    /// </summary>
    PersonalGrowth = 5,

    /// <summary>
    /// Family planning goals.
    /// </summary>
    FamilyPlanning = 6,

    /// <summary>
    /// Quality time goals.
    /// </summary>
    QualityTime = 7,

    /// <summary>
    /// Career and education goals.
    /// </summary>
    CareerAndEducation = 8,

    /// <summary>
    /// Other or custom goals.
    /// </summary>
    Other = 9,
}
