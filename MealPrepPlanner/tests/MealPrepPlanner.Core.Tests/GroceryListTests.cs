// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MealPrepPlanner.Core.Tests;

public class GroceryListTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesGroceryList()
    {
        // Arrange
        var groceryListId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var mealPlanId = Guid.NewGuid();
        var name = "Weekly Groceries";
        var items = "[\"Milk\", \"Eggs\", \"Bread\"]";
        var shoppingDate = DateTime.UtcNow.AddDays(1);
        var estimatedCost = 45.50m;
        var notes = "Don't forget coupons";

        // Act
        var groceryList = new GroceryList
        {
            GroceryListId = groceryListId,
            UserId = userId,
            MealPlanId = mealPlanId,
            Name = name,
            Items = items,
            ShoppingDate = shoppingDate,
            EstimatedCost = estimatedCost,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(groceryList.GroceryListId, Is.EqualTo(groceryListId));
            Assert.That(groceryList.UserId, Is.EqualTo(userId));
            Assert.That(groceryList.MealPlanId, Is.EqualTo(mealPlanId));
            Assert.That(groceryList.Name, Is.EqualTo(name));
            Assert.That(groceryList.Items, Is.EqualTo(items));
            Assert.That(groceryList.ShoppingDate, Is.EqualTo(shoppingDate));
            Assert.That(groceryList.EstimatedCost, Is.EqualTo(estimatedCost));
            Assert.That(groceryList.Notes, Is.EqualTo(notes));
            Assert.That(groceryList.IsCompleted, Is.False);
            Assert.That(groceryList.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_DefaultValues_InitializesCorrectly()
    {
        // Act
        var groceryList = new GroceryList();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(groceryList.Name, Is.EqualTo(string.Empty));
            Assert.That(groceryList.Items, Is.EqualTo("[]"));
            Assert.That(groceryList.IsCompleted, Is.False);
            Assert.That(groceryList.ShoppingDate, Is.Null);
            Assert.That(groceryList.EstimatedCost, Is.Null);
            Assert.That(groceryList.Notes, Is.Null);
            Assert.That(groceryList.MealPlanId, Is.Null);
            Assert.That(groceryList.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Complete_WhenCalled_SetsIsCompletedToTrue()
    {
        // Arrange
        var groceryList = new GroceryList
        {
            GroceryListId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test List",
            IsCompleted = false
        };

        // Act
        groceryList.Complete();

        // Assert
        Assert.That(groceryList.IsCompleted, Is.True);
    }

    [Test]
    public void Complete_WhenAlreadyCompleted_RemainsCompleted()
    {
        // Arrange
        var groceryList = new GroceryList
        {
            GroceryListId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test List",
            IsCompleted = true
        };

        // Act
        groceryList.Complete();

        // Assert
        Assert.That(groceryList.IsCompleted, Is.True);
    }

    [Test]
    public void IsScheduledForToday_WhenShoppingDateIsToday_ReturnsTrue()
    {
        // Arrange
        var groceryList = new GroceryList
        {
            GroceryListId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test List",
            ShoppingDate = DateTime.UtcNow
        };

        // Act
        var result = groceryList.IsScheduledForToday();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsScheduledForToday_WhenShoppingDateIsTomorrow_ReturnsFalse()
    {
        // Arrange
        var groceryList = new GroceryList
        {
            GroceryListId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test List",
            ShoppingDate = DateTime.UtcNow.AddDays(1)
        };

        // Act
        var result = groceryList.IsScheduledForToday();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsScheduledForToday_WhenShoppingDateIsYesterday_ReturnsFalse()
    {
        // Arrange
        var groceryList = new GroceryList
        {
            GroceryListId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test List",
            ShoppingDate = DateTime.UtcNow.AddDays(-1)
        };

        // Act
        var result = groceryList.IsScheduledForToday();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsScheduledForToday_WhenShoppingDateIsNull_ReturnsFalse()
    {
        // Arrange
        var groceryList = new GroceryList
        {
            GroceryListId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test List",
            ShoppingDate = null
        };

        // Act
        var result = groceryList.IsScheduledForToday();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void Items_DefaultValue_IsEmptyJsonArray()
    {
        // Arrange & Act
        var groceryList = new GroceryList();

        // Assert
        Assert.That(groceryList.Items, Is.EqualTo("[]"));
    }

    [Test]
    public void MealPlan_CanBeAssociated_WithGroceryList()
    {
        // Arrange
        var mealPlan = new MealPlan
        {
            MealPlanId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Plan",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(7)
        };

        var groceryList = new GroceryList
        {
            GroceryListId = Guid.NewGuid(),
            UserId = mealPlan.UserId,
            Name = "Test List",
            MealPlanId = mealPlan.MealPlanId,
            MealPlan = mealPlan
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(groceryList.MealPlanId, Is.EqualTo(mealPlan.MealPlanId));
            Assert.That(groceryList.MealPlan, Is.Not.Null);
            Assert.That(groceryList.MealPlan.Name, Is.EqualTo("Test Plan"));
        });
    }

    [Test]
    public void EstimatedCost_CanBeSetToDecimalValue()
    {
        // Arrange
        var groceryList = new GroceryList
        {
            GroceryListId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test List"
        };

        // Act
        groceryList.EstimatedCost = 123.45m;

        // Assert
        Assert.That(groceryList.EstimatedCost, Is.EqualTo(123.45m));
    }

    [Test]
    public void ShoppingDate_CanBeSetToFutureDate()
    {
        // Arrange
        var futureDate = DateTime.UtcNow.AddDays(5);
        var groceryList = new GroceryList
        {
            GroceryListId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test List"
        };

        // Act
        groceryList.ShoppingDate = futureDate;

        // Assert
        Assert.That(groceryList.ShoppingDate, Is.EqualTo(futureDate));
    }
}
