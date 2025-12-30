// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PhotographySessionLogger.Core.Tests;

public class PhotoTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesPhoto()
    {
        // Arrange & Act
        var photo = new Photo();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(photo.PhotoId, Is.EqualTo(Guid.Empty));
            Assert.That(photo.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(photo.SessionId, Is.EqualTo(Guid.Empty));
            Assert.That(photo.Session, Is.Null);
            Assert.That(photo.FileName, Is.EqualTo(string.Empty));
            Assert.That(photo.FilePath, Is.Null);
            Assert.That(photo.CameraSettings, Is.Null);
            Assert.That(photo.Rating, Is.Null);
            Assert.That(photo.Tags, Is.Null);
            Assert.That(photo.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_WithProperties_SetsValuesCorrectly()
    {
        // Arrange
        var photoId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var sessionId = Guid.NewGuid();

        // Act
        var photo = new Photo
        {
            PhotoId = photoId,
            UserId = userId,
            SessionId = sessionId,
            FileName = "IMG_0001.jpg",
            FilePath = "/photos/IMG_0001.jpg",
            CameraSettings = "f/2.8, 1/200s, ISO 400",
            Rating = 5,
            Tags = "portrait,outdoor"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(photo.PhotoId, Is.EqualTo(photoId));
            Assert.That(photo.UserId, Is.EqualTo(userId));
            Assert.That(photo.SessionId, Is.EqualTo(sessionId));
            Assert.That(photo.FileName, Is.EqualTo("IMG_0001.jpg"));
            Assert.That(photo.FilePath, Is.EqualTo("/photos/IMG_0001.jpg"));
            Assert.That(photo.CameraSettings, Is.EqualTo("f/2.8, 1/200s, ISO 400"));
            Assert.That(photo.Rating, Is.EqualTo(5));
            Assert.That(photo.Tags, Is.EqualTo("portrait,outdoor"));
        });
    }

    [Test]
    public void Session_NavigationProperty_CanBeSet()
    {
        // Arrange
        var photo = new Photo();
        var session = new Session
        {
            SessionId = Guid.NewGuid(),
            Title = "Test Session"
        };

        // Act
        photo.Session = session;

        // Assert
        Assert.That(photo.Session, Is.EqualTo(session));
    }

    [Test]
    public void FilePath_CanBeNull()
    {
        // Arrange & Act
        var photo = new Photo { FilePath = null };

        // Assert
        Assert.That(photo.FilePath, Is.Null);
    }

    [Test]
    public void CameraSettings_CanBeNull()
    {
        // Arrange & Act
        var photo = new Photo { CameraSettings = null };

        // Assert
        Assert.That(photo.CameraSettings, Is.Null);
    }

    [Test]
    public void Rating_CanBeNull()
    {
        // Arrange & Act
        var photo = new Photo { Rating = null };

        // Assert
        Assert.That(photo.Rating, Is.Null);
    }

    [Test]
    public void Rating_CanBeSetToValidValue()
    {
        // Arrange & Act
        var photo = new Photo { Rating = 4 };

        // Assert
        Assert.That(photo.Rating, Is.EqualTo(4));
    }

    [Test]
    public void Tags_CanBeNull()
    {
        // Arrange & Act
        var photo = new Photo { Tags = null };

        // Assert
        Assert.That(photo.Tags, Is.Null);
    }
}
