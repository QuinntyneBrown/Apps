// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TimeAuditTracker.Core;

/// <summary>
/// Represents the category of an activity.
/// </summary>
public enum ActivityCategory
{
    /// <summary>
    /// Work-related activities.
    /// </summary>
    Work = 0,

    /// <summary>
    /// Personal development activities.
    /// </summary>
    PersonalDevelopment = 1,

    /// <summary>
    /// Exercise and fitness activities.
    /// </summary>
    Exercise = 2,

    /// <summary>
    /// Social and relationship activities.
    /// </summary>
    Social = 3,

    /// <summary>
    /// Entertainment and leisure activities.
    /// </summary>
    Entertainment = 4,

    /// <summary>
    /// Household and chores.
    /// </summary>
    Household = 5,

    /// <summary>
    /// Sleep and rest.
    /// </summary>
    Sleep = 6,

    /// <summary>
    /// Meals and eating.
    /// </summary>
    Meals = 7,

    /// <summary>
    /// Commute and travel.
    /// </summary>
    Commute = 8,

    /// <summary>
    /// Other uncategorized activities.
    /// </summary>
    Other = 9,
}
