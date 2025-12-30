// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MusicCollectionOrganizer.Core.Tests;

public class AlbumUpdatedEventTests
{
    [Test]
    public void AlbumUpdatedEvent_CanBeCreated_WithAllProperties()
    {
        // Arrange
        var albumId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new AlbumUpdatedEvent
        {
            AlbumId = albumId,
            UserId = userId,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.AlbumId, Is.EqualTo(albumId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void AlbumUpdatedEvent_DefaultTimestamp_IsNotDefault()
    {
        // Arrange & Act
        var evt = new AlbumUpdatedEvent
        {
            AlbumId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        // Assert
        Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
    }

    [Test]
    public void AlbumUpdatedEvent_IsRecord_SupportsValueEquality()
    {
        // Arrange
        var albumId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var evt1 = new AlbumUpdatedEvent
        {
            AlbumId = albumId,
            UserId = userId,
            Timestamp = timestamp
        };

        var evt2 = new AlbumUpdatedEvent
        {
            AlbumId = albumId,
            UserId = userId,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void AlbumUpdatedEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var evt1 = new AlbumUpdatedEvent
        {
            AlbumId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        var evt2 = new AlbumUpdatedEvent
        {
            AlbumId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        // Act & Assert
        Assert.That(evt1, Is.Not.EqualTo(evt2));
    }
}
