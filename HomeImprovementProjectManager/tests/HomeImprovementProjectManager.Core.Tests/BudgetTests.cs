// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeImprovementProjectManager.Core.Tests;

public class BudgetTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesBudget()
    {
        // Arrange
        var budgetId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var category = "Materials";
        var allocatedAmount = 5000m;
        var spentAmount = 2500m;

        // Act
        var budget = new Budget
        {
            BudgetId = budgetId,
            ProjectId = projectId,
            Category = category,
            AllocatedAmount = allocatedAmount,
            SpentAmount = spentAmount
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(budget.BudgetId, Is.EqualTo(budgetId));
            Assert.That(budget.ProjectId, Is.EqualTo(projectId));
            Assert.That(budget.Category, Is.EqualTo(category));
            Assert.That(budget.AllocatedAmount, Is.EqualTo(allocatedAmount));
            Assert.That(budget.SpentAmount, Is.EqualTo(spentAmount));
        });
    }

    [Test]
    public void Budget_DefaultValues_AreSetCorrectly()
    {
        // Act
        var budget = new Budget();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(budget.Category, Is.EqualTo(string.Empty));
            Assert.That(budget.AllocatedAmount, Is.EqualTo(0m));
            Assert.That(budget.SpentAmount, Is.Null);
            Assert.That(budget.Project, Is.Null);
            Assert.That(budget.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Budget_WithoutSpentAmount_IsValid()
    {
        // Arrange & Act
        var budget = new Budget
        {
            BudgetId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            Category = "Labor",
            AllocatedAmount = 10000m
        };

        // Assert
        Assert.That(budget.SpentAmount, Is.Null);
    }

    [Test]
    public void Budget_WithZeroAllocatedAmount_IsValid()
    {
        // Arrange & Act
        var budget = new Budget
        {
            BudgetId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            Category = "Contingency",
            AllocatedAmount = 0m
        };

        // Assert
        Assert.That(budget.AllocatedAmount, Is.EqualTo(0m));
    }

    [Test]
    public void Budget_CreatedAt_IsSetToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var budget = new Budget
        {
            BudgetId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            Category = "Test",
            AllocatedAmount = 1000m
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(budget.CreatedAt, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void Budget_SpentAmountExceedsAllocated_CanBeSet()
    {
        // Arrange & Act
        var budget = new Budget
        {
            BudgetId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            Category = "Materials",
            AllocatedAmount = 1000m,
            SpentAmount = 1500m
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(budget.AllocatedAmount, Is.EqualTo(1000m));
            Assert.That(budget.SpentAmount, Is.EqualTo(1500m));
        });
    }

    [Test]
    public void Budget_WithVariousCategories_IsValid()
    {
        // Arrange
        var categories = new[] { "Materials", "Labor", "Permits", "Equipment", "Other" };

        // Act & Assert
        foreach (var category in categories)
        {
            var budget = new Budget
            {
                BudgetId = Guid.NewGuid(),
                ProjectId = Guid.NewGuid(),
                Category = category,
                AllocatedAmount = 5000m
            };

            Assert.That(budget.Category, Is.EqualTo(category));
        }
    }

    [Test]
    public void Budget_CanUpdateSpentAmount()
    {
        // Arrange
        var budget = new Budget
        {
            BudgetId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            Category = "Materials",
            AllocatedAmount = 5000m,
            SpentAmount = 1000m
        };
        var newSpentAmount = 2500m;

        // Act
        budget.SpentAmount = newSpentAmount;

        // Assert
        Assert.That(budget.SpentAmount, Is.EqualTo(newSpentAmount));
    }

    [Test]
    public void Budget_WithLargeAllocatedAmount_IsValid()
    {
        // Arrange
        var largeAmount = 999999.99m;

        // Act
        var budget = new Budget
        {
            BudgetId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            Category = "Construction",
            AllocatedAmount = largeAmount
        };

        // Assert
        Assert.That(budget.AllocatedAmount, Is.EqualTo(largeAmount));
    }

    [Test]
    public void Budget_AllProperties_CanBeSet()
    {
        // Arrange
        var budgetId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var category = "Complete Category";
        var allocatedAmount = 15000m;
        var spentAmount = 7500m;
        var createdAt = DateTime.UtcNow.AddDays(-15);

        // Act
        var budget = new Budget
        {
            BudgetId = budgetId,
            ProjectId = projectId,
            Category = category,
            AllocatedAmount = allocatedAmount,
            SpentAmount = spentAmount,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(budget.BudgetId, Is.EqualTo(budgetId));
            Assert.That(budget.ProjectId, Is.EqualTo(projectId));
            Assert.That(budget.Category, Is.EqualTo(category));
            Assert.That(budget.AllocatedAmount, Is.EqualTo(allocatedAmount));
            Assert.That(budget.SpentAmount, Is.EqualTo(spentAmount));
            Assert.That(budget.CreatedAt, Is.EqualTo(createdAt));
        });
    }
}
