// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MusicCollectionOrganizer.Core.Tests;

public class AlbumAddedEventTests
{
    [Test]
    public void AlbumAddedEvent_CanBeCreated_WithAllProperties()
    {
        // Arrange
        var albumId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "Dark Side of the Moon";
        var format = Format.Vinyl;
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new AlbumAddedEvent
        {
            AlbumId = albumId,
            UserId = userId,
            Title = title,
            Format = format,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.AlbumId, Is.EqualTo(albumId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Title, Is.EqualTo(title));
            Assert.That(evt.Format, Is.EqualTo(format));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void AlbumAddedEvent_DefaultTimestamp_IsNotDefault()
    {
        // Arrange & Act
        var evt = new AlbumAddedEvent
        {
            AlbumId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Album"
        };

        // Assert
        Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
    }

    [Test]
    public void AlbumAddedEvent_IsRecord_SupportsValueEquality()
    {
        // Arrange
        var albumId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var evt1 = new AlbumAddedEvent
        {
            AlbumId = albumId,
            UserId = userId,
            Title = "Test Album",
            Format = Format.CD,
            Timestamp = timestamp
        };

        var evt2 = new AlbumAddedEvent
        {
            AlbumId = albumId,
            UserId = userId,
            Title = "Test Album",
            Format = Format.CD,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void AlbumAddedEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var evt1 = new AlbumAddedEvent
        {
            AlbumId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Album 1",
            Format = Format.CD
        };

        var evt2 = new AlbumAddedEvent
        {
            AlbumId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Album 2",
            Format = Format.Vinyl
        };

        // Act & Assert
        Assert.That(evt1, Is.Not.EqualTo(evt2));
    }
}
