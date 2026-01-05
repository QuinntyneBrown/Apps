// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SalaryCompensationTracker.Core;

/// <summary>
/// Represents the type of compensation.
/// </summary>
public enum CompensationType
{
    /// <summary>
    /// Full-time employment.
    /// </summary>
    FullTime = 0,

    /// <summary>
    /// Part-time employment.
    /// </summary>
    PartTime = 1,

    /// <summary>
    /// Contract position.
    /// </summary>
    Contract = 2,

    /// <summary>
    /// Freelance work.
    /// </summary>
    Freelance = 3,

    /// <summary>
    /// Consulting engagement.
    /// </summary>
    Consulting = 4,

    /// <summary>
    /// Bonus or one-time payment.
    /// </summary>
    Bonus = 5,

    /// <summary>
    /// Raise or salary increase.
    /// </summary>
    Raise = 6,

    /// <summary>
    /// Other type of compensation.
    /// </summary>
    Other = 7,
}
