// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnniversaryBirthdayReminder.Core;

namespace AnniversaryBirthdayReminder.Api;

/// <summary>
/// Data transfer object for Reminder.
/// </summary>
public record ReminderDto
{
    /// <summary>
    /// Gets or sets the reminder ID.
    /// </summary>
    public Guid ReminderId { get; init; }

    /// <summary>
    /// Gets or sets the important date ID.
    /// </summary>
    public Guid ImportantDateId { get; init; }

    /// <summary>
    /// Gets or sets the scheduled time.
    /// </summary>
    public DateTime ScheduledTime { get; init; }

    /// <summary>
    /// Gets or sets the advance notice days.
    /// </summary>
    public int AdvanceNoticeDays { get; init; }

    /// <summary>
    /// Gets or sets the delivery channel.
    /// </summary>
    public DeliveryChannel DeliveryChannel { get; init; }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    public ReminderStatus Status { get; init; }

    /// <summary>
    /// Gets or sets the sent timestamp.
    /// </summary>
    public DateTime? SentAt { get; init; }
}

/// <summary>
/// Extension methods for Reminder.
/// </summary>
public static class ReminderExtensions
{
    /// <summary>
    /// Converts a Reminder to a DTO.
    /// </summary>
    /// <param name="reminder">The reminder.</param>
    /// <returns>The DTO.</returns>
    public static ReminderDto ToDto(this Reminder reminder)
    {
        return new ReminderDto
        {
            ReminderId = reminder.ReminderId,
            ImportantDateId = reminder.ImportantDateId,
            ScheduledTime = reminder.ScheduledTime,
            AdvanceNoticeDays = reminder.AdvanceNoticeDays,
            DeliveryChannel = reminder.DeliveryChannel,
            Status = reminder.Status,
            SentAt = reminder.SentAt,
        };
    }
}
