// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RecipeManagerMealPlanner.Core.Tests;

public class ShoppingListTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesShoppingList()
    {
        var shoppingList = new ShoppingList();
        Assert.Multiple(() =>
        {
            Assert.That(shoppingList.ShoppingListId, Is.EqualTo(Guid.Empty));
            Assert.That(shoppingList.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(shoppingList.Name, Is.EqualTo(string.Empty));
            Assert.That(shoppingList.Items, Is.EqualTo(string.Empty));
            Assert.That(shoppingList.CreatedDate, Is.EqualTo(default(DateTime)));
            Assert.That(shoppingList.IsCompleted, Is.False);
            Assert.That(shoppingList.CompletedDate, Is.Null);
            Assert.That(shoppingList.Notes, Is.Null);
            Assert.That(shoppingList.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Constructor_WithValidParameters_CreatesShoppingList()
    {
        var shoppingListId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Weekly Groceries";
        var items = "Milk, Bread, Eggs, Cheese";
        var createdDate = new DateTime(2024, 6, 15);

        var shoppingList = new ShoppingList
        {
            ShoppingListId = shoppingListId,
            UserId = userId,
            Name = name,
            Items = items,
            CreatedDate = createdDate
        };

        Assert.Multiple(() =>
        {
            Assert.That(shoppingList.ShoppingListId, Is.EqualTo(shoppingListId));
            Assert.That(shoppingList.UserId, Is.EqualTo(userId));
            Assert.That(shoppingList.Name, Is.EqualTo(name));
            Assert.That(shoppingList.Items, Is.EqualTo(items));
            Assert.That(shoppingList.CreatedDate, Is.EqualTo(createdDate));
        });
    }

    [Test]
    public void Complete_PendingList_MarksAsCompleted()
    {
        var shoppingList = new ShoppingList { Name = "Test List", IsCompleted = false };
        var beforeComplete = DateTime.UtcNow;

        shoppingList.Complete();

        Assert.Multiple(() =>
        {
            Assert.That(shoppingList.IsCompleted, Is.True);
            Assert.That(shoppingList.CompletedDate, Is.Not.Null);
            Assert.That(shoppingList.CompletedDate.Value, Is.GreaterThanOrEqualTo(beforeComplete));
            Assert.That(shoppingList.CompletedDate.Value, Is.LessThanOrEqualTo(DateTime.UtcNow));
        });
    }

    [Test]
    public void Complete_AlreadyCompleted_UpdatesCompletedDate()
    {
        var shoppingList = new ShoppingList { Name = "Test List", IsCompleted = true, CompletedDate = DateTime.UtcNow.AddDays(-1) };
        var previousDate = shoppingList.CompletedDate;

        Thread.Sleep(10);
        shoppingList.Complete();

        Assert.Multiple(() =>
        {
            Assert.That(shoppingList.IsCompleted, Is.True);
            Assert.That(shoppingList.CompletedDate, Is.Not.EqualTo(previousDate));
            Assert.That(shoppingList.CompletedDate, Is.GreaterThan(previousDate));
        });
    }

    [Test]
    public void ShoppingList_WithNotes_SetsCorrectly()
    {
        var notes = "Buy organic produce";
        var shoppingList = new ShoppingList { Name = "Groceries", Notes = notes };
        Assert.That(shoppingList.Notes, Is.EqualTo(notes));
    }

    [Test]
    public void ShoppingList_JsonItems_StoresCorrectly()
    {
        var items = "{\"items\": [\"Milk\", \"Bread\", \"Eggs\"]}";
        var shoppingList = new ShoppingList { Name = "JSON List", Items = items };
        Assert.That(shoppingList.Items, Is.EqualTo(items));
    }

    [Test]
    public void ShoppingList_LongItemList_StoresCorrectly()
    {
        var items = string.Join(", ", Enumerable.Repeat("Item", 50));
        var shoppingList = new ShoppingList { Name = "Long List", Items = items };
        Assert.That(shoppingList.Items.Length, Is.GreaterThan(100));
    }

    [Test]
    public void ShoppingList_WithAllProperties_SetsAllCorrectly()
    {
        var shoppingList = new ShoppingList
        {
            ShoppingListId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Complete Shopping List",
            Items = "Vegetables, Fruits, Dairy",
            CreatedDate = new DateTime(2024, 6, 1),
            IsCompleted = false,
            Notes = "Shop at local market"
        };

        Assert.Multiple(() =>
        {
            Assert.That(shoppingList.Name, Is.EqualTo("Complete Shopping List"));
            Assert.That(shoppingList.Items, Is.EqualTo("Vegetables, Fruits, Dairy"));
            Assert.That(shoppingList.CreatedDate, Is.EqualTo(new DateTime(2024, 6, 1)));
            Assert.That(shoppingList.IsCompleted, Is.False);
            Assert.That(shoppingList.Notes, Is.EqualTo("Shop at local market"));
        });
    }
}
