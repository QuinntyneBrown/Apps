// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RecipeManagerMealPlanner.Core.Tests;

public class ShoppingListCreatedEventTests
{
    [Test]
    public void Constructor_WithValidParameters_CreatesEvent()
    {
        var shoppingListId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Weekly Groceries";

        var eventData = new ShoppingListCreatedEvent
        {
            ShoppingListId = shoppingListId,
            UserId = userId,
            Name = name
        };

        Assert.Multiple(() =>
        {
            Assert.That(eventData.ShoppingListId, Is.EqualTo(shoppingListId));
            Assert.That(eventData.UserId, Is.EqualTo(userId));
            Assert.That(eventData.Name, Is.EqualTo(name));
            Assert.That(eventData.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        var shoppingListId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new ShoppingListCreatedEvent { ShoppingListId = shoppingListId, UserId = userId, Name = "Groceries", Timestamp = timestamp };
        var event2 = new ShoppingListCreatedEvent { ShoppingListId = shoppingListId, UserId = userId, Name = "Groceries", Timestamp = timestamp };

        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Record_Inequality_WorksCorrectly()
    {
        var event1 = new ShoppingListCreatedEvent { ShoppingListId = Guid.NewGuid(), UserId = Guid.NewGuid(), Name = "List 1" };
        var event2 = new ShoppingListCreatedEvent { ShoppingListId = Guid.NewGuid(), UserId = Guid.NewGuid(), Name = "List 2" };

        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
