// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PhotographySessionLogger.Core.Tests;

public class PhotoUploadedEventTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesEvent()
    {
        // Arrange & Act
        var eventRecord = new PhotoUploadedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.PhotoId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.SessionId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.FileName, Is.EqualTo(string.Empty));
            Assert.That(eventRecord.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_WithProperties_SetsValuesCorrectly()
    {
        // Arrange
        var photoId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var sessionId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var eventRecord = new PhotoUploadedEvent
        {
            PhotoId = photoId,
            UserId = userId,
            SessionId = sessionId,
            FileName = "IMG_0001.jpg",
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.PhotoId, Is.EqualTo(photoId));
            Assert.That(eventRecord.UserId, Is.EqualTo(userId));
            Assert.That(eventRecord.SessionId, Is.EqualTo(sessionId));
            Assert.That(eventRecord.FileName, Is.EqualTo("IMG_0001.jpg"));
            Assert.That(eventRecord.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Equality_SameValues_AreEqual()
    {
        // Arrange
        var photoId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var sessionId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new PhotoUploadedEvent
        {
            PhotoId = photoId,
            UserId = userId,
            SessionId = sessionId,
            FileName = "photo.jpg",
            Timestamp = timestamp
        };

        var event2 = new PhotoUploadedEvent
        {
            PhotoId = photoId,
            UserId = userId,
            SessionId = sessionId,
            FileName = "photo.jpg",
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Equality_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new PhotoUploadedEvent
        {
            PhotoId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            SessionId = Guid.NewGuid(),
            FileName = "photo1.jpg"
        };

        var event2 = new PhotoUploadedEvent
        {
            PhotoId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            SessionId = Guid.NewGuid(),
            FileName = "photo2.jpg"
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
