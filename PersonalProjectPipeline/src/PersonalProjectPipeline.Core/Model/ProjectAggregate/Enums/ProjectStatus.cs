// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalProjectPipeline.Core;

/// <summary>
/// Represents the status of a project.
/// </summary>
public enum ProjectStatus
{
    /// <summary>
    /// Project is in the idea or planning phase.
    /// </summary>
    Idea = 0,

    /// <summary>
    /// Project is planned but not started.
    /// </summary>
    Planned = 1,

    /// <summary>
    /// Project is in progress.
    /// </summary>
    InProgress = 2,

    /// <summary>
    /// Project is on hold.
    /// </summary>
    OnHold = 3,

    /// <summary>
    /// Project is completed.
    /// </summary>
    Completed = 4,

    /// <summary>
    /// Project is cancelled.
    /// </summary>
    Cancelled = 5,
}
