// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnniversaryBirthdayReminder.Core;

/// <summary>
/// Event raised when a reminder is snoozed.
/// </summary>
public record ReminderSnoozedEvent
{
    /// <summary>
    /// Gets the reminder ID.
    /// </summary>
    public Guid ReminderId { get; init; }

    /// <summary>
    /// Gets the original scheduled time.
    /// </summary>
    public DateTime OriginalScheduledTime { get; init; }

    /// <summary>
    /// Gets the new scheduled time.
    /// </summary>
    public DateTime NewScheduledTime { get; init; }

    /// <summary>
    /// Gets the snooze duration.
    /// </summary>
    public TimeSpan SnoozeDuration { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
