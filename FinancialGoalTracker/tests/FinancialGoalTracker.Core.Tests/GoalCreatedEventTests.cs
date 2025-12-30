// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FinancialGoalTracker.Core.Tests;

public class GoalCreatedEventTests
{
    [Test]
    public void Constructor_CreatesEvent_WithAllProperties()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        var name = "Retirement Fund";
        var targetAmount = 100000m;
        var targetDate = new DateTime(2050, 1, 1);
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new GoalCreatedEvent
        {
            GoalId = goalId,
            Name = name,
            TargetAmount = targetAmount,
            TargetDate = targetDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.GoalId, Is.EqualTo(goalId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.TargetAmount, Is.EqualTo(targetAmount));
            Assert.That(evt.TargetDate, Is.EqualTo(targetDate));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Event_WithDefaultTimestamp_SetsUtcNow()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        var name = "Emergency Fund";
        var targetAmount = 10000m;
        var targetDate = new DateTime(2025, 12, 31);

        // Act
        var evt = new GoalCreatedEvent
        {
            GoalId = goalId,
            Name = name,
            TargetAmount = targetAmount,
            TargetDate = targetDate
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void Event_IsRecord_AndSupportsValueEquality()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        var name = "Car Purchase";
        var targetAmount = 30000m;
        var targetDate = new DateTime(2026, 6, 1);
        var timestamp = DateTime.UtcNow;

        // Act
        var event1 = new GoalCreatedEvent
        {
            GoalId = goalId,
            Name = name,
            TargetAmount = targetAmount,
            TargetDate = targetDate,
            Timestamp = timestamp
        };

        var event2 = new GoalCreatedEvent
        {
            GoalId = goalId,
            Name = name,
            TargetAmount = targetAmount,
            TargetDate = targetDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Event_WithDifferentProperties_AreNotEqual()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        var targetDate = new DateTime(2025, 1, 1);
        var timestamp = DateTime.UtcNow;

        var event1 = new GoalCreatedEvent
        {
            GoalId = goalId,
            Name = "Goal 1",
            TargetAmount = 5000m,
            TargetDate = targetDate,
            Timestamp = timestamp
        };

        var event2 = new GoalCreatedEvent
        {
            GoalId = goalId,
            Name = "Goal 2",
            TargetAmount = 5000m,
            TargetDate = targetDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }

    [Test]
    public void Event_Properties_AreInitOnly()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        var name = "House Down Payment";
        var targetAmount = 50000m;
        var targetDate = new DateTime(2028, 1, 1);

        // Act
        var evt = new GoalCreatedEvent
        {
            GoalId = goalId,
            Name = name,
            TargetAmount = targetAmount,
            TargetDate = targetDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.GoalId, Is.EqualTo(goalId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.TargetAmount, Is.EqualTo(targetAmount));
            Assert.That(evt.TargetDate, Is.EqualTo(targetDate));
        });
    }

    [Test]
    public void Event_ToString_ContainsTypeName()
    {
        // Arrange
        var evt = new GoalCreatedEvent
        {
            GoalId = Guid.NewGuid(),
            Name = "Test Goal",
            TargetAmount = 1000m,
            TargetDate = DateTime.UtcNow
        };

        // Act
        var result = evt.ToString();

        // Assert
        Assert.That(result, Does.Contain("GoalCreatedEvent"));
    }
}
