// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MeetingNotesActionItemTracker.Core;

/// <summary>
/// Represents a meeting.
/// </summary>
public class Meeting
{
    /// <summary>
    /// Gets or sets the unique identifier for the meeting.
    /// </summary>
    public Guid MeetingId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this meeting.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the meeting title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the meeting date and time.
    /// </summary>
    public DateTime MeetingDateTime { get; set; }

    /// <summary>
    /// Gets or sets the duration in minutes.
    /// </summary>
    public int? DurationMinutes { get; set; }

    /// <summary>
    /// Gets or sets the meeting location or link.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets the list of attendees.
    /// </summary>
    public List<string> Attendees { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the meeting agenda.
    /// </summary>
    public string? Agenda { get; set; }

    /// <summary>
    /// Gets or sets the meeting summary.
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the collection of notes for this meeting.
    /// </summary>
    public ICollection<Note> Notes { get; set; } = new List<Note>();

    /// <summary>
    /// Gets or sets the collection of action items from this meeting.
    /// </summary>
    public ICollection<ActionItem> ActionItems { get; set; } = new List<ActionItem>();

    /// <summary>
    /// Adds an attendee to the meeting.
    /// </summary>
    /// <param name="attendee">The attendee name.</param>
    public void AddAttendee(string attendee)
    {
        if (!Attendees.Contains(attendee, StringComparer.OrdinalIgnoreCase))
        {
            Attendees.Add(attendee);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
