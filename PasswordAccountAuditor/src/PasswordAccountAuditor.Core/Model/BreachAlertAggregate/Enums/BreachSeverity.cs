// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core;

/// <summary>
/// Represents the severity level of a security breach.
/// </summary>
public enum BreachSeverity
{
    /// <summary>
    /// Low severity breach.
    /// </summary>
    Low = 0,

    /// <summary>
    /// Medium severity breach.
    /// </summary>
    Medium = 1,

    /// <summary>
    /// High severity breach requiring prompt action.
    /// </summary>
    High = 2,

    /// <summary>
    /// Critical severity breach requiring immediate action.
    /// </summary>
    Critical = 3,
}
