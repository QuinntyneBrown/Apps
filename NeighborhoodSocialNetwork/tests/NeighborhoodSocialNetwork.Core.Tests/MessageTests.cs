// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NeighborhoodSocialNetwork.Core.Tests;

public class MessageTests
{
    [Test]
    public void Constructor_CreatesMessage_WithDefaultValues()
    {
        // Arrange & Act
        var message = new Message();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(message.MessageId, Is.EqualTo(Guid.Empty));
            Assert.That(message.SenderNeighborId, Is.EqualTo(Guid.Empty));
            Assert.That(message.RecipientNeighborId, Is.EqualTo(Guid.Empty));
            Assert.That(message.Subject, Is.EqualTo(string.Empty));
            Assert.That(message.Content, Is.EqualTo(string.Empty));
            Assert.That(message.IsRead, Is.False);
            Assert.That(message.ReadAt, Is.Null);
            Assert.That(message.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(message.SenderNeighbor, Is.Null);
        });
    }

    [Test]
    public void MessageId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var message = new Message();
        var expectedId = Guid.NewGuid();

        // Act
        message.MessageId = expectedId;

        // Assert
        Assert.That(message.MessageId, Is.EqualTo(expectedId));
    }

    [Test]
    public void SenderNeighborId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var message = new Message();
        var expectedId = Guid.NewGuid();

        // Act
        message.SenderNeighborId = expectedId;

        // Assert
        Assert.That(message.SenderNeighborId, Is.EqualTo(expectedId));
    }

    [Test]
    public void RecipientNeighborId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var message = new Message();
        var expectedId = Guid.NewGuid();

        // Act
        message.RecipientNeighborId = expectedId;

        // Assert
        Assert.That(message.RecipientNeighborId, Is.EqualTo(expectedId));
    }

    [Test]
    public void Subject_CanBeSet_AndRetrieved()
    {
        // Arrange
        var message = new Message();
        var expectedSubject = "Neighborhood Meeting";

        // Act
        message.Subject = expectedSubject;

        // Assert
        Assert.That(message.Subject, Is.EqualTo(expectedSubject));
    }

    [Test]
    public void Content_CanBeSet_AndRetrieved()
    {
        // Arrange
        var message = new Message();
        var expectedContent = "Would you like to join our meeting?";

        // Act
        message.Content = expectedContent;

        // Assert
        Assert.That(message.Content, Is.EqualTo(expectedContent));
    }

    [Test]
    public void IsRead_DefaultsToFalse()
    {
        // Arrange & Act
        var message = new Message();

        // Assert
        Assert.That(message.IsRead, Is.False);
    }

    [Test]
    public void MarkAsRead_SetsIsReadToTrue()
    {
        // Arrange
        var message = new Message
        {
            MessageId = Guid.NewGuid(),
            Subject = "Test"
        };

        // Act
        message.MarkAsRead();

        // Assert
        Assert.That(message.IsRead, Is.True);
    }

    [Test]
    public void MarkAsRead_SetsReadAtToCurrentTime()
    {
        // Arrange
        var message = new Message
        {
            MessageId = Guid.NewGuid(),
            Subject = "Test"
        };
        var beforeMark = DateTime.UtcNow;

        // Act
        message.MarkAsRead();
        var afterMark = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(message.ReadAt, Is.Not.Null);
            Assert.That(message.ReadAt!.Value, Is.GreaterThanOrEqualTo(beforeMark));
            Assert.That(message.ReadAt!.Value, Is.LessThanOrEqualTo(afterMark));
        });
    }

    [Test]
    public void MarkAsRead_WhenCalledMultipleTimes_UpdatesReadAt()
    {
        // Arrange
        var message = new Message
        {
            MessageId = Guid.NewGuid(),
            Subject = "Test"
        };
        message.MarkAsRead();
        var firstReadAt = message.ReadAt;
        Thread.Sleep(10);

        // Act
        message.MarkAsRead();

        // Assert
        Assert.That(message.ReadAt, Is.GreaterThanOrEqualTo(firstReadAt));
    }

    [Test]
    public void SenderNeighbor_CanBeSet_AndRetrieved()
    {
        // Arrange
        var message = new Message();
        var neighbor = new Neighbor { NeighborId = Guid.NewGuid(), Name = "John Doe" };

        // Act
        message.SenderNeighbor = neighbor;

        // Assert
        Assert.That(message.SenderNeighbor, Is.EqualTo(neighbor));
    }

    [Test]
    public void Message_WithAllPropertiesSet_IsValid()
    {
        // Arrange
        var messageId = Guid.NewGuid();
        var senderId = Guid.NewGuid();
        var recipientId = Guid.NewGuid();

        // Act
        var message = new Message
        {
            MessageId = messageId,
            SenderNeighborId = senderId,
            RecipientNeighborId = recipientId,
            Subject = "Test Subject",
            Content = "Test Content"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(message.MessageId, Is.EqualTo(messageId));
            Assert.That(message.SenderNeighborId, Is.EqualTo(senderId));
            Assert.That(message.RecipientNeighborId, Is.EqualTo(recipientId));
            Assert.That(message.Subject, Is.EqualTo("Test Subject"));
            Assert.That(message.Content, Is.EqualTo("Test Content"));
            Assert.That(message.IsRead, Is.False);
        });
    }
}
