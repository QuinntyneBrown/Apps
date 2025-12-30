// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GroceryShoppingListApp.Core.Tests;

public class EventTests
{
    [Test]
    public void PriceRecordedEvent_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var priceHistoryId = Guid.NewGuid();
        var price = 5.99m;
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new PriceRecordedEvent
        {
            PriceHistoryId = priceHistoryId,
            Price = price,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.PriceHistoryId, Is.EqualTo(priceHistoryId));
            Assert.That(evt.Price, Is.EqualTo(price));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void PriceRecordedEvent_Timestamp_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var evt = new PriceRecordedEvent
        {
            PriceHistoryId = Guid.NewGuid(),
            Price = 3.99m
        };
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(evt.Timestamp, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void ItemAddedEvent_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var groceryItemId = Guid.NewGuid();
        var name = "Apples";
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new ItemAddedEvent
        {
            GroceryItemId = groceryItemId,
            Name = name,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.GroceryItemId, Is.EqualTo(groceryItemId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void ItemAddedEvent_Timestamp_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var evt = new ItemAddedEvent
        {
            GroceryItemId = Guid.NewGuid(),
            Name = "Milk"
        };
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(evt.Timestamp, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void ListCreatedEvent_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var groceryListId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new ListCreatedEvent
        {
            GroceryListId = groceryListId,
            UserId = userId,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.GroceryListId, Is.EqualTo(groceryListId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void ListCreatedEvent_Timestamp_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var evt = new ListCreatedEvent
        {
            GroceryListId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(evt.Timestamp, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void StoreAddedEvent_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var storeId = Guid.NewGuid();
        var name = "Safeway";
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new StoreAddedEvent
        {
            StoreId = storeId,
            Name = name,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.StoreId, Is.EqualTo(storeId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void StoreAddedEvent_Timestamp_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var evt = new StoreAddedEvent
        {
            StoreId = Guid.NewGuid(),
            Name = "Kroger"
        };
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(evt.Timestamp, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void Events_AreRecords_AndSupportValueEquality()
    {
        // Arrange
        var id = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new PriceRecordedEvent
        {
            PriceHistoryId = id,
            Price = 4.99m,
            Timestamp = timestamp
        };

        var event2 = new PriceRecordedEvent
        {
            PriceHistoryId = id,
            Price = 4.99m,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }
}
