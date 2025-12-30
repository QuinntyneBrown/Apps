// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MealPrepPlanner.Core.Tests;

public class MealPlanTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesMealPlan()
    {
        // Arrange
        var mealPlanId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Weekly Meal Plan";
        var startDate = DateTime.UtcNow;
        var endDate = DateTime.UtcNow.AddDays(7);
        var dailyCalorieTarget = 2000;
        var dietaryPreferences = "Vegetarian";
        var notes = "Focus on protein";

        // Act
        var mealPlan = new MealPlan
        {
            MealPlanId = mealPlanId,
            UserId = userId,
            Name = name,
            StartDate = startDate,
            EndDate = endDate,
            DailyCalorieTarget = dailyCalorieTarget,
            DietaryPreferences = dietaryPreferences,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(mealPlan.MealPlanId, Is.EqualTo(mealPlanId));
            Assert.That(mealPlan.UserId, Is.EqualTo(userId));
            Assert.That(mealPlan.Name, Is.EqualTo(name));
            Assert.That(mealPlan.StartDate, Is.EqualTo(startDate));
            Assert.That(mealPlan.EndDate, Is.EqualTo(endDate));
            Assert.That(mealPlan.DailyCalorieTarget, Is.EqualTo(dailyCalorieTarget));
            Assert.That(mealPlan.DietaryPreferences, Is.EqualTo(dietaryPreferences));
            Assert.That(mealPlan.Notes, Is.EqualTo(notes));
            Assert.That(mealPlan.IsActive, Is.True);
            Assert.That(mealPlan.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_DefaultValues_InitializesCorrectly()
    {
        // Act
        var mealPlan = new MealPlan();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(mealPlan.Name, Is.EqualTo(string.Empty));
            Assert.That(mealPlan.IsActive, Is.True);
            Assert.That(mealPlan.DailyCalorieTarget, Is.Null);
            Assert.That(mealPlan.DietaryPreferences, Is.Null);
            Assert.That(mealPlan.Notes, Is.Null);
            Assert.That(mealPlan.Recipes, Is.Not.Null);
            Assert.That(mealPlan.Recipes.Count, Is.EqualTo(0));
            Assert.That(mealPlan.GroceryLists, Is.Not.Null);
            Assert.That(mealPlan.GroceryLists.Count, Is.EqualTo(0));
            Assert.That(mealPlan.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void GetDuration_ValidDateRange_ReturnsCorrectDuration()
    {
        // Arrange
        var startDate = new DateTime(2025, 1, 1);
        var endDate = new DateTime(2025, 1, 7);
        var mealPlan = new MealPlan
        {
            MealPlanId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Plan",
            StartDate = startDate,
            EndDate = endDate
        };

        // Act
        var duration = mealPlan.GetDuration();

        // Assert
        Assert.That(duration, Is.EqualTo(7)); // 7 days inclusive
    }

    [Test]
    public void GetDuration_SingleDay_ReturnsOne()
    {
        // Arrange
        var date = new DateTime(2025, 1, 1);
        var mealPlan = new MealPlan
        {
            MealPlanId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Plan",
            StartDate = date,
            EndDate = date
        };

        // Act
        var duration = mealPlan.GetDuration();

        // Assert
        Assert.That(duration, Is.EqualTo(1));
    }

    [Test]
    public void GetDuration_ThirtyDays_ReturnsThirty()
    {
        // Arrange
        var startDate = new DateTime(2025, 1, 1);
        var endDate = new DateTime(2025, 1, 30);
        var mealPlan = new MealPlan
        {
            MealPlanId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Plan",
            StartDate = startDate,
            EndDate = endDate
        };

        // Act
        var duration = mealPlan.GetDuration();

        // Assert
        Assert.That(duration, Is.EqualTo(30));
    }

    [Test]
    public void IsCurrentlyActive_ActivePlanWithCurrentDate_ReturnsTrue()
    {
        // Arrange
        var today = DateTime.UtcNow;
        var mealPlan = new MealPlan
        {
            MealPlanId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Plan",
            StartDate = today.AddDays(-1),
            EndDate = today.AddDays(1),
            IsActive = true
        };

        // Act
        var result = mealPlan.IsCurrentlyActive();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsCurrentlyActive_InactivePlan_ReturnsFalse()
    {
        // Arrange
        var today = DateTime.UtcNow;
        var mealPlan = new MealPlan
        {
            MealPlanId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Plan",
            StartDate = today.AddDays(-1),
            EndDate = today.AddDays(1),
            IsActive = false
        };

        // Act
        var result = mealPlan.IsCurrentlyActive();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsCurrentlyActive_FuturePlan_ReturnsFalse()
    {
        // Arrange
        var today = DateTime.UtcNow;
        var mealPlan = new MealPlan
        {
            MealPlanId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Plan",
            StartDate = today.AddDays(1),
            EndDate = today.AddDays(7),
            IsActive = true
        };

        // Act
        var result = mealPlan.IsCurrentlyActive();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsCurrentlyActive_PastPlan_ReturnsFalse()
    {
        // Arrange
        var today = DateTime.UtcNow;
        var mealPlan = new MealPlan
        {
            MealPlanId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Plan",
            StartDate = today.AddDays(-7),
            EndDate = today.AddDays(-1),
            IsActive = true
        };

        // Act
        var result = mealPlan.IsCurrentlyActive();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsCurrentlyActive_StartDateIsToday_ReturnsTrue()
    {
        // Arrange
        var today = DateTime.UtcNow;
        var mealPlan = new MealPlan
        {
            MealPlanId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Plan",
            StartDate = today,
            EndDate = today.AddDays(7),
            IsActive = true
        };

        // Act
        var result = mealPlan.IsCurrentlyActive();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsCurrentlyActive_EndDateIsToday_ReturnsTrue()
    {
        // Arrange
        var today = DateTime.UtcNow;
        var mealPlan = new MealPlan
        {
            MealPlanId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Plan",
            StartDate = today.AddDays(-7),
            EndDate = today,
            IsActive = true
        };

        // Act
        var result = mealPlan.IsCurrentlyActive();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void Recipes_CanAddRecipes_ToCollection()
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

        var recipe = new Recipe
        {
            RecipeId = Guid.NewGuid(),
            UserId = mealPlan.UserId,
            Name = "Test Recipe",
            MealPlanId = mealPlan.MealPlanId
        };

        // Act
        mealPlan.Recipes.Add(recipe);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(mealPlan.Recipes.Count, Is.EqualTo(1));
            Assert.That(mealPlan.Recipes.First().RecipeId, Is.EqualTo(recipe.RecipeId));
        });
    }

    [Test]
    public void GroceryLists_CanAddGroceryLists_ToCollection()
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
            MealPlanId = mealPlan.MealPlanId
        };

        // Act
        mealPlan.GroceryLists.Add(groceryList);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(mealPlan.GroceryLists.Count, Is.EqualTo(1));
            Assert.That(mealPlan.GroceryLists.First().GroceryListId, Is.EqualTo(groceryList.GroceryListId));
        });
    }

    [Test]
    public void DailyCalorieTarget_CanBeSet_ToPositiveValue()
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

        // Act
        mealPlan.DailyCalorieTarget = 2500;

        // Assert
        Assert.That(mealPlan.DailyCalorieTarget, Is.EqualTo(2500));
    }

    [Test]
    public void IsActive_CanBeToggled()
    {
        // Arrange
        var mealPlan = new MealPlan
        {
            MealPlanId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Plan",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(7),
            IsActive = true
        };

        // Act
        mealPlan.IsActive = false;

        // Assert
        Assert.That(mealPlan.IsActive, Is.False);
    }
}
