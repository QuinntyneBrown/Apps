// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MealPrepPlanner.Core.Tests;

public class NutritionTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesNutrition()
    {
        // Arrange
        var nutritionId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var recipeId = Guid.NewGuid();
        var calories = 350;
        var protein = 25.5m;
        var carbohydrates = 45.2m;
        var fat = 12.8m;
        var fiber = 5.5m;
        var sugar = 8.2m;
        var sodium = 450.0m;
        var additionalNutrients = "{\"VitaminC\": 50}";

        // Act
        var nutrition = new Nutrition
        {
            NutritionId = nutritionId,
            UserId = userId,
            RecipeId = recipeId,
            Calories = calories,
            Protein = protein,
            Carbohydrates = carbohydrates,
            Fat = fat,
            Fiber = fiber,
            Sugar = sugar,
            Sodium = sodium,
            AdditionalNutrients = additionalNutrients
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(nutrition.NutritionId, Is.EqualTo(nutritionId));
            Assert.That(nutrition.UserId, Is.EqualTo(userId));
            Assert.That(nutrition.RecipeId, Is.EqualTo(recipeId));
            Assert.That(nutrition.Calories, Is.EqualTo(calories));
            Assert.That(nutrition.Protein, Is.EqualTo(protein));
            Assert.That(nutrition.Carbohydrates, Is.EqualTo(carbohydrates));
            Assert.That(nutrition.Fat, Is.EqualTo(fat));
            Assert.That(nutrition.Fiber, Is.EqualTo(fiber));
            Assert.That(nutrition.Sugar, Is.EqualTo(sugar));
            Assert.That(nutrition.Sodium, Is.EqualTo(sodium));
            Assert.That(nutrition.AdditionalNutrients, Is.EqualTo(additionalNutrients));
            Assert.That(nutrition.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_DefaultValues_InitializesCorrectly()
    {
        // Act
        var nutrition = new Nutrition();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(nutrition.Calories, Is.EqualTo(0));
            Assert.That(nutrition.Protein, Is.EqualTo(0));
            Assert.That(nutrition.Carbohydrates, Is.EqualTo(0));
            Assert.That(nutrition.Fat, Is.EqualTo(0));
            Assert.That(nutrition.Fiber, Is.Null);
            Assert.That(nutrition.Sugar, Is.Null);
            Assert.That(nutrition.Sodium, Is.Null);
            Assert.That(nutrition.AdditionalNutrients, Is.Null);
            Assert.That(nutrition.RecipeId, Is.Null);
            Assert.That(nutrition.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void GetProteinCaloriesPercentage_ValidValues_ReturnsCorrectPercentage()
    {
        // Arrange
        var nutrition = new Nutrition
        {
            NutritionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Calories = 400,
            Protein = 25m, // 25g * 4 cal/g = 100 calories
            Carbohydrates = 50m,
            Fat = 10m
        };

        // Act
        var percentage = nutrition.GetProteinCaloriesPercentage();

        // Assert
        Assert.That(percentage, Is.EqualTo(25m)); // 100/400 * 100 = 25%
    }

    [Test]
    public void GetProteinCaloriesPercentage_ZeroCalories_ReturnsZero()
    {
        // Arrange
        var nutrition = new Nutrition
        {
            NutritionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Calories = 0,
            Protein = 25m,
            Carbohydrates = 50m,
            Fat = 10m
        };

        // Act
        var percentage = nutrition.GetProteinCaloriesPercentage();

        // Assert
        Assert.That(percentage, Is.EqualTo(0m));
    }

    [Test]
    public void GetProteinCaloriesPercentage_HighProtein_ReturnsHighPercentage()
    {
        // Arrange
        var nutrition = new Nutrition
        {
            NutritionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Calories = 200,
            Protein = 40m, // 40g * 4 cal/g = 160 calories
            Carbohydrates = 5m,
            Fat = 2m
        };

        // Act
        var percentage = nutrition.GetProteinCaloriesPercentage();

        // Assert
        Assert.That(percentage, Is.EqualTo(80m)); // 160/200 * 100 = 80%
    }

    [Test]
    public void IsHighProtein_ProteinAbove30Percent_ReturnsTrue()
    {
        // Arrange
        var nutrition = new Nutrition
        {
            NutritionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Calories = 400,
            Protein = 40m, // 40g * 4 cal/g = 160 calories = 40%
            Carbohydrates = 50m,
            Fat = 10m
        };

        // Act
        var result = nutrition.IsHighProtein();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsHighProtein_ProteinBelow30Percent_ReturnsFalse()
    {
        // Arrange
        var nutrition = new Nutrition
        {
            NutritionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Calories = 400,
            Protein = 20m, // 20g * 4 cal/g = 80 calories = 20%
            Carbohydrates = 50m,
            Fat = 10m
        };

        // Act
        var result = nutrition.IsHighProtein();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsHighProtein_ProteinExactly30Percent_ReturnsFalse()
    {
        // Arrange
        var nutrition = new Nutrition
        {
            NutritionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Calories = 400,
            Protein = 30m, // 30g * 4 cal/g = 120 calories = 30%
            Carbohydrates = 50m,
            Fat = 10m
        };

        // Act
        var result = nutrition.IsHighProtein();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsHighProtein_ProteinJustAbove30Percent_ReturnsTrue()
    {
        // Arrange
        var nutrition = new Nutrition
        {
            NutritionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Calories = 400,
            Protein = 30.5m, // 30.5g * 4 cal/g = 122 calories = 30.5%
            Carbohydrates = 50m,
            Fat = 10m
        };

        // Act
        var result = nutrition.IsHighProtein();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsHighProtein_ZeroCalories_ReturnsFalse()
    {
        // Arrange
        var nutrition = new Nutrition
        {
            NutritionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Calories = 0,
            Protein = 25m,
            Carbohydrates = 0m,
            Fat = 0m
        };

        // Act
        var result = nutrition.IsHighProtein();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void Fiber_CanBeSetToDecimalValue()
    {
        // Arrange
        var nutrition = new Nutrition
        {
            NutritionId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        // Act
        nutrition.Fiber = 8.5m;

        // Assert
        Assert.That(nutrition.Fiber, Is.EqualTo(8.5m));
    }

    [Test]
    public void Sugar_CanBeSetToDecimalValue()
    {
        // Arrange
        var nutrition = new Nutrition
        {
            NutritionId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        // Act
        nutrition.Sugar = 12.3m;

        // Assert
        Assert.That(nutrition.Sugar, Is.EqualTo(12.3m));
    }

    [Test]
    public void Sodium_CanBeSetToDecimalValue()
    {
        // Arrange
        var nutrition = new Nutrition
        {
            NutritionId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        // Act
        nutrition.Sodium = 500.0m;

        // Assert
        Assert.That(nutrition.Sodium, Is.EqualTo(500.0m));
    }

    [Test]
    public void AdditionalNutrients_CanStoreJsonData()
    {
        // Arrange
        var nutrition = new Nutrition
        {
            NutritionId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        var jsonData = "{\"Iron\": 10, \"Calcium\": 200}";

        // Act
        nutrition.AdditionalNutrients = jsonData;

        // Assert
        Assert.That(nutrition.AdditionalNutrients, Is.EqualTo(jsonData));
    }
}
