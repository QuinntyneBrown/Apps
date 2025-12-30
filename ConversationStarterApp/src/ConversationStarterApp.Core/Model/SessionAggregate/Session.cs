// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ConversationStarterApp.Core;

/// <summary>
/// Represents a conversation session.
/// </summary>
public class Session
{
    /// <summary>
    /// Gets or sets the unique identifier for the session.
    /// </summary>
    public Guid SessionId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who started this session.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the title or name of the session.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the start time of the session.
    /// </summary>
    public DateTime StartTime { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the end time of the session.
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Gets or sets the participant names or descriptions.
    /// </summary>
    public string? Participants { get; set; }

    /// <summary>
    /// Gets or sets the prompts used in this session (comma-separated IDs).
    /// </summary>
    public string? PromptsUsed { get; set; }

    /// <summary>
    /// Gets or sets notes or reflections from the session.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the session was successful or enjoyable.
    /// </summary>
    public bool WasSuccessful { get; set; } = true;

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Ends the session.
    /// </summary>
    public void EndSession()
    {
        EndTime = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Calculates the duration of the session.
    /// </summary>
    /// <returns>The duration, or null if the session has not ended.</returns>
    public TimeSpan? GetDuration()
    {
        if (EndTime.HasValue)
        {
            return EndTime.Value - StartTime;
        }

        return null;
    }
}
