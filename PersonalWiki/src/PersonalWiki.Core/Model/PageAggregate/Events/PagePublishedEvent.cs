// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalWiki.Core;

/// <summary>
/// Event raised when a wiki page is published.
/// </summary>
public record PagePublishedEvent
{
    /// <summary>
    /// Gets the page ID.
    /// </summary>
    public Guid WikiPageId { get; init; }

    /// <summary>
    /// Gets the page title.
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// Gets the version number.
    /// </summary>
    public int Version { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
