// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalMissionStatementBuilder.Core;

/// <summary>
/// Event raised when a mission statement is updated.
/// </summary>
public record MissionStatementUpdatedEvent
{
    /// <summary>
    /// Gets the mission statement ID.
    /// </summary>
    public Guid MissionStatementId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
