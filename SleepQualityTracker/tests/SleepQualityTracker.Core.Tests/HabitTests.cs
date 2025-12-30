// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SleepQualityTracker.Core.Tests;

public class HabitTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesHabit()
    {
        // Arrange
        var habitId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Evening Exercise";
        var description = "30 minutes of light cardio";
        var habitType = "Exercise";
        var isPositive = true;
        var typicalTime = new TimeSpan(19, 0, 0);
        var impactLevel = 4;

        // Act
        var habit = new Habit
        {
            HabitId = habitId,
            UserId = userId,
            Name = name,
            Description = description,
            HabitType = habitType,
            IsPositive = isPositive,
            TypicalTime = typicalTime,
            ImpactLevel = impactLevel
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(habit.HabitId, Is.EqualTo(habitId));
            Assert.That(habit.UserId, Is.EqualTo(userId));
            Assert.That(habit.Name, Is.EqualTo(name));
            Assert.That(habit.Description, Is.EqualTo(description));
            Assert.That(habit.HabitType, Is.EqualTo(habitType));
            Assert.That(habit.IsPositive, Is.True);
            Assert.That(habit.TypicalTime, Is.EqualTo(typicalTime));
            Assert.That(habit.ImpactLevel, Is.EqualTo(4));
            Assert.That(habit.IsActive, Is.True);
        });
    }

    [Test]
    public void DefaultValues_NewHabit_HasExpectedDefaults()
    {
        // Act
        var habit = new Habit();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(habit.Name, Is.EqualTo(string.Empty));
            Assert.That(habit.HabitType, Is.EqualTo(string.Empty));
            Assert.That(habit.IsPositive, Is.False);
            Assert.That(habit.ImpactLevel, Is.EqualTo(0));
            Assert.That(habit.IsActive, Is.True);
            Assert.That(habit.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void IsHighImpact_ImpactLevel4_ReturnsTrue()
    {
        // Arrange
        var habit = new Habit
        {
            ImpactLevel = 4
        };

        // Act
        var result = habit.IsHighImpact();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsHighImpact_ImpactLevel5_ReturnsTrue()
    {
        // Arrange
        var habit = new Habit
        {
            ImpactLevel = 5
        };

        // Act
        var result = habit.IsHighImpact();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsHighImpact_ImpactLevel3_ReturnsFalse()
    {
        // Arrange
        var habit = new Habit
        {
            ImpactLevel = 3
        };

        // Act
        var result = habit.IsHighImpact();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsHighImpact_ImpactLevel1_ReturnsFalse()
    {
        // Arrange
        var habit = new Habit
        {
            ImpactLevel = 1
        };

        // Act
        var result = habit.IsHighImpact();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void ToggleActive_WhenTrue_SetsToFalse()
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
    public void ToggleActive_WhenFalse_SetsToTrue()
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
    public void ToggleActive_MultipleTimes_TogglesCorrectly()
    {
        // Arrange
        var habit = new Habit
        {
            IsActive = true
        };

        // Act & Assert
        habit.ToggleActive();
        Assert.That(habit.IsActive, Is.False);

        habit.ToggleActive();
        Assert.That(habit.IsActive, Is.True);

        habit.ToggleActive();
        Assert.That(habit.IsActive, Is.False);
    }

    [Test]
    public void IsPositive_PositiveHabit_ReturnsTrue()
    {
        // Arrange
        var habit = new Habit
        {
            IsPositive = true
        };

        // Assert
        Assert.That(habit.IsPositive, Is.True);
    }

    [Test]
    public void IsPositive_NegativeHabit_ReturnsFalse()
    {
        // Arrange
        var habit = new Habit
        {
            IsPositive = false
        };

        // Assert
        Assert.That(habit.IsPositive, Is.False);
    }

    [Test]
    public void Description_OptionalField_CanBeNull()
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
    public void TypicalTime_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var habit = new Habit
        {
            TypicalTime = null
        };

        // Assert
        Assert.That(habit.TypicalTime, Is.Null);
    }

    [Test]
    public void TypicalTime_CanStoreValidTime_MorningTime()
    {
        // Arrange
        var morningTime = new TimeSpan(6, 30, 0);

        // Act
        var habit = new Habit
        {
            TypicalTime = morningTime
        };

        // Assert
        Assert.That(habit.TypicalTime, Is.EqualTo(morningTime));
    }

    [Test]
    public void TypicalTime_CanStoreValidTime_EveningTime()
    {
        // Arrange
        var eveningTime = new TimeSpan(22, 0, 0);

        // Act
        var habit = new Habit
        {
            TypicalTime = eveningTime
        };

        // Assert
        Assert.That(habit.TypicalTime, Is.EqualTo(eveningTime));
    }

    [Test]
    public void ImpactLevel_ValidRange_CanBeStored()
    {
        // Arrange & Act
        var habit1 = new Habit { ImpactLevel = 1 };
        var habit2 = new Habit { ImpactLevel = 3 };
        var habit3 = new Habit { ImpactLevel = 5 };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(habit1.ImpactLevel, Is.EqualTo(1));
            Assert.That(habit2.ImpactLevel, Is.EqualTo(3));
            Assert.That(habit3.ImpactLevel, Is.EqualTo(5));
        });
    }
}
