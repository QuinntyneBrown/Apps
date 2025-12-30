// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WorkoutPlanBuilder.Core;

/// <summary>
/// Event raised when a workout is completed.
/// </summary>
public record WorkoutCompletedEvent
{
    /// <summary>
    /// Gets the progress record ID.
    /// </summary>
    public Guid ProgressRecordId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the workout ID.
    /// </summary>
    public Guid WorkoutId { get; init; }

    /// <summary>
    /// Gets the actual duration in minutes.
    /// </summary>
    public int ActualDurationMinutes { get; init; }

    /// <summary>
    /// Gets the performance rating.
    /// </summary>
    public int? PerformanceRating { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
