// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MensGroupDiscussionTracker.Core;

/// <summary>
/// Represents a group meeting.
/// </summary>
public class Meeting
{
    /// <summary>
    /// Gets or sets the unique identifier for the meeting.
    /// </summary>
    public Guid MeetingId { get; set; }

    /// <summary>
    /// Gets or sets the group ID this meeting belongs to.
    /// </summary>
    public Guid GroupId { get; set; }

    /// <summary>
    /// Gets or sets the title of the meeting.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the meeting date and time.
    /// </summary>
    public DateTime MeetingDateTime { get; set; }

    /// <summary>
    /// Gets or sets the location of the meeting.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets the meeting notes or summary.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the attendee count.
    /// </summary>
    public int AttendeeCount { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the group this meeting belongs to.
    /// </summary>
    public Group? Group { get; set; }

    /// <summary>
    /// Gets or sets the collection of topics discussed in this meeting.
    /// </summary>
    public ICollection<Topic> Topics { get; set; } = new List<Topic>();
}
