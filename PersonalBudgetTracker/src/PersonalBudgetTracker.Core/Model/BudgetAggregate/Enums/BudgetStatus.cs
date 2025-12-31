// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalBudgetTracker.Core;

/// <summary>
/// Represents the status of a budget.
/// </summary>
public enum BudgetStatus
{
    /// <summary>
    /// Budget is in draft status.
    /// </summary>
    Draft = 0,

    /// <summary>
    /// Budget is active.
    /// </summary>
    Active = 1,

    /// <summary>
    /// Budget is completed.
    /// </summary>
    Completed = 2,
}
