// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeImprovementProjectManager.Core.Tests;

public class BudgetCreatedEventTests
{
    [Test]
    public void BudgetCreatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var budgetId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var allocatedAmount = 5000m;
        var timestamp = DateTime.UtcNow;

        // Act
        var budgetEvent = new BudgetCreatedEvent
        {
            BudgetId = budgetId,
            ProjectId = projectId,
            AllocatedAmount = allocatedAmount,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(budgetEvent.BudgetId, Is.EqualTo(budgetId));
            Assert.That(budgetEvent.ProjectId, Is.EqualTo(projectId));
            Assert.That(budgetEvent.AllocatedAmount, Is.EqualTo(allocatedAmount));
            Assert.That(budgetEvent.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void BudgetCreatedEvent_DefaultValues_AreSetCorrectly()
    {
        // Act
        var budgetEvent = new BudgetCreatedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(budgetEvent.BudgetId, Is.EqualTo(Guid.Empty));
            Assert.That(budgetEvent.ProjectId, Is.EqualTo(Guid.Empty));
            Assert.That(budgetEvent.AllocatedAmount, Is.EqualTo(0m));
            Assert.That(budgetEvent.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void BudgetCreatedEvent_Timestamp_IsSetToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var budgetEvent = new BudgetCreatedEvent
        {
            BudgetId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            AllocatedAmount = 10000m
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(budgetEvent.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void BudgetCreatedEvent_WithZeroAmount_IsValid()
    {
        // Arrange & Act
        var budgetEvent = new BudgetCreatedEvent
        {
            BudgetId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            AllocatedAmount = 0m
        };

        // Assert
        Assert.That(budgetEvent.AllocatedAmount, Is.EqualTo(0m));
    }

    [Test]
    public void BudgetCreatedEvent_IsImmutable()
    {
        // Arrange
        var budgetId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var allocatedAmount = 15000m;

        // Act
        var budgetEvent = new BudgetCreatedEvent
        {
            BudgetId = budgetId,
            ProjectId = projectId,
            AllocatedAmount = allocatedAmount
        };

        // Assert - Record properties are init-only
        Assert.Multiple(() =>
        {
            Assert.That(budgetEvent.BudgetId, Is.EqualTo(budgetId));
            Assert.That(budgetEvent.ProjectId, Is.EqualTo(projectId));
            Assert.That(budgetEvent.AllocatedAmount, Is.EqualTo(allocatedAmount));
        });
    }

    [Test]
    public void BudgetCreatedEvent_EqualityByValue()
    {
        // Arrange
        var budgetId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var allocatedAmount = 7500m;
        var timestamp = DateTime.UtcNow;

        var event1 = new BudgetCreatedEvent
        {
            BudgetId = budgetId,
            ProjectId = projectId,
            AllocatedAmount = allocatedAmount,
            Timestamp = timestamp
        };

        var event2 = new BudgetCreatedEvent
        {
            BudgetId = budgetId,
            ProjectId = projectId,
            AllocatedAmount = allocatedAmount,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void BudgetCreatedEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new BudgetCreatedEvent
        {
            BudgetId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            AllocatedAmount = 5000m
        };

        var event2 = new BudgetCreatedEvent
        {
            BudgetId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            AllocatedAmount = 10000m
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }

    [Test]
    public void BudgetCreatedEvent_WithLargeAmount_IsValid()
    {
        // Arrange
        var largeAmount = 999999.99m;

        // Act
        var budgetEvent = new BudgetCreatedEvent
        {
            BudgetId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            AllocatedAmount = largeAmount
        };

        // Assert
        Assert.That(budgetEvent.AllocatedAmount, Is.EqualTo(largeAmount));
    }
}
