// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MensGroupDiscussionTracker.Core;

/// <summary>
/// Represents a resource shared with the group.
/// </summary>
public class Resource
{
    /// <summary>
    /// Gets or sets the unique identifier for the resource.
    /// </summary>
    public Guid ResourceId { get; set; }

    /// <summary>
    /// Gets or sets the group ID this resource belongs to.
    /// </summary>
    public Guid GroupId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who shared this resource.
    /// </summary>
    public Guid SharedByUserId { get; set; }

    /// <summary>
    /// Gets or sets the title of the resource.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the resource.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the URL or link to the resource.
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// Gets or sets the resource type (e.g., "Book", "Article", "Video").
    /// </summary>
    public string? ResourceType { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the group this resource belongs to.
    /// </summary>
    public Group? Group { get; set; }
}
