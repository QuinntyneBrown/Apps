// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GroceryShoppingListApp.Core.Tests;

public class GroceryItemTests
{
    [Test]
    public void GroceryItem_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var groceryItemId = Guid.NewGuid();
        var groceryListId = Guid.NewGuid();
        var name = "Milk";
        var category = Category.Dairy;
        var quantity = 2;
        var isChecked = false;
        var createdAt = DateTime.UtcNow;

        // Act
        var groceryItem = new GroceryItem
        {
            GroceryItemId = groceryItemId,
            GroceryListId = groceryListId,
            Name = name,
            Category = category,
            Quantity = quantity,
            IsChecked = isChecked,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(groceryItem.GroceryItemId, Is.EqualTo(groceryItemId));
            Assert.That(groceryItem.GroceryListId, Is.EqualTo(groceryListId));
            Assert.That(groceryItem.Name, Is.EqualTo(name));
            Assert.That(groceryItem.Category, Is.EqualTo(category));
            Assert.That(groceryItem.Quantity, Is.EqualTo(quantity));
            Assert.That(groceryItem.IsChecked, Is.False);
            Assert.That(groceryItem.CreatedAt, Is.EqualTo(createdAt));
        });
    }

    [Test]
    public void GroceryItem_DefaultName_IsEmptyString()
    {
        // Arrange & Act
        var groceryItem = new GroceryItem();

        // Assert
        Assert.That(groceryItem.Name, Is.EqualTo(string.Empty));
    }

    [Test]
    public void GroceryItem_CreatedAt_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var groceryItem = new GroceryItem();
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(groceryItem.CreatedAt, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void GroceryItem_GroceryListId_CanBeNull()
    {
        // Arrange & Act
        var groceryItem = new GroceryItem
        {
            GroceryListId = null
        };

        // Assert
        Assert.That(groceryItem.GroceryListId, Is.Null);
    }

    [Test]
    public void GroceryItem_IsChecked_CanBeSetToTrue()
    {
        // Arrange
        var groceryItem = new GroceryItem
        {
            Name = "Bread"
        };

        // Act
        groceryItem.IsChecked = true;

        // Assert
        Assert.That(groceryItem.IsChecked, Is.True);
    }

    [Test]
    public void GroceryItem_Quantity_CanBePositive()
    {
        // Arrange & Act
        var groceryItem = new GroceryItem
        {
            Quantity = 5
        };

        // Assert
        Assert.That(groceryItem.Quantity, Is.Positive);
    }

    [Test]
    public void GroceryItem_Quantity_CanBeZero()
    {
        // Arrange & Act
        var groceryItem = new GroceryItem
        {
            Quantity = 0
        };

        // Assert
        Assert.That(groceryItem.Quantity, Is.EqualTo(0));
    }

    [Test]
    public void GroceryItem_CanHaveDifferentCategories()
    {
        // Arrange & Act
        var produceItem = new GroceryItem { Category = Category.Produce };
        var dairyItem = new GroceryItem { Category = Category.Dairy };
        var meatItem = new GroceryItem { Category = Category.Meat };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(produceItem.Category, Is.EqualTo(Category.Produce));
            Assert.That(dairyItem.Category, Is.EqualTo(Category.Dairy));
            Assert.That(meatItem.Category, Is.EqualTo(Category.Meat));
        });
    }

    [Test]
    public void GroceryItem_GroceryList_CanBeSet()
    {
        // Arrange
        var groceryList = new GroceryList
        {
            GroceryListId = Guid.NewGuid(),
            Name = "Weekly Shopping"
        };

        var groceryItem = new GroceryItem();

        // Act
        groceryItem.GroceryList = groceryList;

        // Assert
        Assert.That(groceryItem.GroceryList, Is.EqualTo(groceryList));
    }

    [Test]
    public void GroceryItem_AllProperties_CanBeModified()
    {
        // Arrange
        var groceryItem = new GroceryItem
        {
            GroceryItemId = Guid.NewGuid(),
            Name = "Initial Name"
        };

        var newGroceryItemId = Guid.NewGuid();
        var newGroceryListId = Guid.NewGuid();

        // Act
        groceryItem.GroceryItemId = newGroceryItemId;
        groceryItem.GroceryListId = newGroceryListId;
        groceryItem.Name = "Updated Name";
        groceryItem.Category = Category.Frozen;
        groceryItem.Quantity = 3;
        groceryItem.IsChecked = true;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(groceryItem.GroceryItemId, Is.EqualTo(newGroceryItemId));
            Assert.That(groceryItem.GroceryListId, Is.EqualTo(newGroceryListId));
            Assert.That(groceryItem.Name, Is.EqualTo("Updated Name"));
            Assert.That(groceryItem.Category, Is.EqualTo(Category.Frozen));
            Assert.That(groceryItem.Quantity, Is.EqualTo(3));
            Assert.That(groceryItem.IsChecked, Is.True);
        });
    }
}
