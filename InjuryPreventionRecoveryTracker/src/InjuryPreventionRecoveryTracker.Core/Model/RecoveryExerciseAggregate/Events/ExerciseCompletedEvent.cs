// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InjuryPreventionRecoveryTracker.Core;

/// <summary>
/// Event raised when a recovery exercise is completed.
/// </summary>
public record ExerciseCompletedEvent
{
    /// <summary>
    /// Gets the recovery exercise ID.
    /// </summary>
    public Guid RecoveryExerciseId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the injury ID.
    /// </summary>
    public Guid InjuryId { get; init; }

    /// <summary>
    /// Gets the exercise name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the completion timestamp.
    /// </summary>
    public DateTime CompletedAt { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
