// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GroceryShoppingListApp.Core.Tests;

public class StoreTests
{
    [Test]
    public void Store_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var storeId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Walmart";
        var address = "123 Main St";
        var createdAt = DateTime.UtcNow;

        // Act
        var store = new Store
        {
            StoreId = storeId,
            UserId = userId,
            Name = name,
            Address = address,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(store.StoreId, Is.EqualTo(storeId));
            Assert.That(store.UserId, Is.EqualTo(userId));
            Assert.That(store.Name, Is.EqualTo(name));
            Assert.That(store.Address, Is.EqualTo(address));
            Assert.That(store.CreatedAt, Is.EqualTo(createdAt));
        });
    }

    [Test]
    public void Store_DefaultName_IsEmptyString()
    {
        // Arrange & Act
        var store = new Store();

        // Assert
        Assert.That(store.Name, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Store_CreatedAt_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var store = new Store();
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(store.CreatedAt, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void Store_Address_CanBeNull()
    {
        // Arrange & Act
        var store = new Store
        {
            StoreId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Target",
            Address = null
        };

        // Assert
        Assert.That(store.Address, Is.Null);
    }

    [Test]
    public void Store_AllProperties_CanBeModified()
    {
        // Arrange
        var store = new Store
        {
            StoreId = Guid.NewGuid(),
            Name = "Initial Name"
        };

        var newStoreId = Guid.NewGuid();
        var newUserId = Guid.NewGuid();

        // Act
        store.StoreId = newStoreId;
        store.UserId = newUserId;
        store.Name = "Updated Name";
        store.Address = "456 Oak Ave";

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(store.StoreId, Is.EqualTo(newStoreId));
            Assert.That(store.UserId, Is.EqualTo(newUserId));
            Assert.That(store.Name, Is.EqualTo("Updated Name"));
            Assert.That(store.Address, Is.EqualTo("456 Oak Ave"));
        });
    }

    [Test]
    public void Store_CanHaveDifferentNames()
    {
        // Arrange & Act
        var store1 = new Store { Name = "Kroger" };
        var store2 = new Store { Name = "Whole Foods" };
        var store3 = new Store { Name = "Costco" };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(store1.Name, Is.EqualTo("Kroger"));
            Assert.That(store2.Name, Is.EqualTo("Whole Foods"));
            Assert.That(store3.Name, Is.EqualTo("Costco"));
        });
    }

    [Test]
    public void Store_Address_CanBeSet()
    {
        // Arrange
        var store = new Store();

        // Act
        store.Address = "789 Pine Street, City, State 12345";

        // Assert
        Assert.That(store.Address, Is.EqualTo("789 Pine Street, City, State 12345"));
    }

    [Test]
    public void Store_CanBeIdentifiedByUserId()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        var store = new Store
        {
            UserId = userId,
            Name = "My Store"
        };

        // Assert
        Assert.That(store.UserId, Is.EqualTo(userId));
    }
}
