// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InjuryPreventionRecoveryTracker.Core;

/// <summary>
/// Represents the severity level of an injury.
/// </summary>
public enum InjurySeverity
{
    /// <summary>
    /// Minor injury requiring minimal treatment.
    /// </summary>
    Minor = 0,

    /// <summary>
    /// Moderate injury requiring medical attention.
    /// </summary>
    Moderate = 1,

    /// <summary>
    /// Severe injury requiring significant medical intervention.
    /// </summary>
    Severe = 2,

    /// <summary>
    /// Critical injury requiring immediate medical attention.
    /// </summary>
    Critical = 3,
}
