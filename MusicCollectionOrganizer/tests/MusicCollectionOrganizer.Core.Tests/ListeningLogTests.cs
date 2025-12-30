// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MusicCollectionOrganizer.Core.Tests;

public class ListeningLogTests
{
    [Test]
    public void Constructor_CreatesListeningLog_WithDefaultValues()
    {
        // Arrange & Act
        var log = new ListeningLog();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(log.ListeningLogId, Is.EqualTo(Guid.Empty));
            Assert.That(log.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(log.AlbumId, Is.EqualTo(Guid.Empty));
            Assert.That(log.Album, Is.Null);
            Assert.That(log.ListeningDate, Is.Not.EqualTo(default(DateTime)));
            Assert.That(log.Rating, Is.Null);
            Assert.That(log.Notes, Is.Null);
            Assert.That(log.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void ListeningLogId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var log = new ListeningLog();
        var expectedId = Guid.NewGuid();

        // Act
        log.ListeningLogId = expectedId;

        // Assert
        Assert.That(log.ListeningLogId, Is.EqualTo(expectedId));
    }

    [Test]
    public void UserId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var log = new ListeningLog();
        var expectedUserId = Guid.NewGuid();

        // Act
        log.UserId = expectedUserId;

        // Assert
        Assert.That(log.UserId, Is.EqualTo(expectedUserId));
    }

    [Test]
    public void AlbumId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var log = new ListeningLog();
        var expectedAlbumId = Guid.NewGuid();

        // Act
        log.AlbumId = expectedAlbumId;

        // Assert
        Assert.That(log.AlbumId, Is.EqualTo(expectedAlbumId));
    }

    [Test]
    public void Album_CanBeSet_AndRetrieved()
    {
        // Arrange
        var log = new ListeningLog();
        var expectedAlbum = new Album { AlbumId = Guid.NewGuid(), Title = "Test Album" };

        // Act
        log.Album = expectedAlbum;

        // Assert
        Assert.That(log.Album, Is.EqualTo(expectedAlbum));
    }

    [Test]
    public void ListeningDate_CanBeSet_AndRetrieved()
    {
        // Arrange
        var log = new ListeningLog();
        var expectedDate = new DateTime(2024, 6, 15, 14, 30, 0);

        // Act
        log.ListeningDate = expectedDate;

        // Assert
        Assert.That(log.ListeningDate, Is.EqualTo(expectedDate));
    }

    [Test]
    public void Rating_CanBeSet_AndRetrieved()
    {
        // Arrange
        var log = new ListeningLog();
        var expectedRating = 5;

        // Act
        log.Rating = expectedRating;

        // Assert
        Assert.That(log.Rating, Is.EqualTo(expectedRating));
    }

    [Test]
    public void Rating_CanBeNull()
    {
        // Arrange
        var log = new ListeningLog();

        // Act
        log.Rating = null;

        // Assert
        Assert.That(log.Rating, Is.Null);
    }

    [Test]
    public void Notes_CanBeSet_AndRetrieved()
    {
        // Arrange
        var log = new ListeningLog();
        var expectedNotes = "Great listening session, noticed new details";

        // Act
        log.Notes = expectedNotes;

        // Assert
        Assert.That(log.Notes, Is.EqualTo(expectedNotes));
    }

    [Test]
    public void CreatedAt_CanBeSet_AndRetrieved()
    {
        // Arrange
        var log = new ListeningLog();
        var expectedDate = new DateTime(2024, 1, 1);

        // Act
        log.CreatedAt = expectedDate;

        // Assert
        Assert.That(log.CreatedAt, Is.EqualTo(expectedDate));
    }

    [Test]
    public void ListeningLog_WithAllPropertiesSet_IsValid()
    {
        // Arrange
        var albumId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var logId = Guid.NewGuid();
        var listeningDate = DateTime.UtcNow;

        // Act
        var log = new ListeningLog
        {
            ListeningLogId = logId,
            UserId = userId,
            AlbumId = albumId,
            ListeningDate = listeningDate,
            Rating = 4,
            Notes = "Test notes"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(log.ListeningLogId, Is.EqualTo(logId));
            Assert.That(log.UserId, Is.EqualTo(userId));
            Assert.That(log.AlbumId, Is.EqualTo(albumId));
            Assert.That(log.ListeningDate, Is.EqualTo(listeningDate));
            Assert.That(log.Rating, Is.EqualTo(4));
            Assert.That(log.Notes, Is.EqualTo("Test notes"));
        });
    }
}
