// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace LifeAdminDashboard.Core;

/// <summary>
/// Event raised when a task is created.
/// </summary>
public record TaskCreatedEvent
{
    /// <summary>
    /// Gets the task ID.
    /// </summary>
    public Guid AdminTaskId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the task title.
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// Gets the task category.
    /// </summary>
    public TaskCategory Category { get; init; }

    /// <summary>
    /// Gets the task priority.
    /// </summary>
    public TaskPriority Priority { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
