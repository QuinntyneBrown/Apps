// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FocusSessionTracker.Core;

namespace FocusSessionTracker.Api.Features.FocusSession;

/// <summary>
/// Data transfer object for focus session.
/// </summary>
public class FocusSessionDto
{
    /// <summary>
    /// Gets or sets the unique identifier.
    /// </summary>
    public Guid FocusSessionId { get; set; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the session type.
    /// </summary>
    public SessionType SessionType { get; set; }

    /// <summary>
    /// Gets or sets the session name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the planned duration in minutes.
    /// </summary>
    public int PlannedDurationMinutes { get; set; }

    /// <summary>
    /// Gets or sets the start time.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Gets or sets the end time.
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the focus score.
    /// </summary>
    public int? FocusScore { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the session is completed.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// Gets or sets the actual duration in minutes.
    /// </summary>
    public double? ActualDurationMinutes { get; set; }

    /// <summary>
    /// Gets or sets the distraction count.
    /// </summary>
    public int DistractionCount { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
