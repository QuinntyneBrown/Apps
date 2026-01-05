// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SkillDevelopmentTracker.Core;

/// <summary>
/// Represents the proficiency level of a skill.
/// </summary>
public enum ProficiencyLevel
{
    /// <summary>
    /// No experience or just starting.
    /// </summary>
    Beginner = 0,

    /// <summary>
    /// Basic understanding and limited experience.
    /// </summary>
    Novice = 1,

    /// <summary>
    /// Working knowledge and moderate experience.
    /// </summary>
    Intermediate = 2,

    /// <summary>
    /// Strong knowledge and extensive experience.
    /// </summary>
    Advanced = 3,

    /// <summary>
    /// Expert-level knowledge and mastery.
    /// </summary>
    Expert = 4,
}
