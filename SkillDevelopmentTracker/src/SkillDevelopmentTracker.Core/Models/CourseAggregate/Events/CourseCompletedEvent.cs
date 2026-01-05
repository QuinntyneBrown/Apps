// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SkillDevelopmentTracker.Core;

/// <summary>
/// Event raised when a course is completed.
/// </summary>
public record CourseCompletedEvent
{
    /// <summary>
    /// Gets the course ID.
    /// </summary>
    public Guid CourseId { get; init; }

    /// <summary>
    /// Gets the completion date.
    /// </summary>
    public DateTime CompletionDate { get; init; }

    /// <summary>
    /// Gets the actual hours spent.
    /// </summary>
    public decimal ActualHours { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
