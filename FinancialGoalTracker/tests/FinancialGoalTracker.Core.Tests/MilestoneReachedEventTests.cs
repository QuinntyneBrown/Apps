// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FinancialGoalTracker.Core.Tests;

public class MilestoneReachedEventTests
{
    [Test]
    public void Constructor_CreatesEvent_WithAllProperties()
    {
        // Arrange
        var milestoneId = Guid.NewGuid();
        var goalId = Guid.NewGuid();
        var name = "50% Complete";
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new MilestoneReachedEvent
        {
            MilestoneId = milestoneId,
            GoalId = goalId,
            Name = name,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.MilestoneId, Is.EqualTo(milestoneId));
            Assert.That(evt.GoalId, Is.EqualTo(goalId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Event_WithDefaultTimestamp_SetsUtcNow()
    {
        // Arrange
        var milestoneId = Guid.NewGuid();
        var goalId = Guid.NewGuid();
        var name = "25% Complete";

        // Act
        var evt = new MilestoneReachedEvent
        {
            MilestoneId = milestoneId,
            GoalId = goalId,
            Name = name
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void Event_IsRecord_AndSupportsValueEquality()
    {
        // Arrange
        var milestoneId = Guid.NewGuid();
        var goalId = Guid.NewGuid();
        var name = "75% Complete";
        var timestamp = DateTime.UtcNow;

        // Act
        var event1 = new MilestoneReachedEvent
        {
            MilestoneId = milestoneId,
            GoalId = goalId,
            Name = name,
            Timestamp = timestamp
        };

        var event2 = new MilestoneReachedEvent
        {
            MilestoneId = milestoneId,
            GoalId = goalId,
            Name = name,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Event_WithDifferentProperties_AreNotEqual()
    {
        // Arrange
        var milestoneId = Guid.NewGuid();
        var goalId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new MilestoneReachedEvent
        {
            MilestoneId = milestoneId,
            GoalId = goalId,
            Name = "Milestone 1",
            Timestamp = timestamp
        };

        var event2 = new MilestoneReachedEvent
        {
            MilestoneId = milestoneId,
            GoalId = goalId,
            Name = "Milestone 2",
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }

    [Test]
    public void Event_Properties_AreInitOnly()
    {
        // Arrange
        var milestoneId = Guid.NewGuid();
        var goalId = Guid.NewGuid();
        var name = "Goal Achieved";

        // Act
        var evt = new MilestoneReachedEvent
        {
            MilestoneId = milestoneId,
            GoalId = goalId,
            Name = name
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.MilestoneId, Is.EqualTo(milestoneId));
            Assert.That(evt.GoalId, Is.EqualTo(goalId));
            Assert.That(evt.Name, Is.EqualTo(name));
        });
    }

    [Test]
    public void Event_ToString_ContainsTypeName()
    {
        // Arrange
        var evt = new MilestoneReachedEvent
        {
            MilestoneId = Guid.NewGuid(),
            GoalId = Guid.NewGuid(),
            Name = "Test Milestone"
        };

        // Act
        var result = evt.ToString();

        // Assert
        Assert.That(result, Does.Contain("MilestoneReachedEvent"));
    }
}
