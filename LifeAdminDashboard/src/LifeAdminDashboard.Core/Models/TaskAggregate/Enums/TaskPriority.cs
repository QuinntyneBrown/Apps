// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace LifeAdminDashboard.Core;

/// <summary>
/// Represents the priority of a task.
/// </summary>
public enum TaskPriority
{
    /// <summary>
    /// Low priority.
    /// </summary>
    Low = 0,

    /// <summary>
    /// Medium priority.
    /// </summary>
    Medium = 1,

    /// <summary>
    /// High priority.
    /// </summary>
    High = 2,

    /// <summary>
    /// Urgent priority.
    /// </summary>
    Urgent = 3,
}
