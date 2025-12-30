// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RecipeManagerMealPlanner.Core.Tests;

public class RecipeCreatedEventTests
{
    [Test]
    public void Constructor_WithValidParameters_CreatesEvent()
    {
        var recipeId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Test Recipe";
        var cuisine = Cuisine.Italian;

        var eventData = new RecipeCreatedEvent
        {
            RecipeId = recipeId,
            UserId = userId,
            Name = name,
            Cuisine = cuisine
        };

        Assert.Multiple(() =>
        {
            Assert.That(eventData.RecipeId, Is.EqualTo(recipeId));
            Assert.That(eventData.UserId, Is.EqualTo(userId));
            Assert.That(eventData.Name, Is.EqualTo(name));
            Assert.That(eventData.Cuisine, Is.EqualTo(cuisine));
            Assert.That(eventData.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        var recipeId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new RecipeCreatedEvent { RecipeId = recipeId, UserId = userId, Name = "Test", Cuisine = Cuisine.Italian, Timestamp = timestamp };
        var event2 = new RecipeCreatedEvent { RecipeId = recipeId, UserId = userId, Name = "Test", Cuisine = Cuisine.Italian, Timestamp = timestamp };

        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Record_Inequality_WorksCorrectly()
    {
        var event1 = new RecipeCreatedEvent { RecipeId = Guid.NewGuid(), UserId = Guid.NewGuid(), Name = "Recipe 1", Cuisine = Cuisine.Italian };
        var event2 = new RecipeCreatedEvent { RecipeId = Guid.NewGuid(), UserId = Guid.NewGuid(), Name = "Recipe 2", Cuisine = Cuisine.Mexican };

        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
