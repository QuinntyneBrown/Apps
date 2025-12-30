// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WorkoutPlanBuilder.Core;

/// <summary>
/// Event raised when a new workout is created.
/// </summary>
public record WorkoutCreatedEvent
{
    /// <summary>
    /// Gets the workout ID.
    /// </summary>
    public Guid WorkoutId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the workout name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the difficulty level.
    /// </summary>
    public int DifficultyLevel { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
