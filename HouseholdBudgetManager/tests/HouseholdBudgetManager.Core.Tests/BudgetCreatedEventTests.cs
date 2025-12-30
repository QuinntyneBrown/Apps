// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HouseholdBudgetManager.Core.Tests;

public class BudgetCreatedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesBudgetCreatedEvent()
    {
        // Arrange
        var budgetId = Guid.NewGuid();
        var name = "Monthly Budget";
        var period = "January 2025";
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new BudgetCreatedEvent
        {
            BudgetId = budgetId,
            Name = name,
            Period = period,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.BudgetId, Is.EqualTo(budgetId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.Period, Is.EqualTo(period));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void DefaultConstructor_SetsDefaultTimestamp()
    {
        // Act
        var evt = new BudgetCreatedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.BudgetId, Is.EqualTo(Guid.Empty));
            Assert.That(evt.Name, Is.EqualTo(string.Empty));
            Assert.That(evt.Period, Is.EqualTo(string.Empty));
            Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        // Arrange
        var budgetId = Guid.NewGuid();
        var name = "Test Budget";
        var period = "Q1 2025";
        var timestamp = new DateTime(2025, 1, 1);

        var evt1 = new BudgetCreatedEvent
        {
            BudgetId = budgetId,
            Name = name,
            Period = period,
            Timestamp = timestamp
        };

        var evt2 = new BudgetCreatedEvent
        {
            BudgetId = budgetId,
            Name = name,
            Period = period,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void Record_Inequality_WorksCorrectly()
    {
        // Arrange
        var evt1 = new BudgetCreatedEvent
        {
            BudgetId = Guid.NewGuid(),
            Name = "Budget 1",
            Period = "January",
            Timestamp = DateTime.UtcNow
        };

        var evt2 = new BudgetCreatedEvent
        {
            BudgetId = Guid.NewGuid(),
            Name = "Budget 2",
            Period = "February",
            Timestamp = DateTime.UtcNow
        };

        // Assert
        Assert.That(evt1, Is.Not.EqualTo(evt2));
    }

    [Test]
    public void With_Expression_CreatesNewInstance()
    {
        // Arrange
        var original = new BudgetCreatedEvent
        {
            BudgetId = Guid.NewGuid(),
            Name = "Original",
            Period = "January",
            Timestamp = DateTime.UtcNow
        };

        // Act
        var modified = original with { Name = "Modified" };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(modified.BudgetId, Is.EqualTo(original.BudgetId));
            Assert.That(modified.Name, Is.EqualTo("Modified"));
            Assert.That(modified.Period, Is.EqualTo(original.Period));
            Assert.That(modified.Timestamp, Is.EqualTo(original.Timestamp));
            Assert.That(modified, Is.Not.SameAs(original));
        });
    }

    [Test]
    public void BudgetId_CanBeSetAndRetrieved()
    {
        // Arrange
        var budgetId = Guid.NewGuid();
        var evt = new BudgetCreatedEvent { BudgetId = budgetId };

        // Assert
        Assert.That(evt.BudgetId, Is.EqualTo(budgetId));
    }

    [Test]
    public void Name_CanBeSetAndRetrieved()
    {
        // Arrange
        var name = "Annual Budget";
        var evt = new BudgetCreatedEvent { Name = name };

        // Assert
        Assert.That(evt.Name, Is.EqualTo(name));
    }

    [Test]
    public void Period_CanBeSetAndRetrieved()
    {
        // Arrange
        var period = "Q4 2025";
        var evt = new BudgetCreatedEvent { Period = period };

        // Assert
        Assert.That(evt.Period, Is.EqualTo(period));
    }
}
