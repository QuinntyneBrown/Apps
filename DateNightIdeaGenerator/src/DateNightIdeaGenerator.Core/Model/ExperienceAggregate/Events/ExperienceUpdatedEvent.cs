// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DateNightIdeaGenerator.Core;

/// <summary>
/// Event raised when an experience is updated.
/// </summary>
public record ExperienceUpdatedEvent
{
    /// <summary>
    /// Gets the experience ID.
    /// </summary>
    public Guid ExperienceId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
