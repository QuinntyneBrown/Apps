// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Core;

namespace ConferenceEventManager.Api.Features.Sessions;

/// <summary>
/// Data transfer object for Session.
/// </summary>
public class SessionDto
{
    /// <summary>
    /// Gets or sets the session ID.
    /// </summary>
    public Guid SessionId { get; set; }

    /// <summary>
    /// Gets or sets the user ID.
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
    /// Gets or sets a value indicating whether plans to attend.
    /// </summary>
    public bool PlansToAttend { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether attended.
    /// </summary>
    public bool DidAttend { get; set; }

    /// <summary>
    /// Gets or sets notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Maps a Session entity to SessionDto.
    /// </summary>
    public static SessionDto FromSession(Session session)
    {
        return new SessionDto
        {
            SessionId = session.SessionId,
            UserId = session.UserId,
            EventId = session.EventId,
            Title = session.Title,
            Speaker = session.Speaker,
            Description = session.Description,
            StartTime = session.StartTime,
            EndTime = session.EndTime,
            Room = session.Room,
            PlansToAttend = session.PlansToAttend,
            DidAttend = session.DidAttend,
            Notes = session.Notes,
            CreatedAt = session.CreatedAt,
            UpdatedAt = session.UpdatedAt
        };
    }
}
