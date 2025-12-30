// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GiftIdeaTracker.Core.Tests;

public class EventTests
{
    [Test]
    public void GiftIdeaCreatedEvent_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var giftIdeaId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Laptop";
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new GiftIdeaCreatedEvent
        {
            GiftIdeaId = giftIdeaId,
            UserId = userId,
            Name = name,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.GiftIdeaId, Is.EqualTo(giftIdeaId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void GiftIdeaCreatedEvent_Timestamp_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var evt = new GiftIdeaCreatedEvent
        {
            GiftIdeaId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(evt.Timestamp, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void RecipientAddedEvent_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var recipientId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "John Smith";
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new RecipientAddedEvent
        {
            RecipientId = recipientId,
            UserId = userId,
            Name = name,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.RecipientId, Is.EqualTo(recipientId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void RecipientAddedEvent_Timestamp_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var evt = new RecipientAddedEvent
        {
            RecipientId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(evt.Timestamp, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void GiftPurchasedEvent_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var purchaseId = Guid.NewGuid();
        var giftIdeaId = Guid.NewGuid();
        var actualPrice = 149.99m;
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new GiftPurchasedEvent
        {
            PurchaseId = purchaseId,
            GiftIdeaId = giftIdeaId,
            ActualPrice = actualPrice,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.PurchaseId, Is.EqualTo(purchaseId));
            Assert.That(evt.GiftIdeaId, Is.EqualTo(giftIdeaId));
            Assert.That(evt.ActualPrice, Is.EqualTo(actualPrice));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void GiftPurchasedEvent_Timestamp_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var evt = new GiftPurchasedEvent
        {
            PurchaseId = Guid.NewGuid(),
            GiftIdeaId = Guid.NewGuid(),
            ActualPrice = 99.99m
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
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new GiftIdeaCreatedEvent
        {
            GiftIdeaId = id,
            UserId = userId,
            Name = "Test",
            Timestamp = timestamp
        };

        var event2 = new GiftIdeaCreatedEvent
        {
            GiftIdeaId = id,
            UserId = userId,
            Name = "Test",
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }
}
