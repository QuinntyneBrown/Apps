// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FreelanceProjectManager.Core;

/// <summary>
/// Represents the status of a freelance project.
/// </summary>
public enum ProjectStatus
{
    /// <summary>
    /// Project is being planned.
    /// </summary>
    Planning = 0,

    /// <summary>
    /// Project is in progress.
    /// </summary>
    InProgress = 1,

    /// <summary>
    /// Project is on hold.
    /// </summary>
    OnHold = 2,

    /// <summary>
    /// Project is under review.
    /// </summary>
    UnderReview = 3,

    /// <summary>
    /// Project is completed.
    /// </summary>
    Completed = 4,

    /// <summary>
    /// Project is cancelled.
    /// </summary>
    Cancelled = 5,
}
