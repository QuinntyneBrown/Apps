// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalWiki.Core;

/// <summary>
/// Represents the status of a wiki page.
/// </summary>
public enum PageStatus
{
    /// <summary>
    /// Page is in draft status.
    /// </summary>
    Draft = 0,

    /// <summary>
    /// Page is published and visible.
    /// </summary>
    Published = 1,

    /// <summary>
    /// Page is under review.
    /// </summary>
    Review = 2,

    /// <summary>
    /// Page is archived.
    /// </summary>
    Archived = 3,
}
