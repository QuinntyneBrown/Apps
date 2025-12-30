// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalProjectPipeline.Core;

/// <summary>
/// Represents the priority of a project.
/// </summary>
public enum ProjectPriority
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
    /// Critical priority.
    /// </summary>
    Critical = 3,
}
