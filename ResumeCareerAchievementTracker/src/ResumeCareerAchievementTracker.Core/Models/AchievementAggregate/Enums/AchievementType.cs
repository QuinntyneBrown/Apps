// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ResumeCareerAchievementTracker.Core;

/// <summary>
/// Represents the type of an achievement.
/// </summary>
public enum AchievementType
{
    /// <summary>
    /// A professional award or recognition.
    /// </summary>
    Award = 0,

    /// <summary>
    /// A professional certification.
    /// </summary>
    Certification = 1,

    /// <summary>
    /// A publication (article, book, paper).
    /// </summary>
    Publication = 2,

    /// <summary>
    /// A presentation or speaking engagement.
    /// </summary>
    Presentation = 3,

    /// <summary>
    /// A project milestone or completion.
    /// </summary>
    ProjectMilestone = 4,

    /// <summary>
    /// A promotion or career advancement.
    /// </summary>
    Promotion = 5,

    /// <summary>
    /// Revenue or cost savings achievement.
    /// </summary>
    FinancialImpact = 6,

    /// <summary>
    /// A leadership accomplishment.
    /// </summary>
    Leadership = 7,

    /// <summary>
    /// Innovation or process improvement.
    /// </summary>
    Innovation = 8,

    /// <summary>
    /// Other type of achievement.
    /// </summary>
    Other = 9,
}
