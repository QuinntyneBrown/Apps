// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MusicCollectionOrganizer.Core.Tests;

public class ArtistUpdatedEventTests
{
    [Test]
    public void ArtistUpdatedEvent_CanBeCreated_WithAllProperties()
    {
        // Arrange
        var artistId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new ArtistUpdatedEvent
        {
            ArtistId = artistId,
            UserId = userId,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ArtistId, Is.EqualTo(artistId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void ArtistUpdatedEvent_DefaultTimestamp_IsNotDefault()
    {
        // Arrange & Act
        var evt = new ArtistUpdatedEvent
        {
            ArtistId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        // Assert
        Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
    }

    [Test]
    public void ArtistUpdatedEvent_IsRecord_SupportsValueEquality()
    {
        // Arrange
        var artistId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var evt1 = new ArtistUpdatedEvent
        {
            ArtistId = artistId,
            UserId = userId,
            Timestamp = timestamp
        };

        var evt2 = new ArtistUpdatedEvent
        {
            ArtistId = artistId,
            UserId = userId,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void ArtistUpdatedEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var evt1 = new ArtistUpdatedEvent
        {
            ArtistId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        var evt2 = new ArtistUpdatedEvent
        {
            ArtistId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        // Act & Assert
        Assert.That(evt1, Is.Not.EqualTo(evt2));
    }
}
