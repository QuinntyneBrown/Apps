// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HouseholdBudgetManager.Core.Tests;

public class ExpenseRecordedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesExpenseRecordedEvent()
    {
        // Arrange
        var expenseId = Guid.NewGuid();
        var budgetId = Guid.NewGuid();
        var amount = 150.50m;
        var category = ExpenseCategory.Food;
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new ExpenseRecordedEvent
        {
            ExpenseId = expenseId,
            BudgetId = budgetId,
            Amount = amount,
            Category = category,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ExpenseId, Is.EqualTo(expenseId));
            Assert.That(evt.BudgetId, Is.EqualTo(budgetId));
            Assert.That(evt.Amount, Is.EqualTo(amount));
            Assert.That(evt.Category, Is.EqualTo(category));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void DefaultConstructor_SetsDefaultTimestamp()
    {
        // Act
        var evt = new ExpenseRecordedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ExpenseId, Is.EqualTo(Guid.Empty));
            Assert.That(evt.BudgetId, Is.EqualTo(Guid.Empty));
            Assert.That(evt.Amount, Is.EqualTo(0m));
            Assert.That(evt.Category, Is.EqualTo(ExpenseCategory.Housing));
            Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        // Arrange
        var expenseId = Guid.NewGuid();
        var budgetId = Guid.NewGuid();
        var amount = 100m;
        var category = ExpenseCategory.Transportation;
        var timestamp = new DateTime(2025, 1, 1);

        var evt1 = new ExpenseRecordedEvent
        {
            ExpenseId = expenseId,
            BudgetId = budgetId,
            Amount = amount,
            Category = category,
            Timestamp = timestamp
        };

        var evt2 = new ExpenseRecordedEvent
        {
            ExpenseId = expenseId,
            BudgetId = budgetId,
            Amount = amount,
            Category = category,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void Record_Inequality_WorksCorrectly()
    {
        // Arrange
        var evt1 = new ExpenseRecordedEvent
        {
            ExpenseId = Guid.NewGuid(),
            BudgetId = Guid.NewGuid(),
            Amount = 100m,
            Category = ExpenseCategory.Food,
            Timestamp = DateTime.UtcNow
        };

        var evt2 = new ExpenseRecordedEvent
        {
            ExpenseId = Guid.NewGuid(),
            BudgetId = Guid.NewGuid(),
            Amount = 200m,
            Category = ExpenseCategory.Healthcare,
            Timestamp = DateTime.UtcNow
        };

        // Assert
        Assert.That(evt1, Is.Not.EqualTo(evt2));
    }

    [Test]
    public void With_Expression_CreatesNewInstance()
    {
        // Arrange
        var original = new ExpenseRecordedEvent
        {
            ExpenseId = Guid.NewGuid(),
            BudgetId = Guid.NewGuid(),
            Amount = 100m,
            Category = ExpenseCategory.Food,
            Timestamp = DateTime.UtcNow
        };

        // Act
        var modified = original with { Amount = 200m };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(modified.ExpenseId, Is.EqualTo(original.ExpenseId));
            Assert.That(modified.BudgetId, Is.EqualTo(original.BudgetId));
            Assert.That(modified.Amount, Is.EqualTo(200m));
            Assert.That(modified.Category, Is.EqualTo(original.Category));
            Assert.That(modified.Timestamp, Is.EqualTo(original.Timestamp));
            Assert.That(modified, Is.Not.SameAs(original));
        });
    }

    [Test]
    public void Category_AllCategories_CanBeSet()
    {
        // Arrange & Act & Assert
        Assert.DoesNotThrow(() => new ExpenseRecordedEvent { Category = ExpenseCategory.Housing });
        Assert.DoesNotThrow(() => new ExpenseRecordedEvent { Category = ExpenseCategory.Transportation });
        Assert.DoesNotThrow(() => new ExpenseRecordedEvent { Category = ExpenseCategory.Food });
        Assert.DoesNotThrow(() => new ExpenseRecordedEvent { Category = ExpenseCategory.Healthcare });
        Assert.DoesNotThrow(() => new ExpenseRecordedEvent { Category = ExpenseCategory.Entertainment });
        Assert.DoesNotThrow(() => new ExpenseRecordedEvent { Category = ExpenseCategory.PersonalCare });
        Assert.DoesNotThrow(() => new ExpenseRecordedEvent { Category = ExpenseCategory.Education });
        Assert.DoesNotThrow(() => new ExpenseRecordedEvent { Category = ExpenseCategory.DebtPayment });
        Assert.DoesNotThrow(() => new ExpenseRecordedEvent { Category = ExpenseCategory.Savings });
        Assert.DoesNotThrow(() => new ExpenseRecordedEvent { Category = ExpenseCategory.Other });
    }

    [Test]
    public void Amount_DecimalValue_IsPreserved()
    {
        // Arrange
        var amount = 123.45m;
        var evt = new ExpenseRecordedEvent { Amount = amount };

        // Assert
        Assert.That(evt.Amount, Is.EqualTo(amount));
    }

    [Test]
    public void ExpenseId_CanBeSetAndRetrieved()
    {
        // Arrange
        var expenseId = Guid.NewGuid();
        var evt = new ExpenseRecordedEvent { ExpenseId = expenseId };

        // Assert
        Assert.That(evt.ExpenseId, Is.EqualTo(expenseId));
    }

    [Test]
    public void BudgetId_CanBeSetAndRetrieved()
    {
        // Arrange
        var budgetId = Guid.NewGuid();
        var evt = new ExpenseRecordedEvent { BudgetId = budgetId };

        // Assert
        Assert.That(evt.BudgetId, Is.EqualTo(budgetId));
    }
}
