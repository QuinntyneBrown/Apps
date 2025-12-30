// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MensGroupDiscussionTracker.Core;

/// <summary>
/// Represents a discussion topic.
/// </summary>
public class Topic
{
    /// <summary>
    /// Gets or sets the unique identifier for the topic.
    /// </summary>
    public Guid TopicId { get; set; }

    /// <summary>
    /// Gets or sets the meeting ID this topic was discussed in.
    /// </summary>
    public Guid? MeetingId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who created this topic.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the title of the topic.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description or details of the topic.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the category of the topic.
    /// </summary>
    public TopicCategory Category { get; set; }

    /// <summary>
    /// Gets or sets discussion notes or key points.
    /// </summary>
    public string? DiscussionNotes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the meeting this topic was discussed in.
    /// </summary>
    public Meeting? Meeting { get; set; }
}
