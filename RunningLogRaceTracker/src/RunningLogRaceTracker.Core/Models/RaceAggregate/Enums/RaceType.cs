// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RunningLogRaceTracker.Core;

/// <summary>
/// Represents the type of race.
/// </summary>
public enum RaceType
{
    /// <summary>
    /// 5 kilometer race.
    /// </summary>
    FiveK = 0,

    /// <summary>
    /// 10 kilometer race.
    /// </summary>
    TenK = 1,

    /// <summary>
    /// Half marathon (21.1 km).
    /// </summary>
    HalfMarathon = 2,

    /// <summary>
    /// Full marathon (42.2 km).
    /// </summary>
    Marathon = 3,

    /// <summary>
    /// Ultra marathon (any distance over 42.2 km).
    /// </summary>
    UltraMarathon = 4,

    /// <summary>
    /// Trail race.
    /// </summary>
    Trail = 5,

    /// <summary>
    /// Other or custom race type.
    /// </summary>
    Other = 6,
}
