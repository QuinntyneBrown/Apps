// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SleepQualityTracker.Core;

/// <summary>
/// Represents the quality of a sleep session.
/// </summary>
public enum SleepQuality
{
    /// <summary>
    /// Very poor sleep quality.
    /// </summary>
    VeryPoor = 0,

    /// <summary>
    /// Poor sleep quality.
    /// </summary>
    Poor = 1,

    /// <summary>
    /// Fair sleep quality.
    /// </summary>
    Fair = 2,

    /// <summary>
    /// Good sleep quality.
    /// </summary>
    Good = 3,

    /// <summary>
    /// Excellent sleep quality.
    /// </summary>
    Excellent = 4,
}
