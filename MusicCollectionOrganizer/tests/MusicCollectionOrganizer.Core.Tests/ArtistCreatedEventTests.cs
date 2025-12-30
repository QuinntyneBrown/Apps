// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MusicCollectionOrganizer.Core.Tests;

public class ArtistCreatedEventTests
{
    [Test]
    public void ArtistCreatedEvent_CanBeCreated_WithAllProperties()
    {
        // Arrange
        var artistId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "The Beatles";
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new ArtistCreatedEvent
        {
            ArtistId = artistId,
            UserId = userId,
            Name = name,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ArtistId, Is.EqualTo(artistId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void ArtistCreatedEvent_DefaultTimestamp_IsNotDefault()
    {
        // Arrange & Act
        var evt = new ArtistCreatedEvent
        {
            ArtistId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Artist"
        };

        // Assert
        Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
    }

    [Test]
    public void ArtistCreatedEvent_IsRecord_SupportsValueEquality()
    {
        // Arrange
        var artistId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var evt1 = new ArtistCreatedEvent
        {
            ArtistId = artistId,
            UserId = userId,
            Name = "Test Artist",
            Timestamp = timestamp
        };

        var evt2 = new ArtistCreatedEvent
        {
            ArtistId = artistId,
            UserId = userId,
            Name = "Test Artist",
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void ArtistCreatedEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var evt1 = new ArtistCreatedEvent
        {
            ArtistId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Artist 1"
        };

        var evt2 = new ArtistCreatedEvent
        {
            ArtistId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Artist 2"
        };

        // Act & Assert
        Assert.That(evt1, Is.Not.EqualTo(evt2));
    }
}
