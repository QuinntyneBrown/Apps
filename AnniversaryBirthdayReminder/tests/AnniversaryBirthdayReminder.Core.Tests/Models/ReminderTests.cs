// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnniversaryBirthdayReminder.Core.Tests;

/// <summary>
/// Unit tests for the Reminder entity.
/// </summary>
[TestFixture]
public class ReminderTests
{
    /// <summary>
    /// Tests that MarkAsSent sets status and timestamp correctly.
    /// </summary>
    [Test]
    public void MarkAsSent_SetsStatusAndTimestamp()
    {
        // Arrange
        var reminder = new Reminder
        {
            ReminderId = Guid.NewGuid(),
            ImportantDateId = Guid.NewGuid(),
            ScheduledTime = DateTime.UtcNow,
            AdvanceNoticeDays = 7,
            DeliveryChannel = DeliveryChannel.Email,
            Status = ReminderStatus.Scheduled,
        };

        // Act
        reminder.MarkAsSent();

        // Assert
        Assert.That(reminder.Status, Is.EqualTo(ReminderStatus.Sent));
        Assert.That(reminder.SentAt, Is.Not.Null);
        Assert.That(reminder.SentAt!.Value, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    /// <summary>
    /// Tests that Dismiss sets the status correctly.
    /// </summary>
    [Test]
    public void Dismiss_SetsStatusToDismissed()
    {
        // Arrange
        var reminder = new Reminder
        {
            ReminderId = Guid.NewGuid(),
            ImportantDateId = Guid.NewGuid(),
            ScheduledTime = DateTime.UtcNow,
            AdvanceNoticeDays = 7,
            DeliveryChannel = DeliveryChannel.Email,
            Status = ReminderStatus.Sent,
        };

        // Act
        reminder.Dismiss();

        // Assert
        Assert.That(reminder.Status, Is.EqualTo(ReminderStatus.Dismissed));
    }

    /// <summary>
    /// Tests that Snooze updates status and scheduled time correctly.
    /// </summary>
    [Test]
    public void Snooze_UpdatesStatusAndScheduledTime()
    {
        // Arrange
        var originalTime = DateTime.UtcNow;
        var reminder = new Reminder
        {
            ReminderId = Guid.NewGuid(),
            ImportantDateId = Guid.NewGuid(),
            ScheduledTime = originalTime,
            AdvanceNoticeDays = 7,
            DeliveryChannel = DeliveryChannel.Email,
            Status = ReminderStatus.Sent,
        };
        var snoozeDuration = TimeSpan.FromMinutes(30);

        // Act
        reminder.Snooze(snoozeDuration);

        // Assert
        Assert.That(reminder.Status, Is.EqualTo(ReminderStatus.Snoozed));
        Assert.That(reminder.ScheduledTime, Is.GreaterThan(originalTime));
    }

    /// <summary>
    /// Tests that IsReadyToSend returns true when scheduled and time has passed.
    /// </summary>
    [Test]
    public void IsReadyToSend_WhenScheduledAndTimePassed_ReturnsTrue()
    {
        // Arrange
        var reminder = new Reminder
        {
            ReminderId = Guid.NewGuid(),
            ImportantDateId = Guid.NewGuid(),
            ScheduledTime = DateTime.UtcNow.AddMinutes(-5),
            AdvanceNoticeDays = 7,
            DeliveryChannel = DeliveryChannel.Email,
            Status = ReminderStatus.Scheduled,
        };

        // Act
        var isReady = reminder.IsReadyToSend();

        // Assert
        Assert.That(isReady, Is.True);
    }

    /// <summary>
    /// Tests that IsReadyToSend returns false when already sent.
    /// </summary>
    [Test]
    public void IsReadyToSend_WhenAlreadySent_ReturnsFalse()
    {
        // Arrange
        var reminder = new Reminder
        {
            ReminderId = Guid.NewGuid(),
            ImportantDateId = Guid.NewGuid(),
            ScheduledTime = DateTime.UtcNow.AddMinutes(-5),
            AdvanceNoticeDays = 7,
            DeliveryChannel = DeliveryChannel.Email,
            Status = ReminderStatus.Sent,
        };

        // Act
        var isReady = reminder.IsReadyToSend();

        // Assert
        Assert.That(isReady, Is.False);
    }

    /// <summary>
    /// Tests that IsReadyToSend returns false when scheduled time is in the future.
    /// </summary>
    [Test]
    public void IsReadyToSend_WhenTimeInFuture_ReturnsFalse()
    {
        // Arrange
        var reminder = new Reminder
        {
            ReminderId = Guid.NewGuid(),
            ImportantDateId = Guid.NewGuid(),
            ScheduledTime = DateTime.UtcNow.AddMinutes(30),
            AdvanceNoticeDays = 7,
            DeliveryChannel = DeliveryChannel.Email,
            Status = ReminderStatus.Scheduled,
        };

        // Act
        var isReady = reminder.IsReadyToSend();

        // Assert
        Assert.That(isReady, Is.False);
    }
}
