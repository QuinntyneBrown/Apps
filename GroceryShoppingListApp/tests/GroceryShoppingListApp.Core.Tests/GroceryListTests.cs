// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GroceryShoppingListApp.Core.Tests;

public class GroceryListTests
{
    [Test]
    public void GroceryList_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var groceryListId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Weekly Shopping";
        var createdDate = DateTime.UtcNow.AddDays(-1);
        var isCompleted = false;
        var createdAt = DateTime.UtcNow;

        // Act
        var groceryList = new GroceryList
        {
            GroceryListId = groceryListId,
            UserId = userId,
            Name = name,
            CreatedDate = createdDate,
            IsCompleted = isCompleted,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(groceryList.GroceryListId, Is.EqualTo(groceryListId));
            Assert.That(groceryList.UserId, Is.EqualTo(userId));
            Assert.That(groceryList.Name, Is.EqualTo(name));
            Assert.That(groceryList.CreatedDate, Is.EqualTo(createdDate));
            Assert.That(groceryList.IsCompleted, Is.False);
            Assert.That(groceryList.CreatedAt, Is.EqualTo(createdAt));
        });
    }

    [Test]
    public void GroceryList_DefaultName_IsEmptyString()
    {
        // Arrange & Act
        var groceryList = new GroceryList();

        // Assert
        Assert.That(groceryList.Name, Is.EqualTo(string.Empty));
    }

    [Test]
    public void GroceryList_CreatedAt_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var groceryList = new GroceryList();
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(groceryList.CreatedAt, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void GroceryList_Items_DefaultsToEmptyList()
    {
        // Arrange & Act
        var groceryList = new GroceryList();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(groceryList.Items, Is.Not.Null);
            Assert.That(groceryList.Items, Is.Empty);
        });
    }

    [Test]
    public void GroceryList_IsCompleted_CanBeSetToTrue()
    {
        // Arrange
        var groceryList = new GroceryList
        {
            Name = "Shopping List"
        };

        // Act
        groceryList.IsCompleted = true;

        // Assert
        Assert.That(groceryList.IsCompleted, Is.True);
    }

    [Test]
    public void GroceryList_CanHaveMultipleItems()
    {
        // Arrange
        var groceryList = new GroceryList();
        var item1 = new GroceryItem { Name = "Apples" };
        var item2 = new GroceryItem { Name = "Bananas" };

        // Act
        groceryList.Items.Add(item1);
        groceryList.Items.Add(item2);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(groceryList.Items, Has.Count.EqualTo(2));
            Assert.That(groceryList.Items, Contains.Item(item1));
            Assert.That(groceryList.Items, Contains.Item(item2));
        });
    }

    [Test]
    public void GroceryList_AllProperties_CanBeModified()
    {
        // Arrange
        var groceryList = new GroceryList
        {
            GroceryListId = Guid.NewGuid(),
            Name = "Initial Name"
        };

        var newGroceryListId = Guid.NewGuid();
        var newUserId = Guid.NewGuid();
        var newCreatedDate = DateTime.UtcNow;

        // Act
        groceryList.GroceryListId = newGroceryListId;
        groceryList.UserId = newUserId;
        groceryList.Name = "Updated Name";
        groceryList.CreatedDate = newCreatedDate;
        groceryList.IsCompleted = true;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(groceryList.GroceryListId, Is.EqualTo(newGroceryListId));
            Assert.That(groceryList.UserId, Is.EqualTo(newUserId));
            Assert.That(groceryList.Name, Is.EqualTo("Updated Name"));
            Assert.That(groceryList.CreatedDate, Is.EqualTo(newCreatedDate));
            Assert.That(groceryList.IsCompleted, Is.True);
        });
    }

    [Test]
    public void GroceryList_Items_CanBeReplaced()
    {
        // Arrange
        var groceryList = new GroceryList();
        var newItems = new List<GroceryItem>
        {
            new GroceryItem { Name = "Item 1" },
            new GroceryItem { Name = "Item 2" }
        };

        // Act
        groceryList.Items = newItems;

        // Assert
        Assert.That(groceryList.Items, Is.EqualTo(newItems));
    }

    [Test]
    public void GroceryList_CanBeNamed()
    {
        // Arrange & Act
        var weeklyList = new GroceryList { Name = "Weekly" };
        var monthlyList = new GroceryList { Name = "Monthly" };
        var partyList = new GroceryList { Name = "Party Shopping" };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(weeklyList.Name, Is.EqualTo("Weekly"));
            Assert.That(monthlyList.Name, Is.EqualTo("Monthly"));
            Assert.That(partyList.Name, Is.EqualTo("Party Shopping"));
        });
    }
}
