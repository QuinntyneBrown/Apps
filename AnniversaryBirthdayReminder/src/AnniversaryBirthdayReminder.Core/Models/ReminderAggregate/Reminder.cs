// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnniversaryBirthdayReminder.Core;

/// <summary>
/// Represents a reminder for an important date.
/// </summary>
public class Reminder
{
    /// <summary>
    /// Gets or sets the unique identifier for the reminder.
    /// </summary>
    public Guid ReminderId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the important date.
    /// </summary>
    public Guid ImportantDateId { get; set; }

    /// <summary>
    /// Gets or sets the scheduled time for the reminder.
    /// </summary>
    public DateTime ScheduledTime { get; set; }

    /// <summary>
    /// Gets or sets the number of days in advance to send the reminder.
    /// </summary>
    public int AdvanceNoticeDays { get; set; }

    /// <summary>
    /// Gets or sets the delivery channel for the reminder.
    /// </summary>
    public DeliveryChannel DeliveryChannel { get; set; }

    /// <summary>
    /// Gets or sets the status of the reminder.
    /// </summary>
    public ReminderStatus Status { get; set; } = ReminderStatus.Scheduled;

    /// <summary>
    /// Gets or sets the time when the reminder was sent.
    /// </summary>
    public DateTime? SentAt { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the important date.
    /// </summary>
    public ImportantDate? ImportantDate { get; set; }

    /// <summary>
    /// Marks the reminder as sent.
    /// </summary>
    public void MarkAsSent()
    {
        Status = ReminderStatus.Sent;
        SentAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Dismisses the reminder.
    /// </summary>
    public void Dismiss()
    {
        Status = ReminderStatus.Dismissed;
    }

    /// <summary>
    /// Snoozes the reminder for the specified duration.
    /// </summary>
    /// <param name="duration">The duration to snooze.</param>
    public void Snooze(TimeSpan duration)
    {
        Status = ReminderStatus.Snoozed;
        ScheduledTime = DateTime.UtcNow.Add(duration);
    }

    /// <summary>
    /// Gets a value indicating whether the reminder is ready to be sent.
    /// </summary>
    /// <returns>True if the reminder is ready to be sent; otherwise, false.</returns>
    public bool IsReadyToSend()
    {
        return Status == ReminderStatus.Scheduled && ScheduledTime <= DateTime.UtcNow;
    }
}
