using HabitFormationApp.Api.Features.Reminders;
using HabitFormationApp.Core;
using NUnit.Framework;

namespace HabitFormationApp.Api.Tests.Features.Reminders;

[TestFixture]
public class ReminderDtoTests
{
    [Test]
    public void ToDto_ShouldMapAllProperties_WhenReminderIsValid()
    {
        // Arrange
        var reminder = new Reminder
        {
            ReminderId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            HabitId = Guid.NewGuid(),
            ReminderTime = new TimeSpan(6, 0, 0),
            Message = "Time for your morning exercise!",
            IsEnabled = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = reminder.ToDto();

        // Assert
        Assert.That(dto.ReminderId, Is.EqualTo(reminder.ReminderId));
        Assert.That(dto.UserId, Is.EqualTo(reminder.UserId));
        Assert.That(dto.HabitId, Is.EqualTo(reminder.HabitId));
        Assert.That(dto.ReminderTime, Is.EqualTo(reminder.ReminderTime));
        Assert.That(dto.Message, Is.EqualTo(reminder.Message));
        Assert.That(dto.IsEnabled, Is.EqualTo(reminder.IsEnabled));
        Assert.That(dto.CreatedAt, Is.EqualTo(reminder.CreatedAt));
    }

    [Test]
    public void ToDto_ShouldHandleNullMessage()
    {
        // Arrange
        var reminder = new Reminder
        {
            ReminderId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            HabitId = Guid.NewGuid(),
            ReminderTime = new TimeSpan(6, 0, 0),
            Message = null,
            IsEnabled = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = reminder.ToDto();

        // Assert
        Assert.That(dto.Message, Is.Null);
    }
}
