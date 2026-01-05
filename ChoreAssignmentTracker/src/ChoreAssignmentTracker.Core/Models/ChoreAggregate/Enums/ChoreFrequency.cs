// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ChoreAssignmentTracker.Core;

/// <summary>
/// Represents the frequency of a chore.
/// </summary>
public enum ChoreFrequency
{
    /// <summary>
    /// Chore is performed daily.
    /// </summary>
    Daily = 0,

    /// <summary>
    /// Chore is performed weekly.
    /// </summary>
    Weekly = 1,

    /// <summary>
    /// Chore is performed bi-weekly (every two weeks).
    /// </summary>
    BiWeekly = 2,

    /// <summary>
    /// Chore is performed monthly.
    /// </summary>
    Monthly = 3,

    /// <summary>
    /// Chore is performed as needed.
    /// </summary>
    AsNeeded = 4,
}
