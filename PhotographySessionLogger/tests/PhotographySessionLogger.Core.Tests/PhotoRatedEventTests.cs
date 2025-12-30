// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PhotographySessionLogger.Core.Tests;

public class PhotoRatedEventTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesEvent()
    {
        // Arrange & Act
        var eventRecord = new PhotoRatedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.PhotoId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.Rating, Is.EqualTo(0));
            Assert.That(eventRecord.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_WithProperties_SetsValuesCorrectly()
    {
        // Arrange
        var photoId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var eventRecord = new PhotoRatedEvent
        {
            PhotoId = photoId,
            UserId = userId,
            Rating = 5,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.PhotoId, Is.EqualTo(photoId));
            Assert.That(eventRecord.UserId, Is.EqualTo(userId));
            Assert.That(eventRecord.Rating, Is.EqualTo(5));
            Assert.That(eventRecord.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Equality_SameValues_AreEqual()
    {
        // Arrange
        var photoId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new PhotoRatedEvent
        {
            PhotoId = photoId,
            UserId = userId,
            Rating = 4,
            Timestamp = timestamp
        };

        var event2 = new PhotoRatedEvent
        {
            PhotoId = photoId,
            UserId = userId,
            Rating = 4,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Equality_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new PhotoRatedEvent
        {
            PhotoId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Rating = 5
        };

        var event2 = new PhotoRatedEvent
        {
            PhotoId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Rating = 3
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
