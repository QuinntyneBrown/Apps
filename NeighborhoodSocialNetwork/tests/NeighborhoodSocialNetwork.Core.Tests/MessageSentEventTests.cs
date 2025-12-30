// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NeighborhoodSocialNetwork.Core.Tests;

public class MessageSentEventTests
{
    [Test]
    public void MessageSentEvent_CanBeCreated_WithAllProperties()
    {
        // Arrange
        var messageId = Guid.NewGuid();
        var senderId = Guid.NewGuid();
        var recipientId = Guid.NewGuid();
        var subject = "Hello Neighbor";
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new MessageSentEvent
        {
            MessageId = messageId,
            SenderNeighborId = senderId,
            RecipientNeighborId = recipientId,
            Subject = subject,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.MessageId, Is.EqualTo(messageId));
            Assert.That(evt.SenderNeighborId, Is.EqualTo(senderId));
            Assert.That(evt.RecipientNeighborId, Is.EqualTo(recipientId));
            Assert.That(evt.Subject, Is.EqualTo(subject));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void MessageSentEvent_DefaultTimestamp_IsNotDefault()
    {
        // Arrange & Act
        var evt = new MessageSentEvent
        {
            MessageId = Guid.NewGuid(),
            SenderNeighborId = Guid.NewGuid(),
            RecipientNeighborId = Guid.NewGuid(),
            Subject = "Test"
        };

        // Assert
        Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
    }

    [Test]
    public void MessageSentEvent_IsRecord_SupportsValueEquality()
    {
        // Arrange
        var messageId = Guid.NewGuid();
        var senderId = Guid.NewGuid();
        var recipientId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var evt1 = new MessageSentEvent
        {
            MessageId = messageId,
            SenderNeighborId = senderId,
            RecipientNeighborId = recipientId,
            Subject = "Test",
            Timestamp = timestamp
        };

        var evt2 = new MessageSentEvent
        {
            MessageId = messageId,
            SenderNeighborId = senderId,
            RecipientNeighborId = recipientId,
            Subject = "Test",
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void MessageSentEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var evt1 = new MessageSentEvent
        {
            MessageId = Guid.NewGuid(),
            SenderNeighborId = Guid.NewGuid(),
            RecipientNeighborId = Guid.NewGuid(),
            Subject = "Subject 1"
        };

        var evt2 = new MessageSentEvent
        {
            MessageId = Guid.NewGuid(),
            SenderNeighborId = Guid.NewGuid(),
            RecipientNeighborId = Guid.NewGuid(),
            Subject = "Subject 2"
        };

        // Act & Assert
        Assert.That(evt1, Is.Not.EqualTo(evt2));
    }
}
