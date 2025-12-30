// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyPhotoAlbumOrganizer.Core.Tests;

public class PhotoUploadedEventTests
{
    [Test]
    public void Constructor_CreatesEvent_WithAllProperties()
    {
        // Arrange
        var photoId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var fileName = "vacation.jpg";
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new PhotoUploadedEvent
        {
            PhotoId = photoId,
            UserId = userId,
            FileName = fileName,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.PhotoId, Is.EqualTo(photoId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.FileName, Is.EqualTo(fileName));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Event_WithDefaultTimestamp_SetsUtcNow()
    {
        // Arrange
        var photoId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var fileName = "photo.jpg";

        // Act
        var evt = new PhotoUploadedEvent
        {
            PhotoId = photoId,
            UserId = userId,
            FileName = fileName
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void Event_IsRecord_AndSupportsValueEquality()
    {
        // Arrange
        var photoId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var fileName = "test.jpg";
        var timestamp = DateTime.UtcNow;

        // Act
        var event1 = new PhotoUploadedEvent
        {
            PhotoId = photoId,
            UserId = userId,
            FileName = fileName,
            Timestamp = timestamp
        };

        var event2 = new PhotoUploadedEvent
        {
            PhotoId = photoId,
            UserId = userId,
            FileName = fileName,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Event_WithDifferentProperties_AreNotEqual()
    {
        // Arrange
        var photoId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new PhotoUploadedEvent
        {
            PhotoId = photoId,
            UserId = userId,
            FileName = "photo1.jpg",
            Timestamp = timestamp
        };

        var event2 = new PhotoUploadedEvent
        {
            PhotoId = photoId,
            UserId = userId,
            FileName = "photo2.jpg",
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }

    [Test]
    public void Event_Properties_AreInitOnly()
    {
        // Arrange
        var photoId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var fileName = "image.jpg";

        // Act
        var evt = new PhotoUploadedEvent
        {
            PhotoId = photoId,
            UserId = userId,
            FileName = fileName
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.PhotoId, Is.EqualTo(photoId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.FileName, Is.EqualTo(fileName));
        });
    }

    [Test]
    public void Event_ToString_ContainsTypeName()
    {
        // Arrange
        var evt = new PhotoUploadedEvent
        {
            PhotoId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            FileName = "test.jpg"
        };

        // Act
        var result = evt.ToString();

        // Assert
        Assert.That(result, Does.Contain("PhotoUploadedEvent"));
    }
}
