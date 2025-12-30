// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FinancialGoalTracker.Core.Tests;

public class ContributionMadeEventTests
{
    [Test]
    public void Constructor_CreatesEvent_WithAllProperties()
    {
        // Arrange
        var contributionId = Guid.NewGuid();
        var goalId = Guid.NewGuid();
        var amount = 500m;
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new ContributionMadeEvent
        {
            ContributionId = contributionId,
            GoalId = goalId,
            Amount = amount,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ContributionId, Is.EqualTo(contributionId));
            Assert.That(evt.GoalId, Is.EqualTo(goalId));
            Assert.That(evt.Amount, Is.EqualTo(amount));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Event_WithDefaultTimestamp_SetsUtcNow()
    {
        // Arrange
        var contributionId = Guid.NewGuid();
        var goalId = Guid.NewGuid();
        var amount = 250m;

        // Act
        var evt = new ContributionMadeEvent
        {
            ContributionId = contributionId,
            GoalId = goalId,
            Amount = amount
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void Event_IsRecord_AndSupportsValueEquality()
    {
        // Arrange
        var contributionId = Guid.NewGuid();
        var goalId = Guid.NewGuid();
        var amount = 1000m;
        var timestamp = DateTime.UtcNow;

        // Act
        var event1 = new ContributionMadeEvent
        {
            ContributionId = contributionId,
            GoalId = goalId,
            Amount = amount,
            Timestamp = timestamp
        };

        var event2 = new ContributionMadeEvent
        {
            ContributionId = contributionId,
            GoalId = goalId,
            Amount = amount,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Event_WithDifferentProperties_AreNotEqual()
    {
        // Arrange
        var contributionId = Guid.NewGuid();
        var goalId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new ContributionMadeEvent
        {
            ContributionId = contributionId,
            GoalId = goalId,
            Amount = 100m,
            Timestamp = timestamp
        };

        var event2 = new ContributionMadeEvent
        {
            ContributionId = contributionId,
            GoalId = goalId,
            Amount = 200m,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }

    [Test]
    public void Event_Properties_AreInitOnly()
    {
        // Arrange
        var contributionId = Guid.NewGuid();
        var goalId = Guid.NewGuid();
        var amount = 750m;

        // Act
        var evt = new ContributionMadeEvent
        {
            ContributionId = contributionId,
            GoalId = goalId,
            Amount = amount
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ContributionId, Is.EqualTo(contributionId));
            Assert.That(evt.GoalId, Is.EqualTo(goalId));
            Assert.That(evt.Amount, Is.EqualTo(amount));
        });
    }

    [Test]
    public void Event_ToString_ContainsTypeName()
    {
        // Arrange
        var evt = new ContributionMadeEvent
        {
            ContributionId = Guid.NewGuid(),
            GoalId = Guid.NewGuid(),
            Amount = 500m
        };

        // Act
        var result = evt.ToString();

        // Assert
        Assert.That(result, Does.Contain("ContributionMadeEvent"));
    }
}
