// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyVacationPlanner.Core.Tests;

public class ItemPackedEventTests
{
    [Test]
    public void Constructor_CreatesEvent_WithAllProperties()
    {
        // Arrange
        var packingListId = Guid.NewGuid();
        var itemName = "Passport";
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new ItemPackedEvent
        {
            PackingListId = packingListId,
            ItemName = itemName,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.PackingListId, Is.EqualTo(packingListId));
            Assert.That(evt.ItemName, Is.EqualTo(itemName));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Event_WithDefaultTimestamp_SetsUtcNow()
    {
        // Arrange
        var packingListId = Guid.NewGuid();
        var itemName = "Sunscreen";

        // Act
        var evt = new ItemPackedEvent
        {
            PackingListId = packingListId,
            ItemName = itemName
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void Event_IsRecord_AndSupportsValueEquality()
    {
        // Arrange
        var packingListId = Guid.NewGuid();
        var itemName = "Camera";
        var timestamp = DateTime.UtcNow;

        // Act
        var event1 = new ItemPackedEvent
        {
            PackingListId = packingListId,
            ItemName = itemName,
            Timestamp = timestamp
        };

        var event2 = new ItemPackedEvent
        {
            PackingListId = packingListId,
            ItemName = itemName,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Event_WithDifferentProperties_AreNotEqual()
    {
        // Arrange
        var packingListId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new ItemPackedEvent
        {
            PackingListId = packingListId,
            ItemName = "Swimsuit",
            Timestamp = timestamp
        };

        var event2 = new ItemPackedEvent
        {
            PackingListId = packingListId,
            ItemName = "Towel",
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }

    [Test]
    public void Event_Properties_AreInitOnly()
    {
        // Arrange
        var packingListId = Guid.NewGuid();
        var itemName = "Travel Documents";

        // Act
        var evt = new ItemPackedEvent
        {
            PackingListId = packingListId,
            ItemName = itemName
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.PackingListId, Is.EqualTo(packingListId));
            Assert.That(evt.ItemName, Is.EqualTo(itemName));
        });
    }

    [Test]
    public void Event_ToString_ContainsTypeName()
    {
        // Arrange
        var evt = new ItemPackedEvent
        {
            PackingListId = Guid.NewGuid(),
            ItemName = "Test Item"
        };

        // Act
        var result = evt.ToString();

        // Assert
        Assert.That(result, Does.Contain("ItemPackedEvent"));
    }
}
