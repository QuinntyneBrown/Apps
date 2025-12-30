// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RecipeManagerMealPlanner.Core.Tests;

public class IngredientAddedEventTests
{
    [Test]
    public void Constructor_WithValidParameters_CreatesEvent()
    {
        var ingredientId = Guid.NewGuid();
        var recipeId = Guid.NewGuid();
        var name = "Flour";

        var eventData = new IngredientAddedEvent
        {
            IngredientId = ingredientId,
            RecipeId = recipeId,
            Name = name
        };

        Assert.Multiple(() =>
        {
            Assert.That(eventData.IngredientId, Is.EqualTo(ingredientId));
            Assert.That(eventData.RecipeId, Is.EqualTo(recipeId));
            Assert.That(eventData.Name, Is.EqualTo(name));
            Assert.That(eventData.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        var ingredientId = Guid.NewGuid();
        var recipeId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new IngredientAddedEvent { IngredientId = ingredientId, RecipeId = recipeId, Name = "Sugar", Timestamp = timestamp };
        var event2 = new IngredientAddedEvent { IngredientId = ingredientId, RecipeId = recipeId, Name = "Sugar", Timestamp = timestamp };

        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Record_Inequality_WorksCorrectly()
    {
        var event1 = new IngredientAddedEvent { IngredientId = Guid.NewGuid(), RecipeId = Guid.NewGuid(), Name = "Salt" };
        var event2 = new IngredientAddedEvent { IngredientId = Guid.NewGuid(), RecipeId = Guid.NewGuid(), Name = "Pepper" };

        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
