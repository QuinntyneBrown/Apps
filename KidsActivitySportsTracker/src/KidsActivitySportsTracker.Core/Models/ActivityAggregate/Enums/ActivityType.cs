// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KidsActivitySportsTracker.Core;

/// <summary>
/// Represents the type of activity.
/// </summary>
public enum ActivityType
{
    /// <summary>
    /// Team sports.
    /// </summary>
    TeamSports = 0,

    /// <summary>
    /// Individual sports.
    /// </summary>
    IndividualSports = 1,

    /// <summary>
    /// Music lessons or activities.
    /// </summary>
    Music = 2,

    /// <summary>
    /// Art classes or activities.
    /// </summary>
    Art = 3,

    /// <summary>
    /// Academic enrichment.
    /// </summary>
    Academic = 4,

    /// <summary>
    /// Dance classes.
    /// </summary>
    Dance = 5,

    /// <summary>
    /// Other activities.
    /// </summary>
    Other = 6,
}
