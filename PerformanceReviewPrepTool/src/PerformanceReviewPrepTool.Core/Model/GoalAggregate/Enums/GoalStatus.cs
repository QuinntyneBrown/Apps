// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PerformanceReviewPrepTool.Core;

/// <summary>
/// Represents the status of a goal.
/// </summary>
public enum GoalStatus
{
    /// <summary>
    /// Not started.
    /// </summary>
    NotStarted = 0,

    /// <summary>
    /// In progress.
    /// </summary>
    InProgress = 1,

    /// <summary>
    /// On track.
    /// </summary>
    OnTrack = 2,

    /// <summary>
    /// At risk.
    /// </summary>
    AtRisk = 3,

    /// <summary>
    /// Completed.
    /// </summary>
    Completed = 4,

    /// <summary>
    /// Deferred or postponed.
    /// </summary>
    Deferred = 5,

    /// <summary>
    /// Cancelled.
    /// </summary>
    Cancelled = 6,
}
