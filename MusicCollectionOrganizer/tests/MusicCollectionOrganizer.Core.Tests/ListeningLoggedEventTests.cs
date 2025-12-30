// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MusicCollectionOrganizer.Core.Tests;

public class ListeningLoggedEventTests
{
    [Test]
    public void ListeningLoggedEvent_CanBeCreated_WithAllProperties()
    {
        // Arrange
        var listeningLogId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var albumId = Guid.NewGuid();
        var listeningDate = DateTime.UtcNow;
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new ListeningLoggedEvent
        {
            ListeningLogId = listeningLogId,
            UserId = userId,
            AlbumId = albumId,
            ListeningDate = listeningDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ListeningLogId, Is.EqualTo(listeningLogId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.AlbumId, Is.EqualTo(albumId));
            Assert.That(evt.ListeningDate, Is.EqualTo(listeningDate));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void ListeningLoggedEvent_DefaultTimestamp_IsNotDefault()
    {
        // Arrange & Act
        var evt = new ListeningLoggedEvent
        {
            ListeningLogId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            AlbumId = Guid.NewGuid(),
            ListeningDate = DateTime.UtcNow
        };

        // Assert
        Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
    }

    [Test]
    public void ListeningLoggedEvent_IsRecord_SupportsValueEquality()
    {
        // Arrange
        var listeningLogId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var albumId = Guid.NewGuid();
        var listeningDate = DateTime.UtcNow;
        var timestamp = DateTime.UtcNow;

        var evt1 = new ListeningLoggedEvent
        {
            ListeningLogId = listeningLogId,
            UserId = userId,
            AlbumId = albumId,
            ListeningDate = listeningDate,
            Timestamp = timestamp
        };

        var evt2 = new ListeningLoggedEvent
        {
            ListeningLogId = listeningLogId,
            UserId = userId,
            AlbumId = albumId,
            ListeningDate = listeningDate,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void ListeningLoggedEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var evt1 = new ListeningLoggedEvent
        {
            ListeningLogId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            AlbumId = Guid.NewGuid(),
            ListeningDate = DateTime.UtcNow
        };

        var evt2 = new ListeningLoggedEvent
        {
            ListeningLogId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            AlbumId = Guid.NewGuid(),
            ListeningDate = DateTime.UtcNow
        };

        // Act & Assert
        Assert.That(evt1, Is.Not.EqualTo(evt2));
    }
}
