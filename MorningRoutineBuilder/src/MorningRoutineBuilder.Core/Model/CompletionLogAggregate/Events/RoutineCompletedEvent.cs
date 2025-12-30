// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MorningRoutineBuilder.Core;

/// <summary>
/// Event raised when a routine is completed.
/// </summary>
public record RoutineCompletedEvent
{
    /// <summary>
    /// Gets the completion log ID.
    /// </summary>
    public Guid CompletionLogId { get; init; }

    /// <summary>
    /// Gets the routine ID.
    /// </summary>
    public Guid RoutineId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the completion date.
    /// </summary>
    public DateTime CompletionDate { get; init; }

    /// <summary>
    /// Gets the tasks completed.
    /// </summary>
    public int TasksCompleted { get; init; }

    /// <summary>
    /// Gets the total tasks.
    /// </summary>
    public int TotalTasks { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
