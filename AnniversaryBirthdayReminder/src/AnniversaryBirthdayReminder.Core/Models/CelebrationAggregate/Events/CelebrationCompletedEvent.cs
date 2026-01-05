// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnniversaryBirthdayReminder.Core;

/// <summary>
/// Event raised when a celebration is completed.
/// </summary>
public record CelebrationCompletedEvent
{
    /// <summary>
    /// Gets the celebration ID.
    /// </summary>
    public Guid CelebrationId { get; init; }

    /// <summary>
    /// Gets the important date ID.
    /// </summary>
    public Guid ImportantDateId { get; init; }

    /// <summary>
    /// Gets the celebration date.
    /// </summary>
    public DateTime CelebrationDate { get; init; }

    /// <summary>
    /// Gets the rating (1-5).
    /// </summary>
    public int? Rating { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
