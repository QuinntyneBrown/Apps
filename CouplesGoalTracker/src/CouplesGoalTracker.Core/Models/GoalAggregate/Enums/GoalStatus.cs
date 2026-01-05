// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CouplesGoalTracker.Core;

/// <summary>
/// Represents the status of a goal.
/// </summary>
public enum GoalStatus
{
    /// <summary>
    /// Goal has not been started.
    /// </summary>
    NotStarted = 0,

    /// <summary>
    /// Goal is in progress.
    /// </summary>
    InProgress = 1,

    /// <summary>
    /// Goal has been completed.
    /// </summary>
    Completed = 2,

    /// <summary>
    /// Goal is on hold.
    /// </summary>
    OnHold = 3,

    /// <summary>
    /// Goal has been cancelled.
    /// </summary>
    Cancelled = 4,
}
