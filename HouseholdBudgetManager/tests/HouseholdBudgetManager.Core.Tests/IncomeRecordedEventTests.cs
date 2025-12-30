// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HouseholdBudgetManager.Core.Tests;

public class IncomeRecordedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesIncomeRecordedEvent()
    {
        // Arrange
        var incomeId = Guid.NewGuid();
        var budgetId = Guid.NewGuid();
        var amount = 5000m;
        var source = "Employer";
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new IncomeRecordedEvent
        {
            IncomeId = incomeId,
            BudgetId = budgetId,
            Amount = amount,
            Source = source,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.IncomeId, Is.EqualTo(incomeId));
            Assert.That(evt.BudgetId, Is.EqualTo(budgetId));
            Assert.That(evt.Amount, Is.EqualTo(amount));
            Assert.That(evt.Source, Is.EqualTo(source));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void DefaultConstructor_SetsDefaultTimestamp()
    {
        // Act
        var evt = new IncomeRecordedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.IncomeId, Is.EqualTo(Guid.Empty));
            Assert.That(evt.BudgetId, Is.EqualTo(Guid.Empty));
            Assert.That(evt.Amount, Is.EqualTo(0m));
            Assert.That(evt.Source, Is.EqualTo(string.Empty));
            Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        // Arrange
        var incomeId = Guid.NewGuid();
        var budgetId = Guid.NewGuid();
        var amount = 3000m;
        var source = "Freelance";
        var timestamp = new DateTime(2025, 1, 1);

        var evt1 = new IncomeRecordedEvent
        {
            IncomeId = incomeId,
            BudgetId = budgetId,
            Amount = amount,
            Source = source,
            Timestamp = timestamp
        };

        var evt2 = new IncomeRecordedEvent
        {
            IncomeId = incomeId,
            BudgetId = budgetId,
            Amount = amount,
            Source = source,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void Record_Inequality_WorksCorrectly()
    {
        // Arrange
        var evt1 = new IncomeRecordedEvent
        {
            IncomeId = Guid.NewGuid(),
            BudgetId = Guid.NewGuid(),
            Amount = 1000m,
            Source = "Source 1",
            Timestamp = DateTime.UtcNow
        };

        var evt2 = new IncomeRecordedEvent
        {
            IncomeId = Guid.NewGuid(),
            BudgetId = Guid.NewGuid(),
            Amount = 2000m,
            Source = "Source 2",
            Timestamp = DateTime.UtcNow
        };

        // Assert
        Assert.That(evt1, Is.Not.EqualTo(evt2));
    }

    [Test]
    public void With_Expression_CreatesNewInstance()
    {
        // Arrange
        var original = new IncomeRecordedEvent
        {
            IncomeId = Guid.NewGuid(),
            BudgetId = Guid.NewGuid(),
            Amount = 1000m,
            Source = "Original Source",
            Timestamp = DateTime.UtcNow
        };

        // Act
        var modified = original with { Source = "Modified Source" };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(modified.IncomeId, Is.EqualTo(original.IncomeId));
            Assert.That(modified.BudgetId, Is.EqualTo(original.BudgetId));
            Assert.That(modified.Amount, Is.EqualTo(original.Amount));
            Assert.That(modified.Source, Is.EqualTo("Modified Source"));
            Assert.That(modified.Timestamp, Is.EqualTo(original.Timestamp));
            Assert.That(modified, Is.Not.SameAs(original));
        });
    }

    [Test]
    public void Amount_DecimalValue_IsPreserved()
    {
        // Arrange
        var amount = 4567.89m;
        var evt = new IncomeRecordedEvent { Amount = amount };

        // Assert
        Assert.That(evt.Amount, Is.EqualTo(amount));
    }

    [Test]
    public void Source_CanBeSetAndRetrieved()
    {
        // Arrange
        var source = "Investment Returns";
        var evt = new IncomeRecordedEvent { Source = source };

        // Assert
        Assert.That(evt.Source, Is.EqualTo(source));
    }

    [Test]
    public void IncomeId_CanBeSetAndRetrieved()
    {
        // Arrange
        var incomeId = Guid.NewGuid();
        var evt = new IncomeRecordedEvent { IncomeId = incomeId };

        // Assert
        Assert.That(evt.IncomeId, Is.EqualTo(incomeId));
    }

    [Test]
    public void BudgetId_CanBeSetAndRetrieved()
    {
        // Arrange
        var budgetId = Guid.NewGuid();
        var evt = new IncomeRecordedEvent { BudgetId = budgetId };

        // Assert
        Assert.That(evt.BudgetId, Is.EqualTo(budgetId));
    }

    [Test]
    public void Source_EmptyString_CanBeSet()
    {
        // Arrange & Act
        var evt = new IncomeRecordedEvent { Source = "" };

        // Assert
        Assert.That(evt.Source, Is.EqualTo(string.Empty));
    }
}
