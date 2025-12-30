// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NeighborhoodSocialNetwork.Core.Tests;

public class MessageReadEventTests
{
    [Test]
    public void MessageReadEvent_CanBeCreated_WithAllProperties()
    {
        // Arrange
        var messageId = Guid.NewGuid();
        var recipientId = Guid.NewGuid();
        var readAt = DateTime.UtcNow;
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new MessageReadEvent
        {
            MessageId = messageId,
            RecipientNeighborId = recipientId,
            ReadAt = readAt,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.MessageId, Is.EqualTo(messageId));
            Assert.That(evt.RecipientNeighborId, Is.EqualTo(recipientId));
            Assert.That(evt.ReadAt, Is.EqualTo(readAt));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void MessageReadEvent_DefaultTimestamp_IsNotDefault()
    {
        // Arrange & Act
        var evt = new MessageReadEvent
        {
            MessageId = Guid.NewGuid(),
            RecipientNeighborId = Guid.NewGuid(),
            ReadAt = DateTime.UtcNow
        };

        // Assert
        Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
    }

    [Test]
    public void MessageReadEvent_IsRecord_SupportsValueEquality()
    {
        // Arrange
        var messageId = Guid.NewGuid();
        var recipientId = Guid.NewGuid();
        var readAt = DateTime.UtcNow;
        var timestamp = DateTime.UtcNow;

        var evt1 = new MessageReadEvent
        {
            MessageId = messageId,
            RecipientNeighborId = recipientId,
            ReadAt = readAt,
            Timestamp = timestamp
        };

        var evt2 = new MessageReadEvent
        {
            MessageId = messageId,
            RecipientNeighborId = recipientId,
            ReadAt = readAt,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void MessageReadEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var evt1 = new MessageReadEvent
        {
            MessageId = Guid.NewGuid(),
            RecipientNeighborId = Guid.NewGuid(),
            ReadAt = DateTime.UtcNow
        };

        var evt2 = new MessageReadEvent
        {
            MessageId = Guid.NewGuid(),
            RecipientNeighborId = Guid.NewGuid(),
            ReadAt = DateTime.UtcNow
        };

        // Act & Assert
        Assert.That(evt1, Is.Not.EqualTo(evt2));
    }
}
