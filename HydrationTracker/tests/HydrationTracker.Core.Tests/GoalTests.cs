// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HydrationTracker.Core.Tests;

public class GoalTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesGoal()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var dailyGoalMl = 2000m;
        var startDate = new DateTime(2025, 1, 1);
        var isActive = true;
        var notes = "Increase hydration";

        // Act
        var goal = new Goal
        {
            GoalId = goalId,
            UserId = userId,
            DailyGoalMl = dailyGoalMl,
            StartDate = startDate,
            IsActive = isActive,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(goal.GoalId, Is.EqualTo(goalId));
            Assert.That(goal.UserId, Is.EqualTo(userId));
            Assert.That(goal.DailyGoalMl, Is.EqualTo(dailyGoalMl));
            Assert.That(goal.StartDate, Is.EqualTo(startDate));
            Assert.That(goal.IsActive, Is.EqualTo(isActive));
            Assert.That(goal.Notes, Is.EqualTo(notes));
            Assert.That(goal.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void DefaultConstructor_SetsDefaultValues()
    {
        // Act
        var goal = new Goal();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(goal.GoalId, Is.EqualTo(Guid.Empty));
            Assert.That(goal.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(goal.DailyGoalMl, Is.EqualTo(0m));
            Assert.That(goal.IsActive, Is.True);
            Assert.That(goal.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void GetDailyGoalInOz_ValidGoal_ReturnsCorrectConversion()
    {
        // Arrange
        var goal = new Goal { DailyGoalMl = 2000m };

        // Act
        var result = goal.GetDailyGoalInOz();

        // Assert
        Assert.That(result, Is.EqualTo(67.628m).Within(0.001m));
    }

    [Test]
    public void GetDailyGoalInOz_ZeroGoal_ReturnsZero()
    {
        // Arrange
        var goal = new Goal { DailyGoalMl = 0m };

        // Act
        var result = goal.GetDailyGoalInOz();

        // Assert
        Assert.That(result, Is.EqualTo(0m));
    }

    [Test]
    public void GetDailyGoalInOz_LargeGoal_ReturnsCorrectConversion()
    {
        // Arrange
        var goal = new Goal { DailyGoalMl = 5000m };

        // Act
        var result = goal.GetDailyGoalInOz();

        // Assert
        Assert.That(result, Is.EqualTo(169.07m).Within(0.01m));
    }

    [Test]
    public void GetDailyGoalInOz_SmallGoal_ReturnsCorrectConversion()
    {
        // Arrange
        var goal = new Goal { DailyGoalMl = 500m };

        // Act
        var result = goal.GetDailyGoalInOz();

        // Assert
        Assert.That(result, Is.EqualTo(16.907m).Within(0.001m));
    }

    [Test]
    public void IsActive_DefaultValue_IsTrue()
    {
        // Act
        var goal = new Goal();

        // Assert
        Assert.That(goal.IsActive, Is.True);
    }

    [Test]
    public void IsActive_CanBeSetToFalse()
    {
        // Arrange
        var goal = new Goal { IsActive = true };

        // Act
        goal.IsActive = false;

        // Assert
        Assert.That(goal.IsActive, Is.False);
    }

    [Test]
    public void IsActive_CanBeToggled()
    {
        // Arrange
        var goal = new Goal { IsActive = true };

        // Act
        goal.IsActive = !goal.IsActive;

        // Assert
        Assert.That(goal.IsActive, Is.False);
    }

    [Test]
    public void Notes_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var goal = new Goal { Notes = null };

        // Assert
        Assert.That(goal.Notes, Is.Null);
    }

    [Test]
    public void Notes_OptionalField_CanBeSet()
    {
        // Arrange & Act
        var goal = new Goal { Notes = "Summer hydration challenge" };

        // Assert
        Assert.That(goal.Notes, Is.EqualTo("Summer hydration challenge"));
    }

    [Test]
    public void DailyGoalMl_DecimalPrecision_IsPreserved()
    {
        // Arrange
        var goal = new Goal();
        var dailyGoal = 2500.5m;

        // Act
        goal.DailyGoalMl = dailyGoal;

        // Assert
        Assert.That(goal.DailyGoalMl, Is.EqualTo(dailyGoal));
    }

    [Test]
    public void StartDate_CanBeSetToAnyDate()
    {
        // Arrange
        var goal = new Goal();
        var pastDate = new DateTime(2020, 1, 1);
        var futureDate = new DateTime(2026, 12, 31);

        // Act & Assert
        goal.StartDate = pastDate;
        Assert.That(goal.StartDate, Is.EqualTo(pastDate));

        goal.StartDate = futureDate;
        Assert.That(goal.StartDate, Is.EqualTo(futureDate));
    }

    [Test]
    public void CreatedAt_DefaultValue_IsCurrentUtcTime()
    {
        // Act
        var goal = new Goal();

        // Assert
        Assert.That(goal.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void UserId_CanBeSetAndRetrieved()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var goal = new Goal();

        // Act
        goal.UserId = userId;

        // Assert
        Assert.That(goal.UserId, Is.EqualTo(userId));
    }
}
