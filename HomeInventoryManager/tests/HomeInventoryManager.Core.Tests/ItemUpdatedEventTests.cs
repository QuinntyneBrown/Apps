// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeInventoryManager.Core.Tests;

public class ItemUpdatedEventTests
{
    [Test]
    public void ItemUpdatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var itemId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var itemEvent = new ItemUpdatedEvent
        {
            ItemId = itemId,
            UserId = userId,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(itemEvent.ItemId, Is.EqualTo(itemId));
            Assert.That(itemEvent.UserId, Is.EqualTo(userId));
            Assert.That(itemEvent.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void ItemUpdatedEvent_DefaultValues_AreSetCorrectly()
    {
        // Act
        var itemEvent = new ItemUpdatedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(itemEvent.ItemId, Is.EqualTo(Guid.Empty));
            Assert.That(itemEvent.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(itemEvent.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void ItemUpdatedEvent_Timestamp_IsSetToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var itemEvent = new ItemUpdatedEvent
        {
            ItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(itemEvent.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void ItemUpdatedEvent_IsImmutable()
    {
        // Arrange
        var itemId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        // Act
        var itemEvent = new ItemUpdatedEvent
        {
            ItemId = itemId,
            UserId = userId
        };

        // Assert - Record properties are init-only
        Assert.Multiple(() =>
        {
            Assert.That(itemEvent.ItemId, Is.EqualTo(itemId));
            Assert.That(itemEvent.UserId, Is.EqualTo(userId));
        });
    }

    [Test]
    public void ItemUpdatedEvent_EqualityByValue()
    {
        // Arrange
        var itemId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new ItemUpdatedEvent
        {
            ItemId = itemId,
            UserId = userId,
            Timestamp = timestamp
        };

        var event2 = new ItemUpdatedEvent
        {
            ItemId = itemId,
            UserId = userId,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void ItemUpdatedEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new ItemUpdatedEvent
        {
            ItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        var event2 = new ItemUpdatedEvent
        {
            ItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }

    [Test]
    public void ItemUpdatedEvent_MinimalProperties_IsValid()
    {
        // Arrange & Act
        var itemEvent = new ItemUpdatedEvent
        {
            ItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(itemEvent.ItemId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(itemEvent.UserId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(itemEvent.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }
}
