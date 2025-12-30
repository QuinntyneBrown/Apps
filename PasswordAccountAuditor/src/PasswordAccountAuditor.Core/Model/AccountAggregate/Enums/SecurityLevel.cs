// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core;

/// <summary>
/// Represents the security level of an account.
/// </summary>
public enum SecurityLevel
{
    /// <summary>
    /// Security level is unknown or not yet assessed.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Low security (no 2FA, old password, etc.).
    /// </summary>
    Low = 1,

    /// <summary>
    /// Medium security (has 2FA but password needs update).
    /// </summary>
    Medium = 2,

    /// <summary>
    /// High security (has 2FA and recent password).
    /// </summary>
    High = 3,
}
