// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnniversaryBirthdayReminder.Core;

/// <summary>
/// Event raised when a celebration is skipped.
/// </summary>
public record CelebrationSkippedEvent
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
    /// Gets the date that was skipped.
    /// </summary>
    public DateTime SkippedDate { get; init; }

    /// <summary>
    /// Gets the reason for skipping.
    /// </summary>
    public string? Reason { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
