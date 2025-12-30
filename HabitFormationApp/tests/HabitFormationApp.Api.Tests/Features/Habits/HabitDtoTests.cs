using HabitFormationApp.Api.Features.Habits;
using HabitFormationApp.Core;
using NUnit.Framework;

namespace HabitFormationApp.Api.Tests.Features.Habits;

[TestFixture]
public class HabitDtoTests
{
    [Test]
    public void ToDto_ShouldMapAllProperties_WhenHabitIsValid()
    {
        // Arrange
        var habit = new Habit
        {
            HabitId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Morning Exercise",
            Description = "30 minutes of cardio",
            Frequency = HabitFrequency.Daily,
            TargetDaysPerWeek = 7,
            StartDate = DateTime.UtcNow,
            IsActive = true,
            Notes = "Best time is 6:00 AM",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = habit.ToDto();

        // Assert
        Assert.That(dto.HabitId, Is.EqualTo(habit.HabitId));
        Assert.That(dto.UserId, Is.EqualTo(habit.UserId));
        Assert.That(dto.Name, Is.EqualTo(habit.Name));
        Assert.That(dto.Description, Is.EqualTo(habit.Description));
        Assert.That(dto.Frequency, Is.EqualTo(habit.Frequency));
        Assert.That(dto.TargetDaysPerWeek, Is.EqualTo(habit.TargetDaysPerWeek));
        Assert.That(dto.StartDate, Is.EqualTo(habit.StartDate));
        Assert.That(dto.IsActive, Is.EqualTo(habit.IsActive));
        Assert.That(dto.Notes, Is.EqualTo(habit.Notes));
        Assert.That(dto.CreatedAt, Is.EqualTo(habit.CreatedAt));
    }

    [Test]
    public void ToDto_ShouldHandleNullableProperties_WhenTheyAreNull()
    {
        // Arrange
        var habit = new Habit
        {
            HabitId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Meditation",
            Description = null,
            Frequency = HabitFrequency.Daily,
            TargetDaysPerWeek = 7,
            StartDate = DateTime.UtcNow,
            IsActive = true,
            Notes = null,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = habit.ToDto();

        // Assert
        Assert.That(dto.Description, Is.Null);
        Assert.That(dto.Notes, Is.Null);
    }
}
