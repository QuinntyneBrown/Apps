// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyVacationPlanner.Core.Tests;

public class VacationBudgetTests
{
    [Test]
    public void Constructor_CreatesVacationBudget_WithDefaultValues()
    {
        // Arrange & Act
        var budget = new VacationBudget();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(budget.VacationBudgetId, Is.EqualTo(Guid.Empty));
            Assert.That(budget.TripId, Is.EqualTo(Guid.Empty));
            Assert.That(budget.Trip, Is.Null);
            Assert.That(budget.Category, Is.EqualTo(string.Empty));
            Assert.That(budget.AllocatedAmount, Is.EqualTo(0m));
            Assert.That(budget.SpentAmount, Is.Null);
            Assert.That(budget.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void VacationBudgetId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var budget = new VacationBudget();
        var expectedId = Guid.NewGuid();

        // Act
        budget.VacationBudgetId = expectedId;

        // Assert
        Assert.That(budget.VacationBudgetId, Is.EqualTo(expectedId));
    }

    [Test]
    public void Category_CanBeSet_AndRetrieved()
    {
        // Arrange
        var budget = new VacationBudget();
        var expectedCategory = "Accommodation";

        // Act
        budget.Category = expectedCategory;

        // Assert
        Assert.That(budget.Category, Is.EqualTo(expectedCategory));
    }

    [Test]
    public void AllocatedAmount_CanBeSet_AndRetrieved()
    {
        // Arrange
        var budget = new VacationBudget();
        var expectedAmount = 2000.00m;

        // Act
        budget.AllocatedAmount = expectedAmount;

        // Assert
        Assert.That(budget.AllocatedAmount, Is.EqualTo(expectedAmount));
    }

    [Test]
    public void SpentAmount_CanBeSet_AndRetrieved()
    {
        // Arrange
        var budget = new VacationBudget();
        var expectedAmount = 1500.50m;

        // Act
        budget.SpentAmount = expectedAmount;

        // Assert
        Assert.That(budget.SpentAmount, Is.EqualTo(expectedAmount));
    }

    [Test]
    public void SpentAmount_CanBeNull()
    {
        // Arrange
        var budget = new VacationBudget();

        // Act
        budget.SpentAmount = null;

        // Assert
        Assert.That(budget.SpentAmount, Is.Null);
    }

    [Test]
    public void SpentAmount_CanBeZero()
    {
        // Arrange
        var budget = new VacationBudget();

        // Act
        budget.SpentAmount = 0m;

        // Assert
        Assert.That(budget.SpentAmount, Is.EqualTo(0m));
    }

    [Test]
    public void VacationBudget_WithAllProperties_CanBeCreatedAndRetrieved()
    {
        // Arrange
        var budgetId = Guid.NewGuid();
        var tripId = Guid.NewGuid();
        var category = "Food";
        var allocatedAmount = 1000.00m;
        var spentAmount = 750.25m;
        var createdAt = DateTime.UtcNow;

        // Act
        var budget = new VacationBudget
        {
            VacationBudgetId = budgetId,
            TripId = tripId,
            Category = category,
            AllocatedAmount = allocatedAmount,
            SpentAmount = spentAmount,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(budget.VacationBudgetId, Is.EqualTo(budgetId));
            Assert.That(budget.TripId, Is.EqualTo(tripId));
            Assert.That(budget.Category, Is.EqualTo(category));
            Assert.That(budget.AllocatedAmount, Is.EqualTo(allocatedAmount));
            Assert.That(budget.SpentAmount, Is.EqualTo(spentAmount));
            Assert.That(budget.CreatedAt, Is.EqualTo(createdAt));
        });
    }

    [Test]
    public void VacationBudget_ForDifferentCategories_CanBeCreated()
    {
        // Arrange & Act
        var accommodation = new VacationBudget { Category = "Accommodation", AllocatedAmount = 3000m };
        var food = new VacationBudget { Category = "Food", AllocatedAmount = 1000m };
        var activities = new VacationBudget { Category = "Activities", AllocatedAmount = 1500m };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(accommodation.Category, Is.EqualTo("Accommodation"));
            Assert.That(food.Category, Is.EqualTo("Food"));
            Assert.That(activities.Category, Is.EqualTo("Activities"));
        });
    }
}
