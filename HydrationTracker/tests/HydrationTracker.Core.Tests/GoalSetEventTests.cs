// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HydrationTracker.Core.Tests;

public class GoalSetEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesGoalSetEvent()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var dailyGoalMl = 2000m;
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new GoalSetEvent
        {
            GoalId = goalId,
            UserId = userId,
            DailyGoalMl = dailyGoalMl,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.GoalId, Is.EqualTo(goalId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.DailyGoalMl, Is.EqualTo(dailyGoalMl));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void DefaultConstructor_SetsDefaultTimestamp()
    {
        // Act
        var evt = new GoalSetEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.GoalId, Is.EqualTo(Guid.Empty));
            Assert.That(evt.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(evt.DailyGoalMl, Is.EqualTo(0m));
            Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var dailyGoalMl = 2500m;
        var timestamp = new DateTime(2025, 1, 1);

        var evt1 = new GoalSetEvent
        {
            GoalId = goalId,
            UserId = userId,
            DailyGoalMl = dailyGoalMl,
            Timestamp = timestamp
        };

        var evt2 = new GoalSetEvent
        {
            GoalId = goalId,
            UserId = userId,
            DailyGoalMl = dailyGoalMl,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void Record_Inequality_WorksCorrectly()
    {
        // Arrange
        var evt1 = new GoalSetEvent
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            DailyGoalMl = 2000m,
            Timestamp = DateTime.UtcNow
        };

        var evt2 = new GoalSetEvent
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            DailyGoalMl = 3000m,
            Timestamp = DateTime.UtcNow
        };

        // Assert
        Assert.That(evt1, Is.Not.EqualTo(evt2));
    }

    [Test]
    public void With_Expression_CreatesNewInstance()
    {
        // Arrange
        var original = new GoalSetEvent
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            DailyGoalMl = 2000m,
            Timestamp = DateTime.UtcNow
        };

        // Act
        var modified = original with { DailyGoalMl = 2500m };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(modified.GoalId, Is.EqualTo(original.GoalId));
            Assert.That(modified.UserId, Is.EqualTo(original.UserId));
            Assert.That(modified.DailyGoalMl, Is.EqualTo(2500m));
            Assert.That(modified.Timestamp, Is.EqualTo(original.Timestamp));
            Assert.That(modified, Is.Not.SameAs(original));
        });
    }

    [Test]
    public void DailyGoalMl_DecimalValue_IsPreserved()
    {
        // Arrange
        var dailyGoal = 2750.5m;
        var evt = new GoalSetEvent { DailyGoalMl = dailyGoal };

        // Assert
        Assert.That(evt.DailyGoalMl, Is.EqualTo(dailyGoal));
    }

    [Test]
    public void GoalId_CanBeSetAndRetrieved()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        var evt = new GoalSetEvent { GoalId = goalId };

        // Assert
        Assert.That(evt.GoalId, Is.EqualTo(goalId));
    }

    [Test]
    public void UserId_CanBeSetAndRetrieved()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var evt = new GoalSetEvent { UserId = userId };

        // Assert
        Assert.That(evt.UserId, Is.EqualTo(userId));
    }

    [Test]
    public void Timestamp_CanBeSetToSpecificTime()
    {
        // Arrange
        var specificTime = new DateTime(2025, 3, 1, 9, 0, 0);
        var evt = new GoalSetEvent { Timestamp = specificTime };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(specificTime));
    }

    [Test]
    public void DailyGoalMl_LargeValue_CanBeSet()
    {
        // Arrange
        var largeGoal = 10000m;
        var evt = new GoalSetEvent { DailyGoalMl = largeGoal };

        // Assert
        Assert.That(evt.DailyGoalMl, Is.EqualTo(largeGoal));
    }
}
