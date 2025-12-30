// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RecipeManagerMealPlanner.Core.Tests;

public class RecipeTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesRecipe()
    {
        // Arrange & Act
        var recipe = new Recipe();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(recipe.RecipeId, Is.EqualTo(Guid.Empty));
            Assert.That(recipe.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(recipe.Name, Is.EqualTo(string.Empty));
            Assert.That(recipe.Description, Is.Null);
            Assert.That(recipe.Cuisine, Is.EqualTo(Cuisine.American));
            Assert.That(recipe.DifficultyLevel, Is.EqualTo(DifficultyLevel.Easy));
            Assert.That(recipe.PrepTimeMinutes, Is.EqualTo(0));
            Assert.That(recipe.CookTimeMinutes, Is.EqualTo(0));
            Assert.That(recipe.Servings, Is.EqualTo(0));
            Assert.That(recipe.Instructions, Is.EqualTo(string.Empty));
            Assert.That(recipe.PhotoUrl, Is.Null);
            Assert.That(recipe.Source, Is.Null);
            Assert.That(recipe.Rating, Is.Null);
            Assert.That(recipe.Notes, Is.Null);
            Assert.That(recipe.IsFavorite, Is.False);
            Assert.That(recipe.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            Assert.That(recipe.Ingredients, Is.Not.Null);
            Assert.That(recipe.Ingredients, Is.Empty);
            Assert.That(recipe.MealPlans, Is.Not.Null);
            Assert.That(recipe.MealPlans, Is.Empty);
        });
    }

    [Test]
    public void Constructor_WithValidParameters_CreatesRecipe()
    {
        // Arrange
        var recipeId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Spaghetti Carbonara";
        var description = "Classic Italian pasta dish";
        var instructions = "Cook pasta, prepare sauce, combine";

        // Act
        var recipe = new Recipe
        {
            RecipeId = recipeId,
            UserId = userId,
            Name = name,
            Description = description,
            Cuisine = Cuisine.Italian,
            DifficultyLevel = DifficultyLevel.Medium,
            PrepTimeMinutes = 15,
            CookTimeMinutes = 20,
            Servings = 4,
            Instructions = instructions
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(recipe.RecipeId, Is.EqualTo(recipeId));
            Assert.That(recipe.UserId, Is.EqualTo(userId));
            Assert.That(recipe.Name, Is.EqualTo(name));
            Assert.That(recipe.Description, Is.EqualTo(description));
            Assert.That(recipe.Cuisine, Is.EqualTo(Cuisine.Italian));
            Assert.That(recipe.DifficultyLevel, Is.EqualTo(DifficultyLevel.Medium));
            Assert.That(recipe.PrepTimeMinutes, Is.EqualTo(15));
            Assert.That(recipe.CookTimeMinutes, Is.EqualTo(20));
            Assert.That(recipe.Servings, Is.EqualTo(4));
            Assert.That(recipe.Instructions, Is.EqualTo(instructions));
        });
    }

    [Test]
    public void GetTotalTime_ValidTimes_ReturnsSum()
    {
        // Arrange
        var recipe = new Recipe
        {
            PrepTimeMinutes = 15,
            CookTimeMinutes = 30
        };

        // Act
        var totalTime = recipe.GetTotalTime();

        // Assert
        Assert.That(totalTime, Is.EqualTo(45));
    }

    [Test]
    public void GetTotalTime_ZeroTimes_ReturnsZero()
    {
        // Arrange
        var recipe = new Recipe
        {
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
            PrepTimeMinutes = 20,
            CookTimeMinutes = 0
        };

        // Act
        var totalTime = recipe.GetTotalTime();

        // Assert
        Assert.That(totalTime, Is.EqualTo(20));
    }

    [Test]
    public void Recipe_WithRating_SetsCorrectly()
    {
        // Arrange & Act
        var recipe = new Recipe
        {
            Name = "Test Recipe",
            Rating = 5
        };

        // Assert
        Assert.That(recipe.Rating, Is.EqualTo(5));
    }

    [Test]
    public void Recipe_IsFavorite_SetsCorrectly()
    {
        // Arrange & Act
        var recipe = new Recipe
        {
            Name = "Favorite Recipe",
            IsFavorite = true
        };

        // Assert
        Assert.That(recipe.IsFavorite, Is.True);
    }

    [Test]
    public void Recipe_WithPhotoUrl_SetsCorrectly()
    {
        // Arrange
        var photoUrl = "https://example.com/recipe.jpg";

        // Act
        var recipe = new Recipe
        {
            Name = "Visual Recipe",
            PhotoUrl = photoUrl
        };

        // Assert
        Assert.That(recipe.PhotoUrl, Is.EqualTo(photoUrl));
    }

    [Test]
    public void Recipe_WithSource_SetsCorrectly()
    {
        // Arrange
        var source = "Grandma's cookbook";

        // Act
        var recipe = new Recipe
        {
            Name = "Family Recipe",
            Source = source
        };

        // Assert
        Assert.That(recipe.Source, Is.EqualTo(source));
    }

    [Test]
    public void Recipe_WithNotes_SetsCorrectly()
    {
        // Arrange
        var notes = "Add extra cheese for best results";

        // Act
        var recipe = new Recipe
        {
            Name = "Cheesy Dish",
            Notes = notes
        };

        // Assert
        Assert.That(recipe.Notes, Is.EqualTo(notes));
    }

    [Test]
    public void Recipe_DifferentDifficultyLevels_SetCorrectly()
    {
        // Arrange & Act
        var easy = new Recipe { Name = "Easy Recipe", DifficultyLevel = DifficultyLevel.Easy };
        var medium = new Recipe { Name = "Medium Recipe", DifficultyLevel = DifficultyLevel.Medium };
        var hard = new Recipe { Name = "Hard Recipe", DifficultyLevel = DifficultyLevel.Hard };
        var expert = new Recipe { Name = "Expert Recipe", DifficultyLevel = DifficultyLevel.Expert };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(easy.DifficultyLevel, Is.EqualTo(DifficultyLevel.Easy));
            Assert.That(medium.DifficultyLevel, Is.EqualTo(DifficultyLevel.Medium));
            Assert.That(hard.DifficultyLevel, Is.EqualTo(DifficultyLevel.Hard));
            Assert.That(expert.DifficultyLevel, Is.EqualTo(DifficultyLevel.Expert));
        });
    }

    [Test]
    public void Recipe_DifferentCuisines_SetCorrectly()
    {
        // Arrange & Act
        var italian = new Recipe { Name = "Pasta", Cuisine = Cuisine.Italian };
        var mexican = new Recipe { Name = "Tacos", Cuisine = Cuisine.Mexican };
        var chinese = new Recipe { Name = "Fried Rice", Cuisine = Cuisine.Chinese };
        var japanese = new Recipe { Name = "Sushi", Cuisine = Cuisine.Japanese };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(italian.Cuisine, Is.EqualTo(Cuisine.Italian));
            Assert.That(mexican.Cuisine, Is.EqualTo(Cuisine.Mexican));
            Assert.That(chinese.Cuisine, Is.EqualTo(Cuisine.Chinese));
            Assert.That(japanese.Cuisine, Is.EqualTo(Cuisine.Japanese));
        });
    }

    [Test]
    public void GetTotalTime_LongCookingTime_ReturnsCorrectTotal()
    {
        // Arrange
        var recipe = new Recipe
        {
            PrepTimeMinutes = 30,
            CookTimeMinutes = 240 // 4 hours
        };

        // Act
        var totalTime = recipe.GetTotalTime();

        // Assert
        Assert.That(totalTime, Is.EqualTo(270));
    }

    [Test]
    public void Recipe_HighServings_StoresCorrectly()
    {
        // Arrange & Act
        var recipe = new Recipe
        {
            Name = "Party Recipe",
            Servings = 12
        };

        // Assert
        Assert.That(recipe.Servings, Is.EqualTo(12));
    }

    [Test]
    public void Recipe_WithAllOptionalProperties_SetsAllCorrectly()
    {
        // Arrange & Act
        var recipe = new Recipe
        {
            RecipeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Complete Recipe",
            Description = "Fully detailed recipe",
            Cuisine = Cuisine.Mediterranean,
            DifficultyLevel = DifficultyLevel.Hard,
            PrepTimeMinutes = 25,
            CookTimeMinutes = 45,
            Servings = 6,
            Instructions = "Detailed step-by-step instructions",
            PhotoUrl = "https://example.com/photo.jpg",
            Source = "Cookbook",
            Rating = 4,
            Notes = "Great for special occasions",
            IsFavorite = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(recipe.Name, Is.EqualTo("Complete Recipe"));
            Assert.That(recipe.Description, Is.EqualTo("Fully detailed recipe"));
            Assert.That(recipe.Cuisine, Is.EqualTo(Cuisine.Mediterranean));
            Assert.That(recipe.DifficultyLevel, Is.EqualTo(DifficultyLevel.Hard));
            Assert.That(recipe.PrepTimeMinutes, Is.EqualTo(25));
            Assert.That(recipe.CookTimeMinutes, Is.EqualTo(45));
            Assert.That(recipe.Servings, Is.EqualTo(6));
            Assert.That(recipe.PhotoUrl, Is.EqualTo("https://example.com/photo.jpg"));
            Assert.That(recipe.Source, Is.EqualTo("Cookbook"));
            Assert.That(recipe.Rating, Is.EqualTo(4));
            Assert.That(recipe.Notes, Is.EqualTo("Great for special occasions"));
            Assert.That(recipe.IsFavorite, Is.True);
        });
    }
}
