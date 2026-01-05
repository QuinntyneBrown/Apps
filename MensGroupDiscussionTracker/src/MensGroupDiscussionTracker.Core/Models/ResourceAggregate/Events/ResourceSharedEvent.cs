// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MensGroupDiscussionTracker.Core;

/// <summary>
/// Event raised when a resource is shared with the group.
/// </summary>
public record ResourceSharedEvent
{
    /// <summary>
    /// Gets the resource ID.
    /// </summary>
    public Guid ResourceId { get; init; }

    /// <summary>
    /// Gets the group ID.
    /// </summary>
    public Guid GroupId { get; init; }

    /// <summary>
    /// Gets the user ID who shared the resource.
    /// </summary>
    public Guid SharedByUserId { get; init; }

    /// <summary>
    /// Gets the title.
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
