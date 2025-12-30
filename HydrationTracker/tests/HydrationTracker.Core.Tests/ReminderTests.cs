// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HydrationTracker.Core.Tests;

public class ReminderTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesReminder()
    {
        // Arrange
        var reminderId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var reminderTime = new TimeSpan(10, 30, 0);
        var message = "Time to hydrate!";
        var isEnabled = true;

        // Act
        var reminder = new Reminder
        {
            ReminderId = reminderId,
            UserId = userId,
            ReminderTime = reminderTime,
            Message = message,
            IsEnabled = isEnabled
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(reminder.ReminderId, Is.EqualTo(reminderId));
            Assert.That(reminder.UserId, Is.EqualTo(userId));
            Assert.That(reminder.ReminderTime, Is.EqualTo(reminderTime));
            Assert.That(reminder.Message, Is.EqualTo(message));
            Assert.That(reminder.IsEnabled, Is.EqualTo(isEnabled));
            Assert.That(reminder.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void DefaultConstructor_SetsDefaultValues()
    {
        // Act
        var reminder = new Reminder();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(reminder.ReminderId, Is.EqualTo(Guid.Empty));
            Assert.That(reminder.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(reminder.ReminderTime, Is.EqualTo(TimeSpan.Zero));
            Assert.That(reminder.IsEnabled, Is.True);
            Assert.That(reminder.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Toggle_EnabledReminder_DisablesIt()
    {
        // Arrange
        var reminder = new Reminder { IsEnabled = true };

        // Act
        reminder.Toggle();

        // Assert
        Assert.That(reminder.IsEnabled, Is.False);
    }

    [Test]
    public void Toggle_DisabledReminder_EnablesIt()
    {
        // Arrange
        var reminder = new Reminder { IsEnabled = false };

        // Act
        reminder.Toggle();

        // Assert
        Assert.That(reminder.IsEnabled, Is.True);
    }

    [Test]
    public void Toggle_MultipleTimes_TogglesCorrectly()
    {
        // Arrange
        var reminder = new Reminder { IsEnabled = true };

        // Act & Assert
        reminder.Toggle();
        Assert.That(reminder.IsEnabled, Is.False);

        reminder.Toggle();
        Assert.That(reminder.IsEnabled, Is.True);

        reminder.Toggle();
        Assert.That(reminder.IsEnabled, Is.False);
    }

    [Test]
    public void IsEnabled_DefaultValue_IsTrue()
    {
        // Act
        var reminder = new Reminder();

        // Assert
        Assert.That(reminder.IsEnabled, Is.True);
    }

    [Test]
    public void Message_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var reminder = new Reminder { Message = null };

        // Assert
        Assert.That(reminder.Message, Is.Null);
    }

    [Test]
    public void Message_OptionalField_CanBeSet()
    {
        // Arrange & Act
        var reminder = new Reminder { Message = "Drink water!" };

        // Assert
        Assert.That(reminder.Message, Is.EqualTo("Drink water!"));
    }

    [Test]
    public void ReminderTime_MorningTime_CanBeSet()
    {
        // Arrange
        var reminder = new Reminder();
        var morningTime = new TimeSpan(8, 0, 0);

        // Act
        reminder.ReminderTime = morningTime;

        // Assert
        Assert.That(reminder.ReminderTime, Is.EqualTo(morningTime));
    }

    [Test]
    public void ReminderTime_EveningTime_CanBeSet()
    {
        // Arrange
        var reminder = new Reminder();
        var eveningTime = new TimeSpan(20, 30, 0);

        // Act
        reminder.ReminderTime = eveningTime;

        // Assert
        Assert.That(reminder.ReminderTime, Is.EqualTo(eveningTime));
    }

    [Test]
    public void ReminderTime_MidnightTime_CanBeSet()
    {
        // Arrange
        var reminder = new Reminder();
        var midnight = new TimeSpan(0, 0, 0);

        // Act
        reminder.ReminderTime = midnight;

        // Assert
        Assert.That(reminder.ReminderTime, Is.EqualTo(midnight));
    }

    [Test]
    public void ReminderTime_WithSeconds_IsPreserved()
    {
        // Arrange
        var reminder = new Reminder();
        var timeWithSeconds = new TimeSpan(14, 25, 37);

        // Act
        reminder.ReminderTime = timeWithSeconds;

        // Assert
        Assert.That(reminder.ReminderTime, Is.EqualTo(timeWithSeconds));
    }

    [Test]
    public void CreatedAt_DefaultValue_IsCurrentUtcTime()
    {
        // Act
        var reminder = new Reminder();

        // Assert
        Assert.That(reminder.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void UserId_CanBeSetAndRetrieved()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var reminder = new Reminder();

        // Act
        reminder.UserId = userId;

        // Assert
        Assert.That(reminder.UserId, Is.EqualTo(userId));
    }

    [Test]
    public void ReminderId_CanBeSetAndRetrieved()
    {
        // Arrange
        var reminderId = Guid.NewGuid();
        var reminder = new Reminder();

        // Act
        reminder.ReminderId = reminderId;

        // Assert
        Assert.That(reminder.ReminderId, Is.EqualTo(reminderId));
    }
}
