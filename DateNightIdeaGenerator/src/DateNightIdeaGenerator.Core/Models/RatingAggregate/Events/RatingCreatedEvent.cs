// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DateNightIdeaGenerator.Core;

/// <summary>
/// Event raised when a new rating is created.
/// </summary>
public record RatingCreatedEvent
{
    /// <summary>
    /// Gets the rating ID.
    /// </summary>
    public Guid RatingId { get; init; }

    /// <summary>
    /// Gets the date idea ID.
    /// </summary>
    public Guid? DateIdeaId { get; init; }

    /// <summary>
    /// Gets the experience ID.
    /// </summary>
    public Guid? ExperienceId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the rating score.
    /// </summary>
    public int Score { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
