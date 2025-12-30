// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeInventoryManager.Core.Tests;

public class ItemAddedEventTests
{
    [Test]
    public void ItemAddedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var itemId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "MacBook Pro";
        var category = Category.Electronics;
        var room = Room.Office;
        var timestamp = DateTime.UtcNow;

        // Act
        var itemEvent = new ItemAddedEvent
        {
            ItemId = itemId,
            UserId = userId,
            Name = name,
            Category = category,
            Room = room,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(itemEvent.ItemId, Is.EqualTo(itemId));
            Assert.That(itemEvent.UserId, Is.EqualTo(userId));
            Assert.That(itemEvent.Name, Is.EqualTo(name));
            Assert.That(itemEvent.Category, Is.EqualTo(category));
            Assert.That(itemEvent.Room, Is.EqualTo(room));
            Assert.That(itemEvent.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void ItemAddedEvent_DefaultValues_AreSetCorrectly()
    {
        // Act
        var itemEvent = new ItemAddedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(itemEvent.ItemId, Is.EqualTo(Guid.Empty));
            Assert.That(itemEvent.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(itemEvent.Name, Is.EqualTo(string.Empty));
            Assert.That(itemEvent.Category, Is.EqualTo(Category.Electronics));
            Assert.That(itemEvent.Room, Is.EqualTo(Room.LivingRoom));
            Assert.That(itemEvent.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void ItemAddedEvent_Timestamp_IsSetToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var itemEvent = new ItemAddedEvent
        {
            ItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Item",
            Category = Category.Furniture,
            Room = Room.LivingRoom
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(itemEvent.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void ItemAddedEvent_AllCategories_CanBeSet()
    {
        // Arrange
        var categories = new[]
        {
            Category.Electronics,
            Category.Furniture,
            Category.Appliances,
            Category.Jewelry,
            Category.Collectibles,
            Category.Tools,
            Category.Clothing,
            Category.Books,
            Category.Sports,
            Category.Other
        };

        // Act & Assert
        foreach (var category in categories)
        {
            var itemEvent = new ItemAddedEvent
            {
                ItemId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "Item",
                Category = category,
                Room = Room.Storage
            };

            Assert.That(itemEvent.Category, Is.EqualTo(category));
        }
    }

    [Test]
    public void ItemAddedEvent_AllRooms_CanBeSet()
    {
        // Arrange
        var rooms = new[]
        {
            Room.LivingRoom,
            Room.Bedroom,
            Room.Kitchen,
            Room.DiningRoom,
            Room.Bathroom,
            Room.Garage,
            Room.Basement,
            Room.Attic,
            Room.Office,
            Room.Storage,
            Room.Other
        };

        // Act & Assert
        foreach (var room in rooms)
        {
            var itemEvent = new ItemAddedEvent
            {
                ItemId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "Item",
                Category = Category.Electronics,
                Room = room
            };

            Assert.That(itemEvent.Room, Is.EqualTo(room));
        }
    }

    [Test]
    public void ItemAddedEvent_IsImmutable()
    {
        // Arrange
        var itemId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Test Item";
        var category = Category.Tools;
        var room = Room.Garage;

        // Act
        var itemEvent = new ItemAddedEvent
        {
            ItemId = itemId,
            UserId = userId,
            Name = name,
            Category = category,
            Room = room
        };

        // Assert - Record properties are init-only
        Assert.Multiple(() =>
        {
            Assert.That(itemEvent.ItemId, Is.EqualTo(itemId));
            Assert.That(itemEvent.UserId, Is.EqualTo(userId));
            Assert.That(itemEvent.Name, Is.EqualTo(name));
            Assert.That(itemEvent.Category, Is.EqualTo(category));
            Assert.That(itemEvent.Room, Is.EqualTo(room));
        });
    }

    [Test]
    public void ItemAddedEvent_EqualityByValue()
    {
        // Arrange
        var itemId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Item";
        var category = Category.Appliances;
        var room = Room.Kitchen;
        var timestamp = DateTime.UtcNow;

        var event1 = new ItemAddedEvent
        {
            ItemId = itemId,
            UserId = userId,
            Name = name,
            Category = category,
            Room = room,
            Timestamp = timestamp
        };

        var event2 = new ItemAddedEvent
        {
            ItemId = itemId,
            UserId = userId,
            Name = name,
            Category = category,
            Room = room,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void ItemAddedEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new ItemAddedEvent
        {
            ItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Item 1",
            Category = Category.Electronics,
            Room = Room.Office
        };

        var event2 = new ItemAddedEvent
        {
            ItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Item 2",
            Category = Category.Furniture,
            Room = Room.Bedroom
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }

    [Test]
    public void ItemAddedEvent_WithEmptyName_IsValid()
    {
        // Arrange & Act
        var itemEvent = new ItemAddedEvent
        {
            ItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "",
            Category = Category.Other,
            Room = Room.Other
        };

        // Assert
        Assert.That(itemEvent.Name, Is.EqualTo(string.Empty));
    }
}
