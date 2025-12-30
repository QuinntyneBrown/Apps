// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MusicCollectionOrganizer.Core.Tests;

public class ListeningLogUpdatedEventTests
{
    [Test]
    public void ListeningLogUpdatedEvent_CanBeCreated_WithAllProperties()
    {
        // Arrange
        var listeningLogId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new ListeningLogUpdatedEvent
        {
            ListeningLogId = listeningLogId,
            UserId = userId,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ListeningLogId, Is.EqualTo(listeningLogId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void ListeningLogUpdatedEvent_DefaultTimestamp_IsNotDefault()
    {
        // Arrange & Act
        var evt = new ListeningLogUpdatedEvent
        {
            ListeningLogId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        // Assert
        Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
    }

    [Test]
    public void ListeningLogUpdatedEvent_IsRecord_SupportsValueEquality()
    {
        // Arrange
        var listeningLogId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var evt1 = new ListeningLogUpdatedEvent
        {
            ListeningLogId = listeningLogId,
            UserId = userId,
            Timestamp = timestamp
        };

        var evt2 = new ListeningLogUpdatedEvent
        {
            ListeningLogId = listeningLogId,
            UserId = userId,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void ListeningLogUpdatedEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var evt1 = new ListeningLogUpdatedEvent
        {
            ListeningLogId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        var evt2 = new ListeningLogUpdatedEvent
        {
            ListeningLogId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        // Act & Assert
        Assert.That(evt1, Is.Not.EqualTo(evt2));
    }
}
