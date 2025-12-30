// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MealPrepPlanner.Core.Tests;

public class RecipeTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesRecipe()
    {
        // Arrange
        var recipeId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var mealPlanId = Guid.NewGuid();
        var name = "Grilled Chicken";
        var description = "Delicious grilled chicken breast";
        var prepTimeMinutes = 15;
        var cookTimeMinutes = 25;
        var servings = 4;
        var ingredients = "[\"Chicken\", \"Olive Oil\", \"Salt\"]";
        var instructions = "1. Season chicken\n2. Grill for 25 minutes";
        var mealType = "Dinner";
        var tags = "High-Protein,Gluten-Free";

        // Act
        var recipe = new Recipe
        {
            RecipeId = recipeId,
            UserId = userId,
            MealPlanId = mealPlanId,
            Name = name,
            Description = description,
            PrepTimeMinutes = prepTimeMinutes,
            CookTimeMinutes = cookTimeMinutes,
            Servings = servings,
            Ingredients = ingredients,
            Instructions = instructions,
            MealType = mealType,
            Tags = tags,
            IsFavorite = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(recipe.RecipeId, Is.EqualTo(recipeId));
            Assert.That(recipe.UserId, Is.EqualTo(userId));
            Assert.That(recipe.MealPlanId, Is.EqualTo(mealPlanId));
            Assert.That(recipe.Name, Is.EqualTo(name));
            Assert.That(recipe.Description, Is.EqualTo(description));
            Assert.That(recipe.PrepTimeMinutes, Is.EqualTo(prepTimeMinutes));
            Assert.That(recipe.CookTimeMinutes, Is.EqualTo(cookTimeMinutes));
            Assert.That(recipe.Servings, Is.EqualTo(servings));
            Assert.That(recipe.Ingredients, Is.EqualTo(ingredients));
            Assert.That(recipe.Instructions, Is.EqualTo(instructions));
            Assert.That(recipe.MealType, Is.EqualTo(mealType));
            Assert.That(recipe.Tags, Is.EqualTo(tags));
            Assert.That(recipe.IsFavorite, Is.True);
            Assert.That(recipe.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_DefaultValues_InitializesCorrectly()
    {
        // Act
        var recipe = new Recipe();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(recipe.Name, Is.EqualTo(string.Empty));
            Assert.That(recipe.Ingredients, Is.EqualTo(string.Empty));
            Assert.That(recipe.Instructions, Is.EqualTo(string.Empty));
            Assert.That(recipe.MealType, Is.EqualTo(string.Empty));
            Assert.That(recipe.Description, Is.Null);
            Assert.That(recipe.Tags, Is.Null);
            Assert.That(recipe.MealPlanId, Is.Null);
            Assert.That(recipe.IsFavorite, Is.False);
            Assert.That(recipe.PrepTimeMinutes, Is.EqualTo(0));
            Assert.That(recipe.CookTimeMinutes, Is.EqualTo(0));
            Assert.That(recipe.Servings, Is.EqualTo(0));
            Assert.That(recipe.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void GetTotalTime_ValidTimes_ReturnsCorrectSum()
    {
        // Arrange
        var recipe = new Recipe
        {
            RecipeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Recipe",
            PrepTimeMinutes = 15,
            CookTimeMinutes = 25
        };

        // Act
        var totalTime = recipe.GetTotalTime();

        // Assert
        Assert.That(totalTime, Is.EqualTo(40));
    }

    [Test]
    public void GetTotalTime_ZeroTimes_ReturnsZero()
    {
        // Arrange
        var recipe = new Recipe
        {
            RecipeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Recipe",
            PrepTimeMinutes = 0,
            CookTimeMinutes = 0
        };

        // Act
        var totalTime = recipe.GetTotalTime();

        // Assert
        Assert.That(totalTime, Is.EqualTo(0));
    }

    [Test]
    public void GetTotalTime_OnlyPrepTime_ReturnsPrepTime()
    {
        // Arrange
        var recipe = new Recipe
        {
            RecipeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Recipe",
            PrepTimeMinutes = 10,
            CookTimeMinutes = 0
        };

        // Act
        var totalTime = recipe.GetTotalTime();

        // Assert
        Assert.That(totalTime, Is.EqualTo(10));
    }

    [Test]
    public void IsQuickMeal_Under30Minutes_ReturnsTrue()
    {
        // Arrange
        var recipe = new Recipe
        {
            RecipeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Recipe",
            PrepTimeMinutes = 10,
            CookTimeMinutes = 15
        };

        // Act
        var result = recipe.IsQuickMeal();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsQuickMeal_Exactly30Minutes_ReturnsFalse()
    {
        // Arrange
        var recipe = new Recipe
        {
            RecipeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Recipe",
            PrepTimeMinutes = 15,
            CookTimeMinutes = 15
        };

        // Act
        var result = recipe.IsQuickMeal();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsQuickMeal_Over30Minutes_ReturnsFalse()
    {
        // Arrange
        var recipe = new Recipe
        {
            RecipeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Recipe",
            PrepTimeMinutes = 20,
            CookTimeMinutes = 25
        };

        // Act
        var result = recipe.IsQuickMeal();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsQuickMeal_29Minutes_ReturnsTrue()
    {
        // Arrange
        var recipe = new Recipe
        {
            RecipeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Recipe",
            PrepTimeMinutes = 14,
            CookTimeMinutes = 15
        };

        // Act
        var result = recipe.IsQuickMeal();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void ToggleFavorite_WhenFalse_SetsToTrue()
    {
        // Arrange
        var recipe = new Recipe
        {
            RecipeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Recipe",
            IsFavorite = false
        };

        // Act
        recipe.ToggleFavorite();

        // Assert
        Assert.That(recipe.IsFavorite, Is.True);
    }

    [Test]
    public void ToggleFavorite_WhenTrue_SetsToFalse()
    {
        // Arrange
        var recipe = new Recipe
        {
            RecipeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Recipe",
            IsFavorite = true
        };

        // Act
        recipe.ToggleFavorite();

        // Assert
        Assert.That(recipe.IsFavorite, Is.False);
    }

    [Test]
    public void ToggleFavorite_CalledTwice_ReturnsToOriginalState()
    {
        // Arrange
        var recipe = new Recipe
        {
            RecipeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Recipe",
            IsFavorite = false
        };

        // Act
        recipe.ToggleFavorite();
        recipe.ToggleFavorite();

        // Assert
        Assert.That(recipe.IsFavorite, Is.False);
    }

    [Test]
    public void MealPlan_CanBeAssociated_WithRecipe()
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
            MealPlanId = mealPlan.MealPlanId,
            MealPlan = mealPlan
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(recipe.MealPlanId, Is.EqualTo(mealPlan.MealPlanId));
            Assert.That(recipe.MealPlan, Is.Not.Null);
            Assert.That(recipe.MealPlan.Name, Is.EqualTo("Test Plan"));
        });
    }

    [Test]
    public void Servings_CanBeSetToPositiveValue()
    {
        // Arrange
        var recipe = new Recipe
        {
            RecipeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Recipe"
        };

        // Act
        recipe.Servings = 6;

        // Assert
        Assert.That(recipe.Servings, Is.EqualTo(6));
    }

    [Test]
    public void MealType_CanBeSetToDifferentTypes()
    {
        // Arrange
        var recipe = new Recipe
        {
            RecipeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Recipe"
        };

        // Act & Assert - Breakfast
        recipe.MealType = "Breakfast";
        Assert.That(recipe.MealType, Is.EqualTo("Breakfast"));

        // Act & Assert - Lunch
        recipe.MealType = "Lunch";
        Assert.That(recipe.MealType, Is.EqualTo("Lunch"));

        // Act & Assert - Dinner
        recipe.MealType = "Dinner";
        Assert.That(recipe.MealType, Is.EqualTo("Dinner"));

        // Act & Assert - Snack
        recipe.MealType = "Snack";
        Assert.That(recipe.MealType, Is.EqualTo("Snack"));
    }
}
