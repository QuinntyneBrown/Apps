// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FishingLogSpotTracker.Core;

/// <summary>
/// Represents the type of fishing location.
/// </summary>
public enum LocationType
{
    /// <summary>
    /// Lake location.
    /// </summary>
    Lake = 0,

    /// <summary>
    /// River location.
    /// </summary>
    River = 1,

    /// <summary>
    /// Stream location.
    /// </summary>
    Stream = 2,

    /// <summary>
    /// Pond location.
    /// </summary>
    Pond = 3,

    /// <summary>
    /// Ocean location.
    /// </summary>
    Ocean = 4,

    /// <summary>
    /// Bay location.
    /// </summary>
    Bay = 5,

    /// <summary>
    /// Reservoir location.
    /// </summary>
    Reservoir = 6,

    /// <summary>
    /// Other location type.
    /// </summary>
    Other = 7,
}
