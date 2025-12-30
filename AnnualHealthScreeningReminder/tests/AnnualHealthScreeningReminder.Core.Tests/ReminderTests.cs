// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnnualHealthScreeningReminder.Core.Tests;

public class ReminderTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesReminder()
    {
        // Arrange
        var reminderId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var screeningId = Guid.NewGuid();
        var reminderDate = DateTime.UtcNow.AddDays(7);
        var message = "Time for your annual physical exam";

        // Act
        var reminder = new Reminder
        {
            ReminderId = reminderId,
            UserId = userId,
            ScreeningId = screeningId,
            ReminderDate = reminderDate,
            Message = message,
            IsSent = false
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(reminder.ReminderId, Is.EqualTo(reminderId));
            Assert.That(reminder.UserId, Is.EqualTo(userId));
            Assert.That(reminder.ScreeningId, Is.EqualTo(screeningId));
            Assert.That(reminder.ReminderDate, Is.EqualTo(reminderDate));
            Assert.That(reminder.Message, Is.EqualTo(message));
            Assert.That(reminder.IsSent, Is.False);
            Assert.That(reminder.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Constructor_DefaultValues_SetsCorrectDefaults()
    {
        // Act
        var reminder = new Reminder();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(reminder.Message, Is.Null);
            Assert.That(reminder.IsSent, Is.False);
            Assert.That(reminder.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void MarkAsSent_NotSent_SetsSentToTrue()
    {
        // Arrange
        var reminder = new Reminder { IsSent = false };

        // Act
        reminder.MarkAsSent();

        // Assert
        Assert.That(reminder.IsSent, Is.True);
    }

    [Test]
    public void MarkAsSent_AlreadySent_RemainsSent()
    {
        // Arrange
        var reminder = new Reminder { IsSent = true };

        // Act
        reminder.MarkAsSent();

        // Assert
        Assert.That(reminder.IsSent, Is.True);
    }

    [Test]
    public void MarkAsSent_CalledMultipleTimes_RemainsTrue()
    {
        // Arrange
        var reminder = new Reminder { IsSent = false };

        // Act
        reminder.MarkAsSent();
        reminder.MarkAsSent();
        reminder.MarkAsSent();

        // Assert
        Assert.That(reminder.IsSent, Is.True);
    }

    [Test]
    public void Message_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var reminder = new Reminder();
        var message = "Don't forget your dentist appointment";

        // Act
        reminder.Message = message;

        // Assert
        Assert.That(reminder.Message, Is.EqualTo(message));
    }

    [Test]
    public void Message_CanBeSetToNull_SetsCorrectly()
    {
        // Arrange
        var reminder = new Reminder { Message = "Some message" };

        // Act
        reminder.Message = null;

        // Assert
        Assert.That(reminder.Message, Is.Null);
    }

    [Test]
    public void ReminderDate_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var reminder = new Reminder();
        var reminderDate = DateTime.UtcNow.AddDays(14);

        // Act
        reminder.ReminderDate = reminderDate;

        // Assert
        Assert.That(reminder.ReminderDate, Is.EqualTo(reminderDate));
    }

    [Test]
    public void IsSent_CanBeSetManually_SetsCorrectly()
    {
        // Arrange
        var reminder = new Reminder { IsSent = false };

        // Act
        reminder.IsSent = true;

        // Assert
        Assert.That(reminder.IsSent, Is.True);
    }

    [Test]
    public void ScreeningId_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var reminder = new Reminder();
        var screeningId = Guid.NewGuid();

        // Act
        reminder.ScreeningId = screeningId;

        // Assert
        Assert.That(reminder.ScreeningId, Is.EqualTo(screeningId));
    }
}
