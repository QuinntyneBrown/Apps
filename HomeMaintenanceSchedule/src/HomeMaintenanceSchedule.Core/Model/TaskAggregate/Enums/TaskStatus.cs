// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeMaintenanceSchedule.Core;

/// <summary>
/// Represents the status of a maintenance task.
/// </summary>
public enum TaskStatus
{
    /// <summary>
    /// Task is scheduled but not yet started.
    /// </summary>
    Scheduled = 0,

    /// <summary>
    /// Task is currently in progress.
    /// </summary>
    InProgress = 1,

    /// <summary>
    /// Task has been completed.
    /// </summary>
    Completed = 2,

    /// <summary>
    /// Task has been postponed.
    /// </summary>
    Postponed = 3,

    /// <summary>
    /// Task has been cancelled.
    /// </summary>
    Cancelled = 4,
}
