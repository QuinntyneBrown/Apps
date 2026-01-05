// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DateNightIdeaGenerator.Core;

/// <summary>
/// Represents the budget range for a date idea.
/// </summary>
public enum BudgetRange
{
    /// <summary>
    /// Free date.
    /// </summary>
    Free = 0,

    /// <summary>
    /// Low budget ($1-$25).
    /// </summary>
    Low = 1,

    /// <summary>
    /// Medium budget ($26-$75).
    /// </summary>
    Medium = 2,

    /// <summary>
    /// High budget ($76-$150).
    /// </summary>
    High = 3,

    /// <summary>
    /// Premium budget ($150+).
    /// </summary>
    Premium = 4,
}
