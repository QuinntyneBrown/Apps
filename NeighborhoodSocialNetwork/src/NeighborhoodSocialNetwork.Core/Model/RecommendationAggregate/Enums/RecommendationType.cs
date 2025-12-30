// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NeighborhoodSocialNetwork.Core;

/// <summary>
/// Represents the type of recommendation.
/// </summary>
public enum RecommendationType
{
    /// <summary>
    /// Restaurant recommendation.
    /// </summary>
    Restaurant = 0,

    /// <summary>
    /// Service provider recommendation.
    /// </summary>
    ServiceProvider = 1,

    /// <summary>
    /// Shop or retail recommendation.
    /// </summary>
    Shop = 2,

    /// <summary>
    /// Healthcare recommendation.
    /// </summary>
    Healthcare = 3,

    /// <summary>
    /// Entertainment recommendation.
    /// </summary>
    Entertainment = 4,

    /// <summary>
    /// Education recommendation.
    /// </summary>
    Education = 5,

    /// <summary>
    /// Other or general recommendation.
    /// </summary>
    Other = 6,
}
