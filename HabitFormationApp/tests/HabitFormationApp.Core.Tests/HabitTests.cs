// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HabitFormationApp.Core.Tests;

public class HabitTests
{
    [Test]
    public void Habit_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var habitId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Morning Meditation";
        var description = "Meditate for 10 minutes";
        var frequency = HabitFrequency.Daily;
        var targetDaysPerWeek = 7;
        var startDate = DateTime.UtcNow;
        var isActive = true;
        var notes = "Focus on breathing";
        var createdAt = DateTime.UtcNow;

        // Act
        var habit = new Habit
        {
            HabitId = habitId,
            UserId = userId,
            Name = name,
            Description = description,
            Frequency = frequency,
            TargetDaysPerWeek = targetDaysPerWeek,
            StartDate = startDate,
            IsActive = isActive,
            Notes = notes,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(habit.HabitId, Is.EqualTo(habitId));
            Assert.That(habit.UserId, Is.EqualTo(userId));
            Assert.That(habit.Name, Is.EqualTo(name));
            Assert.That(habit.Description, Is.EqualTo(description));
            Assert.That(habit.Frequency, Is.EqualTo(frequency));
            Assert.That(habit.TargetDaysPerWeek, Is.EqualTo(targetDaysPerWeek));
            Assert.That(habit.StartDate, Is.EqualTo(startDate));
            Assert.That(habit.IsActive, Is.True);
            Assert.That(habit.Notes, Is.EqualTo(notes));
            Assert.That(habit.CreatedAt, Is.EqualTo(createdAt));
        });
    }

    [Test]
    public void Habit_DefaultName_IsEmptyString()
    {
        // Arrange & Act
        var habit = new Habit();

        // Assert
        Assert.That(habit.Name, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Habit_DefaultTargetDaysPerWeek_IsSeven()
    {
        // Arrange & Act
        var habit = new Habit();

        // Assert
        Assert.That(habit.TargetDaysPerWeek, Is.EqualTo(7));
    }

    [Test]
    public void Habit_DefaultIsActive_IsTrue()
    {
        // Arrange & Act
        var habit = new Habit();

        // Assert
        Assert.That(habit.IsActive, Is.True);
    }

    [Test]
    public void Habit_StartDate_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var habit = new Habit();
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(habit.StartDate, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void Habit_CreatedAt_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var habit = new Habit();
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(habit.CreatedAt, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void Habit_Description_CanBeNull()
    {
        // Arrange & Act
        var habit = new Habit
        {
            Description = null
        };

        // Assert
        Assert.That(habit.Description, Is.Null);
    }

    [Test]
    public void Habit_Notes_CanBeNull()
    {
        // Arrange & Act
        var habit = new Habit
        {
            Notes = null
        };

        // Assert
        Assert.That(habit.Notes, Is.Null);
    }

    [Test]
    public void Habit_Streaks_DefaultsToEmptyList()
    {
        // Arrange & Act
        var habit = new Habit();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(habit.Streaks, Is.Not.Null);
            Assert.That(habit.Streaks, Is.Empty);
        });
    }

    [Test]
    public void ToggleActive_SwitchesFromTrueToFalse()
    {
        // Arrange
        var habit = new Habit
        {
            IsActive = true
        };

        // Act
        habit.ToggleActive();

        // Assert
        Assert.That(habit.IsActive, Is.False);
    }

    [Test]
    public void ToggleActive_SwitchesFromFalseToTrue()
    {
        // Arrange
        var habit = new Habit
        {
            IsActive = false
        };

        // Act
        habit.ToggleActive();

        // Assert
        Assert.That(habit.IsActive, Is.True);
    }

    [Test]
    public void ToggleActive_CanBeCalledMultipleTimes()
    {
        // Arrange
        var habit = new Habit
        {
            IsActive = true
        };

        // Act
        habit.ToggleActive();
        habit.ToggleActive();

        // Assert
        Assert.That(habit.IsActive, Is.True);
    }

    [Test]
    public void Habit_CanHaveMultipleStreaks()
    {
        // Arrange
        var habit = new Habit();
        var streak1 = new Streak { CurrentStreak = 5 };
        var streak2 = new Streak { CurrentStreak = 10 };

        // Act
        habit.Streaks.Add(streak1);
        habit.Streaks.Add(streak2);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(habit.Streaks, Has.Count.EqualTo(2));
            Assert.That(habit.Streaks, Contains.Item(streak1));
            Assert.That(habit.Streaks, Contains.Item(streak2));
        });
    }

    [Test]
    public void Habit_CanHaveDifferentFrequencies()
    {
        // Arrange & Act
        var dailyHabit = new Habit { Frequency = HabitFrequency.Daily };
        var weeklyHabit = new Habit { Frequency = HabitFrequency.Weekly };
        var customHabit = new Habit { Frequency = HabitFrequency.Custom };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dailyHabit.Frequency, Is.EqualTo(HabitFrequency.Daily));
            Assert.That(weeklyHabit.Frequency, Is.EqualTo(HabitFrequency.Weekly));
            Assert.That(customHabit.Frequency, Is.EqualTo(HabitFrequency.Custom));
        });
    }

    [Test]
    public void Habit_AllProperties_CanBeModified()
    {
        // Arrange
        var habit = new Habit
        {
            HabitId = Guid.NewGuid(),
            Name = "Initial Name"
        };

        var newHabitId = Guid.NewGuid();
        var newUserId = Guid.NewGuid();
        var newStartDate = DateTime.UtcNow;

        // Act
        habit.HabitId = newHabitId;
        habit.UserId = newUserId;
        habit.Name = "Updated Name";
        habit.Description = "New Description";
        habit.Frequency = HabitFrequency.Weekly;
        habit.TargetDaysPerWeek = 5;
        habit.StartDate = newStartDate;
        habit.IsActive = false;
        habit.Notes = "New Notes";

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(habit.HabitId, Is.EqualTo(newHabitId));
            Assert.That(habit.UserId, Is.EqualTo(newUserId));
            Assert.That(habit.Name, Is.EqualTo("Updated Name"));
            Assert.That(habit.Description, Is.EqualTo("New Description"));
            Assert.That(habit.Frequency, Is.EqualTo(HabitFrequency.Weekly));
            Assert.That(habit.TargetDaysPerWeek, Is.EqualTo(5));
            Assert.That(habit.StartDate, Is.EqualTo(newStartDate));
            Assert.That(habit.IsActive, Is.False);
            Assert.That(habit.Notes, Is.EqualTo("New Notes"));
        });
    }
}
