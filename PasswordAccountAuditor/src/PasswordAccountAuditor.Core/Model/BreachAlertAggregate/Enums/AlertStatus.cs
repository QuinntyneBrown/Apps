// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core;

/// <summary>
/// Represents the status of a breach alert.
/// </summary>
public enum AlertStatus
{
    /// <summary>
    /// Alert is newly created and not yet reviewed.
    /// </summary>
    New = 0,

    /// <summary>
    /// Alert has been acknowledged and is being addressed.
    /// </summary>
    Acknowledged = 1,

    /// <summary>
    /// Alert has been resolved.
    /// </summary>
    Resolved = 2,

    /// <summary>
    /// Alert was dismissed as a false positive or not applicable.
    /// </summary>
    Dismissed = 3,
}
