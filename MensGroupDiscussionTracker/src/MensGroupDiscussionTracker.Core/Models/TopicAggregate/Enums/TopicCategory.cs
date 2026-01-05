// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MensGroupDiscussionTracker.Core;

/// <summary>
/// Represents the category of a discussion topic.
/// </summary>
public enum TopicCategory
{
    /// <summary>
    /// Faith and spirituality topics.
    /// </summary>
    FaithAndSpirituality = 0,

    /// <summary>
    /// Relationships and family topics.
    /// </summary>
    RelationshipsAndFamily = 1,

    /// <summary>
    /// Career and work topics.
    /// </summary>
    CareerAndWork = 2,

    /// <summary>
    /// Personal growth topics.
    /// </summary>
    PersonalGrowth = 3,

    /// <summary>
    /// Mental health topics.
    /// </summary>
    MentalHealth = 4,

    /// <summary>
    /// Physical health and fitness topics.
    /// </summary>
    PhysicalHealth = 5,

    /// <summary>
    /// Financial topics.
    /// </summary>
    Financial = 6,

    /// <summary>
    /// Community and service topics.
    /// </summary>
    CommunityAndService = 7,

    /// <summary>
    /// Leadership topics.
    /// </summary>
    Leadership = 8,

    /// <summary>
    /// Other or general topics.
    /// </summary>
    Other = 9,
}
