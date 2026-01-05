// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace JobSearchOrganizer.Core;

/// <summary>
/// Event raised when an interview is scheduled.
/// </summary>
public record InterviewScheduledEvent
{
    /// <summary>
    /// Gets the interview ID.
    /// </summary>
    public Guid InterviewId { get; init; }

    /// <summary>
    /// Gets the application ID.
    /// </summary>
    public Guid ApplicationId { get; init; }

    /// <summary>
    /// Gets the interview type.
    /// </summary>
    public string InterviewType { get; init; } = string.Empty;

    /// <summary>
    /// Gets the scheduled date and time.
    /// </summary>
    public DateTime ScheduledDateTime { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
