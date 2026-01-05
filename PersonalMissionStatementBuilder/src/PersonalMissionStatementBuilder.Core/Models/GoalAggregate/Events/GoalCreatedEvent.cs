// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalMissionStatementBuilder.Core;

/// <summary>
/// Event raised when a new goal is created.
/// </summary>
public record GoalCreatedEvent
{
    /// <summary>
    /// Gets the goal ID.
    /// </summary>
    public Guid GoalId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the title.
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
