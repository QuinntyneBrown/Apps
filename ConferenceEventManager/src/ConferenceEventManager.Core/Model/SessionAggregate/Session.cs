// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ConferenceEventManager.Core;

/// <summary>
/// Represents a session within an event.
/// </summary>
public class Session
{
    /// <summary>
    /// Gets or sets the unique identifier for the session.
    /// </summary>
    public Guid SessionId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this session.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the event ID.
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Gets or sets the session title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the speaker name(s).
    /// </summary>
    public string? Speaker { get; set; }

    /// <summary>
    /// Gets or sets the session description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the start time.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Gets or sets the end time.
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Gets or sets the room or location.
    /// </summary>
    public string? Room { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user plans to attend.
    /// </summary>
    public bool PlansToAttend { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user attended.
    /// </summary>
    public bool DidAttend { get; set; }

    /// <summary>
    /// Gets or sets optional notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the event.
    /// </summary>
    public Event? Event { get; set; }

    /// <summary>
    /// Marks the session as attended.
    /// </summary>
    public void MarkAsAttended()
    {
        DidAttend = true;
        UpdatedAt = DateTime.UtcNow;
    }
}
