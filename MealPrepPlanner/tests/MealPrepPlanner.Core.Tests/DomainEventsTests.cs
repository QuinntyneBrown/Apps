// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MealPrepPlanner.Core.Tests;

public class DomainEventsTests
{
    [Test]
    public void GroceryListCreatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var groceryListId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Weekly Groceries";
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new GroceryListCreatedEvent
        {
            GroceryListId = groceryListId,
            UserId = userId,
            Name = name,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.GroceryListId, Is.EqualTo(groceryListId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void GroceryListCreatedEvent_DefaultTimestamp_IsSet()
    {
        // Act
        var evt = new GroceryListCreatedEvent
        {
            GroceryListId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test List"
        };

        // Assert
        Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
    }

    [Test]
    public void MealPlanCreatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var mealPlanId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Weekly Plan";
        var startDate = DateTime.UtcNow;
        var endDate = DateTime.UtcNow.AddDays(7);
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new MealPlanCreatedEvent
        {
            MealPlanId = mealPlanId,
            UserId = userId,
            Name = name,
            StartDate = startDate,
            EndDate = endDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.MealPlanId, Is.EqualTo(mealPlanId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.StartDate, Is.EqualTo(startDate));
            Assert.That(evt.EndDate, Is.EqualTo(endDate));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void MealPlanCreatedEvent_DefaultTimestamp_IsSet()
    {
        // Act
        var evt = new MealPlanCreatedEvent
        {
            MealPlanId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Plan",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(7)
        };

        // Assert
        Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
    }

    [Test]
    public void NutritionCalculatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var nutritionId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var calories = 350;
        var protein = 25.5m;
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new NutritionCalculatedEvent
        {
            NutritionId = nutritionId,
            UserId = userId,
            Calories = calories,
            Protein = protein,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.NutritionId, Is.EqualTo(nutritionId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Calories, Is.EqualTo(calories));
            Assert.That(evt.Protein, Is.EqualTo(protein));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void NutritionCalculatedEvent_DefaultTimestamp_IsSet()
    {
        // Act
        var evt = new NutritionCalculatedEvent
        {
            NutritionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Calories = 350,
            Protein = 25.5m
        };

        // Assert
        Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
    }

    [Test]
    public void RecipeCreatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var recipeId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Grilled Chicken";
        var mealType = "Dinner";
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new RecipeCreatedEvent
        {
            RecipeId = recipeId,
            UserId = userId,
            Name = name,
            MealType = mealType,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.RecipeId, Is.EqualTo(recipeId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.MealType, Is.EqualTo(mealType));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void RecipeCreatedEvent_DefaultTimestamp_IsSet()
    {
        // Act
        var evt = new RecipeCreatedEvent
        {
            RecipeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Recipe",
            MealType = "Dinner"
        };

        // Assert
        Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
    }
}
