// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HabitFormationApp.Core.Tests;

public class StreakTests
{
    [Test]
    public void Streak_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var streakId = Guid.NewGuid();
        var habitId = Guid.NewGuid();
        var currentStreak = 7;
        var longestStreak = 14;
        var lastCompletedDate = DateTime.UtcNow.AddDays(-1);
        var createdAt = DateTime.UtcNow;

        // Act
        var streak = new Streak
        {
            StreakId = streakId,
            HabitId = habitId,
            CurrentStreak = currentStreak,
            LongestStreak = longestStreak,
            LastCompletedDate = lastCompletedDate,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(streak.StreakId, Is.EqualTo(streakId));
            Assert.That(streak.HabitId, Is.EqualTo(habitId));
            Assert.That(streak.CurrentStreak, Is.EqualTo(currentStreak));
            Assert.That(streak.LongestStreak, Is.EqualTo(longestStreak));
            Assert.That(streak.LastCompletedDate, Is.EqualTo(lastCompletedDate));
            Assert.That(streak.CreatedAt, Is.EqualTo(createdAt));
        });
    }

    [Test]
    public void Streak_CreatedAt_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var streak = new Streak();
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(streak.CreatedAt, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void Streak_LastCompletedDate_CanBeNull()
    {
        // Arrange & Act
        var streak = new Streak
        {
            StreakId = Guid.NewGuid(),
            HabitId = Guid.NewGuid(),
            LastCompletedDate = null
        };

        // Assert
        Assert.That(streak.LastCompletedDate, Is.Null);
    }

    [Test]
    public void IncrementStreak_IncreasesCurrentStreak()
    {
        // Arrange
        var streak = new Streak
        {
            CurrentStreak = 5,
            LongestStreak = 10
        };

        // Act
        streak.IncrementStreak();

        // Assert
        Assert.That(streak.CurrentStreak, Is.EqualTo(6));
    }

    [Test]
    public void IncrementStreak_UpdatesLongestStreak_WhenCurrentExceedsLongest()
    {
        // Arrange
        var streak = new Streak
        {
            CurrentStreak = 10,
            LongestStreak = 10
        };

        // Act
        streak.IncrementStreak();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(streak.CurrentStreak, Is.EqualTo(11));
            Assert.That(streak.LongestStreak, Is.EqualTo(11));
        });
    }

    [Test]
    public void IncrementStreak_DoesNotUpdateLongestStreak_WhenCurrentBelowLongest()
    {
        // Arrange
        var streak = new Streak
        {
            CurrentStreak = 5,
            LongestStreak = 20
        };

        // Act
        streak.IncrementStreak();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(streak.CurrentStreak, Is.EqualTo(6));
            Assert.That(streak.LongestStreak, Is.EqualTo(20));
        });
    }

    [Test]
    public void IncrementStreak_UpdatesLastCompletedDate()
    {
        // Arrange
        var beforeIncrement = DateTime.UtcNow.AddSeconds(-1);
        var streak = new Streak
        {
            CurrentStreak = 3,
            LongestStreak = 10
        };

        // Act
        streak.IncrementStreak();
        var afterIncrement = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(streak.LastCompletedDate, Is.InRange(beforeIncrement, afterIncrement));
    }

    [Test]
    public void IncrementStreak_WorksFromZero()
    {
        // Arrange
        var streak = new Streak
        {
            CurrentStreak = 0,
            LongestStreak = 0
        };

        // Act
        streak.IncrementStreak();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(streak.CurrentStreak, Is.EqualTo(1));
            Assert.That(streak.LongestStreak, Is.EqualTo(1));
        });
    }

    [Test]
    public void Streak_Habit_CanBeSet()
    {
        // Arrange
        var habit = new Habit
        {
            HabitId = Guid.NewGuid(),
            Name = "Exercise"
        };

        var streak = new Streak();

        // Act
        streak.Habit = habit;

        // Assert
        Assert.That(streak.Habit, Is.EqualTo(habit));
    }

    [Test]
    public void Streak_AllProperties_CanBeModified()
    {
        // Arrange
        var streak = new Streak
        {
            StreakId = Guid.NewGuid(),
            CurrentStreak = 5
        };

        var newStreakId = Guid.NewGuid();
        var newHabitId = Guid.NewGuid();
        var newLastCompletedDate = DateTime.UtcNow;

        // Act
        streak.StreakId = newStreakId;
        streak.HabitId = newHabitId;
        streak.CurrentStreak = 10;
        streak.LongestStreak = 15;
        streak.LastCompletedDate = newLastCompletedDate;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(streak.StreakId, Is.EqualTo(newStreakId));
            Assert.That(streak.HabitId, Is.EqualTo(newHabitId));
            Assert.That(streak.CurrentStreak, Is.EqualTo(10));
            Assert.That(streak.LongestStreak, Is.EqualTo(15));
            Assert.That(streak.LastCompletedDate, Is.EqualTo(newLastCompletedDate));
        });
    }

    [Test]
    public void IncrementStreak_CanBeCalledMultipleTimes()
    {
        // Arrange
        var streak = new Streak
        {
            CurrentStreak = 0,
            LongestStreak = 0
        };

        // Act
        streak.IncrementStreak();
        streak.IncrementStreak();
        streak.IncrementStreak();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(streak.CurrentStreak, Is.EqualTo(3));
            Assert.That(streak.LongestStreak, Is.EqualTo(3));
        });
    }
}
