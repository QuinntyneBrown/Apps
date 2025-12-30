// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConversationStarterApp.Core;

namespace ConversationStarterApp.Api;

/// <summary>
/// Data transfer object for Session.
/// </summary>
public record SessionDto
{
    /// <summary>
    /// Gets or sets the session ID.
    /// </summary>
    public Guid SessionId { get; init; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets or sets the title or name of the session.
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the start time of the session.
    /// </summary>
    public DateTime StartTime { get; init; }

    /// <summary>
    /// Gets or sets the end time of the session.
    /// </summary>
    public DateTime? EndTime { get; init; }

    /// <summary>
    /// Gets or sets the participant names or descriptions.
    /// </summary>
    public string? Participants { get; init; }

    /// <summary>
    /// Gets or sets the prompts used in this session.
    /// </summary>
    public string? PromptsUsed { get; init; }

    /// <summary>
    /// Gets or sets notes or reflections from the session.
    /// </summary>
    public string? Notes { get; init; }

    /// <summary>
    /// Gets or sets a value indicating whether the session was successful.
    /// </summary>
    public bool WasSuccessful { get; init; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; init; }

    /// <summary>
    /// Gets or sets the duration of the session.
    /// </summary>
    public TimeSpan? Duration { get; init; }
}

/// <summary>
/// Extension methods for Session.
/// </summary>
public static class SessionExtensions
{
    /// <summary>
    /// Converts a Session to a DTO.
    /// </summary>
    /// <param name="session">The session.</param>
    /// <returns>The DTO.</returns>
    public static SessionDto ToDto(this Session session)
    {
        return new SessionDto
        {
            SessionId = session.SessionId,
            UserId = session.UserId,
            Title = session.Title,
            StartTime = session.StartTime,
            EndTime = session.EndTime,
            Participants = session.Participants,
            PromptsUsed = session.PromptsUsed,
            Notes = session.Notes,
            WasSuccessful = session.WasSuccessful,
            CreatedAt = session.CreatedAt,
            UpdatedAt = session.UpdatedAt,
            Duration = session.GetDuration(),
        };
    }
}
