// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core;

/// <summary>
/// Represents the status of a security audit.
/// </summary>
public enum AuditStatus
{
    /// <summary>
    /// Audit is pending execution.
    /// </summary>
    Pending = 0,

    /// <summary>
    /// Audit is currently in progress.
    /// </summary>
    InProgress = 1,

    /// <summary>
    /// Audit completed successfully.
    /// </summary>
    Completed = 2,

    /// <summary>
    /// Audit failed to complete.
    /// </summary>
    Failed = 3,
}
