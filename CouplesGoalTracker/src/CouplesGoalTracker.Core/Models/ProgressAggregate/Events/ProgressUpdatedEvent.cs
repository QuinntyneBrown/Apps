// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CouplesGoalTracker.Core;

/// <summary>
/// Event raised when a progress entry is updated.
/// </summary>
public record ProgressUpdatedEvent
{
    /// <summary>
    /// Gets the progress ID.
    /// </summary>
    public Guid ProgressId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the new completion percentage.
    /// </summary>
    public double NewCompletionPercentage { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
