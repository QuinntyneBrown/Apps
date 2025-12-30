// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HabitFormationApp.Core.Tests;

public class ReminderTests
{
    [Test]
    public void Reminder_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var reminderId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var habitId = Guid.NewGuid();
        var reminderTime = new TimeSpan(8, 30, 0);
        var message = "Time to exercise!";
        var isEnabled = true;
        var createdAt = DateTime.UtcNow;

        // Act
        var reminder = new Reminder
        {
            ReminderId = reminderId,
            UserId = userId,
            HabitId = habitId,
            ReminderTime = reminderTime,
            Message = message,
            IsEnabled = isEnabled,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(reminder.ReminderId, Is.EqualTo(reminderId));
            Assert.That(reminder.UserId, Is.EqualTo(userId));
            Assert.That(reminder.HabitId, Is.EqualTo(habitId));
            Assert.That(reminder.ReminderTime, Is.EqualTo(reminderTime));
            Assert.That(reminder.Message, Is.EqualTo(message));
            Assert.That(reminder.IsEnabled, Is.True);
            Assert.That(reminder.CreatedAt, Is.EqualTo(createdAt));
        });
    }

    [Test]
    public void Reminder_DefaultIsEnabled_IsTrue()
    {
        // Arrange & Act
        var reminder = new Reminder();

        // Assert
        Assert.That(reminder.IsEnabled, Is.True);
    }

    [Test]
    public void Reminder_CreatedAt_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var reminder = new Reminder();
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(reminder.CreatedAt, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void Reminder_Message_CanBeNull()
    {
        // Arrange & Act
        var reminder = new Reminder
        {
            Message = null
        };

        // Assert
        Assert.That(reminder.Message, Is.Null);
    }

    [Test]
    public void Toggle_SwitchesFromTrueToFalse()
    {
        // Arrange
        var reminder = new Reminder
        {
            IsEnabled = true
        };

        // Act
        reminder.Toggle();

        // Assert
        Assert.That(reminder.IsEnabled, Is.False);
    }

    [Test]
    public void Toggle_SwitchesFromFalseToTrue()
    {
        // Arrange
        var reminder = new Reminder
        {
            IsEnabled = false
        };

        // Act
        reminder.Toggle();

        // Assert
        Assert.That(reminder.IsEnabled, Is.True);
    }

    [Test]
    public void Toggle_CanBeCalledMultipleTimes()
    {
        // Arrange
        var reminder = new Reminder
        {
            IsEnabled = true
        };

        // Act
        reminder.Toggle();
        reminder.Toggle();

        // Assert
        Assert.That(reminder.IsEnabled, Is.True);
    }

    [Test]
    public void Reminder_ReminderTime_CanBeSetToMorning()
    {
        // Arrange
        var morningTime = new TimeSpan(6, 0, 0);

        // Act
        var reminder = new Reminder
        {
            ReminderTime = morningTime
        };

        // Assert
        Assert.That(reminder.ReminderTime, Is.EqualTo(morningTime));
    }

    [Test]
    public void Reminder_ReminderTime_CanBeSetToEvening()
    {
        // Arrange
        var eveningTime = new TimeSpan(20, 30, 0);

        // Act
        var reminder = new Reminder
        {
            ReminderTime = eveningTime
        };

        // Assert
        Assert.That(reminder.ReminderTime, Is.EqualTo(eveningTime));
    }

    [Test]
    public void Reminder_AllProperties_CanBeModified()
    {
        // Arrange
        var reminder = new Reminder
        {
            ReminderId = Guid.NewGuid(),
            IsEnabled = true
        };

        var newReminderId = Guid.NewGuid();
        var newUserId = Guid.NewGuid();
        var newHabitId = Guid.NewGuid();
        var newReminderTime = new TimeSpan(12, 0, 0);

        // Act
        reminder.ReminderId = newReminderId;
        reminder.UserId = newUserId;
        reminder.HabitId = newHabitId;
        reminder.ReminderTime = newReminderTime;
        reminder.Message = "New message";
        reminder.IsEnabled = false;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(reminder.ReminderId, Is.EqualTo(newReminderId));
            Assert.That(reminder.UserId, Is.EqualTo(newUserId));
            Assert.That(reminder.HabitId, Is.EqualTo(newHabitId));
            Assert.That(reminder.ReminderTime, Is.EqualTo(newReminderTime));
            Assert.That(reminder.Message, Is.EqualTo("New message"));
            Assert.That(reminder.IsEnabled, Is.False);
        });
    }

    [Test]
    public void Reminder_CanBeDisabled()
    {
        // Arrange
        var reminder = new Reminder
        {
            IsEnabled = true
        };

        // Act
        reminder.IsEnabled = false;

        // Assert
        Assert.That(reminder.IsEnabled, Is.False);
    }

    [Test]
    public void Reminder_ReminderTime_SupportsSeconds()
    {
        // Arrange
        var timeWithSeconds = new TimeSpan(14, 30, 45);

        // Act
        var reminder = new Reminder
        {
            ReminderTime = timeWithSeconds
        };

        // Assert
        Assert.That(reminder.ReminderTime, Is.EqualTo(timeWithSeconds));
    }
}
