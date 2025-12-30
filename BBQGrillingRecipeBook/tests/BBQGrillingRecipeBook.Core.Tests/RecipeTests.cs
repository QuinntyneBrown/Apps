// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BBQGrillingRecipeBook.Core.Tests;

public class RecipeTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesRecipe()
    {
        // Arrange
        var recipeId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Texas Brisket";
        var description = "Slow smoked beef brisket";
        var meatType = MeatType.Beef;
        var cookingMethod = CookingMethod.Smoking;
        var prepTime = 30;
        var cookTime = 720;
        var ingredients = "Brisket, salt, pepper";
        var instructions = "Season and smoke at 225F";
        var targetTemp = 203;
        var servings = 8;
        var notes = "Use oak wood";

        // Act
        var recipe = new Recipe
        {
            RecipeId = recipeId,
            UserId = userId,
            Name = name,
            Description = description,
            MeatType = meatType,
            CookingMethod = cookingMethod,
            PrepTimeMinutes = prepTime,
            CookTimeMinutes = cookTime,
            Ingredients = ingredients,
            Instructions = instructions,
            TargetTemperature = targetTemp,
            Servings = servings,
            Notes = notes,
            IsFavorite = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(recipe.RecipeId, Is.EqualTo(recipeId));
            Assert.That(recipe.UserId, Is.EqualTo(userId));
            Assert.That(recipe.Name, Is.EqualTo(name));
            Assert.That(recipe.Description, Is.EqualTo(description));
            Assert.That(recipe.MeatType, Is.EqualTo(meatType));
            Assert.That(recipe.CookingMethod, Is.EqualTo(cookingMethod));
            Assert.That(recipe.PrepTimeMinutes, Is.EqualTo(prepTime));
            Assert.That(recipe.CookTimeMinutes, Is.EqualTo(cookTime));
            Assert.That(recipe.Ingredients, Is.EqualTo(ingredients));
            Assert.That(recipe.Instructions, Is.EqualTo(instructions));
            Assert.That(recipe.TargetTemperature, Is.EqualTo(targetTemp));
            Assert.That(recipe.Servings, Is.EqualTo(servings));
            Assert.That(recipe.Notes, Is.EqualTo(notes));
            Assert.That(recipe.IsFavorite, Is.True);
            Assert.That(recipe.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Constructor_DefaultValues_SetsCorrectDefaults()
    {
        // Act
        var recipe = new Recipe();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(recipe.Name, Is.EqualTo(string.Empty));
            Assert.That(recipe.Description, Is.EqualTo(string.Empty));
            Assert.That(recipe.Ingredients, Is.EqualTo(string.Empty));
            Assert.That(recipe.Instructions, Is.EqualTo(string.Empty));
            Assert.That(recipe.TargetTemperature, Is.Null);
            Assert.That(recipe.Servings, Is.EqualTo(4));
            Assert.That(recipe.Notes, Is.Null);
            Assert.That(recipe.IsFavorite, Is.False);
            Assert.That(recipe.CookSessions, Is.Not.Null);
            Assert.That(recipe.CookSessions, Is.Empty);
            Assert.That(recipe.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void GetTotalTimeMinutes_WithPrepAndCookTime_ReturnsSum()
    {
        // Arrange
        var recipe = new Recipe
        {
            PrepTimeMinutes = 30,
            CookTimeMinutes = 120
        };

        // Act
        var total = recipe.GetTotalTimeMinutes();

        // Assert
        Assert.That(total, Is.EqualTo(150));
    }

    [Test]
    public void GetTotalTimeMinutes_WithZeroTimes_ReturnsZero()
    {
        // Arrange
        var recipe = new Recipe
        {
            PrepTimeMinutes = 0,
            CookTimeMinutes = 0
        };

        // Act
        var total = recipe.GetTotalTimeMinutes();

        // Assert
        Assert.That(total, Is.EqualTo(0));
    }

    [Test]
    public void ToggleFavorite_WhenFalse_BecomesTrue()
    {
        // Arrange
        var recipe = new Recipe { IsFavorite = false };

        // Act
        recipe.ToggleFavorite();

        // Assert
        Assert.That(recipe.IsFavorite, Is.True);
    }

    [Test]
    public void ToggleFavorite_WhenTrue_BecomesFalse()
    {
        // Arrange
        var recipe = new Recipe { IsFavorite = true };

        // Act
        recipe.ToggleFavorite();

        // Assert
        Assert.That(recipe.IsFavorite, Is.False);
    }

    [Test]
    public void ToggleFavorite_CalledTwice_ReturnsToOriginalValue()
    {
        // Arrange
        var recipe = new Recipe { IsFavorite = false };

        // Act
        recipe.ToggleFavorite();
        recipe.ToggleFavorite();

        // Assert
        Assert.That(recipe.IsFavorite, Is.False);
    }

    [Test]
    public void MeatType_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var recipe = new Recipe();

        // Act
        recipe.MeatType = MeatType.Pork;

        // Assert
        Assert.That(recipe.MeatType, Is.EqualTo(MeatType.Pork));
    }

    [Test]
    public void CookingMethod_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var recipe = new Recipe();

        // Act
        recipe.CookingMethod = CookingMethod.DirectGrilling;

        // Assert
        Assert.That(recipe.CookingMethod, Is.EqualTo(CookingMethod.DirectGrilling));
    }

    [Test]
    public void CookSessions_CanAddSession_AddsCorrectly()
    {
        // Arrange
        var recipe = new Recipe();
        var session = new CookSession { CookSessionId = Guid.NewGuid() };

        // Act
        recipe.CookSessions.Add(session);

        // Assert
        Assert.That(recipe.CookSessions, Has.Count.EqualTo(1));
        Assert.That(recipe.CookSessions, Contains.Item(session));
    }

    [Test]
    public void TargetTemperature_CanBeSetAndCleared_UpdatesCorrectly()
    {
        // Arrange
        var recipe = new Recipe { TargetTemperature = 225 };

        // Act
        recipe.TargetTemperature = null;

        // Assert
        Assert.That(recipe.TargetTemperature, Is.Null);
    }

    [Test]
    public void Servings_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var recipe = new Recipe();

        // Act
        recipe.Servings = 12;

        // Assert
        Assert.That(recipe.Servings, Is.EqualTo(12));
    }
}
