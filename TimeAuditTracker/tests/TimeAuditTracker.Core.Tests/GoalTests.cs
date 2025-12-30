// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TimeAuditTracker.Core.Tests;

public class GoalTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesGoal()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var category = ActivityCategory.Exercise;
        var targetHoursPerWeek = 10.0;
        var minimumHoursPerWeek = 5.0;
        var description = "Exercise goal";
        var startDate = DateTime.UtcNow;

        // Act
        var goal = new Goal
        {
            GoalId = goalId,
            UserId = userId,
            Category = category,
            TargetHoursPerWeek = targetHoursPerWeek,
            MinimumHoursPerWeek = minimumHoursPerWeek,
            Description = description,
            StartDate = startDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(goal.GoalId, Is.EqualTo(goalId));
            Assert.That(goal.UserId, Is.EqualTo(userId));
            Assert.That(goal.Category, Is.EqualTo(category));
            Assert.That(goal.TargetHoursPerWeek, Is.EqualTo(targetHoursPerWeek));
            Assert.That(goal.MinimumHoursPerWeek, Is.EqualTo(minimumHoursPerWeek));
            Assert.That(goal.Description, Is.EqualTo(description));
            Assert.That(goal.IsActive, Is.True);
            Assert.That(goal.StartDate, Is.EqualTo(startDate));
            Assert.That(goal.EndDate, Is.Null);
        });
    }

    [Test]
    public void GetTargetHoursPerDay_ReturnsCorrectValue()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = ActivityCategory.Work,
            TargetHoursPerWeek = 35.0,
            Description = "Work goal",
            StartDate = DateTime.UtcNow
        };

        // Act
        var hoursPerDay = goal.GetTargetHoursPerDay();

        // Assert
        Assert.That(hoursPerDay, Is.EqualTo(5.0).Within(0.001));
    }

    [Test]
    public void IsGoalMet_WhenActualHoursEqualTarget_ReturnsTrue()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = ActivityCategory.Exercise,
            TargetHoursPerWeek = 10.0,
            Description = "Exercise goal",
            StartDate = DateTime.UtcNow
        };

        // Act
        var isMet = goal.IsGoalMet(10.0);

        // Assert
        Assert.That(isMet, Is.True);
    }

    [Test]
    public void IsGoalMet_WhenActualHoursExceedTarget_ReturnsTrue()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = ActivityCategory.Exercise,
            TargetHoursPerWeek = 10.0,
            Description = "Exercise goal",
            StartDate = DateTime.UtcNow
        };

        // Act
        var isMet = goal.IsGoalMet(12.0);

        // Assert
        Assert.That(isMet, Is.True);
    }

    [Test]
    public void IsGoalMet_WhenActualHoursBelowTarget_ReturnsFalse()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = ActivityCategory.Exercise,
            TargetHoursPerWeek = 10.0,
            Description = "Exercise goal",
            StartDate = DateTime.UtcNow
        };

        // Act
        var isMet = goal.IsGoalMet(8.0);

        // Assert
        Assert.That(isMet, Is.False);
    }

    [Test]
    public void GetProgressPercentage_ReturnsCorrectPercentage()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = ActivityCategory.Work,
            TargetHoursPerWeek = 40.0,
            Description = "Work goal",
            StartDate = DateTime.UtcNow
        };

        // Act
        var progress = goal.GetProgressPercentage(20.0);

        // Assert
        Assert.That(progress, Is.EqualTo(50.0));
    }

    [Test]
    public void GetProgressPercentage_WhenTargetIsZero_ReturnsZero()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = ActivityCategory.Other,
            TargetHoursPerWeek = 0.0,
            Description = "Test goal",
            StartDate = DateTime.UtcNow
        };

        // Act
        var progress = goal.GetProgressPercentage(10.0);

        // Assert
        Assert.That(progress, Is.EqualTo(0.0));
    }

    [Test]
    public void GetProgressPercentage_WhenExceedingTarget_ReturnsOver100()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = ActivityCategory.Exercise,
            TargetHoursPerWeek = 10.0,
            Description = "Exercise goal",
            StartDate = DateTime.UtcNow
        };

        // Act
        var progress = goal.GetProgressPercentage(15.0);

        // Assert
        Assert.That(progress, Is.EqualTo(150.0));
    }

    [Test]
    public void Deactivate_SetsIsActiveToFalseAndSetsEndDate()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = ActivityCategory.Work,
            TargetHoursPerWeek = 40.0,
            Description = "Work goal",
            StartDate = DateTime.UtcNow,
            IsActive = true
        };

        var beforeCall = DateTime.UtcNow;

        // Act
        goal.Deactivate();

        var afterCall = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(goal.IsActive, Is.False);
            Assert.That(goal.EndDate, Is.Not.Null);
            Assert.That(goal.EndDate.Value, Is.GreaterThanOrEqualTo(beforeCall));
            Assert.That(goal.EndDate.Value, Is.LessThanOrEqualTo(afterCall));
        });
    }

    [Test]
    public void CreatedAt_DefaultsToUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = ActivityCategory.Work,
            TargetHoursPerWeek = 40.0,
            Description = "Work goal",
            StartDate = DateTime.UtcNow
        };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(goal.CreatedAt, Is.GreaterThanOrEqualTo(beforeCreation));
            Assert.That(goal.CreatedAt, Is.LessThanOrEqualTo(afterCreation));
        });
    }

    [Test]
    public void Goal_WithoutMinimumHours_IsValid()
    {
        // Arrange & Act
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = ActivityCategory.Work,
            TargetHoursPerWeek = 40.0,
            Description = "Work goal",
            StartDate = DateTime.UtcNow
        };

        // Assert
        Assert.That(goal.MinimumHoursPerWeek, Is.Null);
    }

    [Test]
    public void GetTargetHoursPerDay_WithSmallTarget_ReturnsCorrectValue()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = ActivityCategory.Exercise,
            TargetHoursPerWeek = 7.0,
            Description = "Daily exercise goal",
            StartDate = DateTime.UtcNow
        };

        // Act
        var hoursPerDay = goal.GetTargetHoursPerDay();

        // Assert
        Assert.That(hoursPerDay, Is.EqualTo(1.0).Within(0.001));
    }

    [Test]
    public void IsActive_DefaultsToTrue()
    {
        // Arrange & Act
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = ActivityCategory.Work,
            TargetHoursPerWeek = 40.0,
            Description = "Work goal",
            StartDate = DateTime.UtcNow
        };

        // Assert
        Assert.That(goal.IsActive, Is.True);
    }

    [Test]
    public void GetProgressPercentage_WithZeroActualHours_ReturnsZero()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = ActivityCategory.Work,
            TargetHoursPerWeek = 40.0,
            Description = "Work goal",
            StartDate = DateTime.UtcNow
        };

        // Act
        var progress = goal.GetProgressPercentage(0.0);

        // Assert
        Assert.That(progress, Is.EqualTo(0.0));
    }
}
