// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core;

/// <summary>
/// Represents the type of security audit.
/// </summary>
public enum AuditType
{
    /// <summary>
    /// Manual security review.
    /// </summary>
    Manual = 0,

    /// <summary>
    /// Automated security scan.
    /// </summary>
    Automated = 1,

    /// <summary>
    /// Password strength analysis.
    /// </summary>
    PasswordStrength = 2,

    /// <summary>
    /// Two-factor authentication check.
    /// </summary>
    TwoFactorCheck = 3,

    /// <summary>
    /// Breach detection scan.
    /// </summary>
    BreachDetection = 4,

    /// <summary>
    /// Compliance audit.
    /// </summary>
    Compliance = 5,
}
