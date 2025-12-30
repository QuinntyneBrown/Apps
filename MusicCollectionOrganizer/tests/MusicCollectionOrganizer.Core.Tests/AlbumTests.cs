// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MusicCollectionOrganizer.Core.Tests;

public class AlbumTests
{
    [Test]
    public void Constructor_CreatesAlbum_WithDefaultValues()
    {
        // Arrange & Act
        var album = new Album();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(album.AlbumId, Is.EqualTo(Guid.Empty));
            Assert.That(album.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(album.Title, Is.EqualTo(string.Empty));
            Assert.That(album.ArtistId, Is.Null);
            Assert.That(album.Artist, Is.Null);
            Assert.That(album.Format, Is.EqualTo(Format.CD));
            Assert.That(album.Genre, Is.EqualTo(Genre.Rock));
            Assert.That(album.ReleaseYear, Is.Null);
            Assert.That(album.Label, Is.Null);
            Assert.That(album.PurchasePrice, Is.Null);
            Assert.That(album.PurchaseDate, Is.Null);
            Assert.That(album.Notes, Is.Null);
            Assert.That(album.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(album.ListeningLogs, Is.Not.Null);
            Assert.That(album.ListeningLogs, Is.Empty);
        });
    }

    [Test]
    public void AlbumId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var album = new Album();
        var expectedId = Guid.NewGuid();

        // Act
        album.AlbumId = expectedId;

        // Assert
        Assert.That(album.AlbumId, Is.EqualTo(expectedId));
    }

    [Test]
    public void UserId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var album = new Album();
        var expectedUserId = Guid.NewGuid();

        // Act
        album.UserId = expectedUserId;

        // Assert
        Assert.That(album.UserId, Is.EqualTo(expectedUserId));
    }

    [Test]
    public void Title_CanBeSet_AndRetrieved()
    {
        // Arrange
        var album = new Album();
        var expectedTitle = "Dark Side of the Moon";

        // Act
        album.Title = expectedTitle;

        // Assert
        Assert.That(album.Title, Is.EqualTo(expectedTitle));
    }

    [Test]
    public void ArtistId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var album = new Album();
        var expectedArtistId = Guid.NewGuid();

        // Act
        album.ArtistId = expectedArtistId;

        // Assert
        Assert.That(album.ArtistId, Is.EqualTo(expectedArtistId));
    }

    [Test]
    public void Artist_CanBeSet_AndRetrieved()
    {
        // Arrange
        var album = new Album();
        var expectedArtist = new Artist { ArtistId = Guid.NewGuid(), Name = "Pink Floyd" };

        // Act
        album.Artist = expectedArtist;

        // Assert
        Assert.That(album.Artist, Is.EqualTo(expectedArtist));
    }

    [Test]
    public void Format_CanBeSet_AndRetrieved()
    {
        // Arrange
        var album = new Album();

        // Act
        album.Format = Format.Vinyl;

        // Assert
        Assert.That(album.Format, Is.EqualTo(Format.Vinyl));
    }

    [Test]
    public void Genre_CanBeSet_AndRetrieved()
    {
        // Arrange
        var album = new Album();

        // Act
        album.Genre = Genre.Rock;

        // Assert
        Assert.That(album.Genre, Is.EqualTo(Genre.Rock));
    }

    [Test]
    public void ReleaseYear_CanBeSet_AndRetrieved()
    {
        // Arrange
        var album = new Album();
        var expectedYear = 1973;

        // Act
        album.ReleaseYear = expectedYear;

        // Assert
        Assert.That(album.ReleaseYear, Is.EqualTo(expectedYear));
    }

    [Test]
    public void Label_CanBeSet_AndRetrieved()
    {
        // Arrange
        var album = new Album();
        var expectedLabel = "Harvest Records";

        // Act
        album.Label = expectedLabel;

        // Assert
        Assert.That(album.Label, Is.EqualTo(expectedLabel));
    }

    [Test]
    public void PurchasePrice_CanBeSet_AndRetrieved()
    {
        // Arrange
        var album = new Album();
        var expectedPrice = 29.99m;

        // Act
        album.PurchasePrice = expectedPrice;

        // Assert
        Assert.That(album.PurchasePrice, Is.EqualTo(expectedPrice));
    }

    [Test]
    public void PurchaseDate_CanBeSet_AndRetrieved()
    {
        // Arrange
        var album = new Album();
        var expectedDate = new DateTime(2023, 6, 15);

        // Act
        album.PurchaseDate = expectedDate;

        // Assert
        Assert.That(album.PurchaseDate, Is.EqualTo(expectedDate));
    }

    [Test]
    public void Notes_CanBeSet_AndRetrieved()
    {
        // Arrange
        var album = new Album();
        var expectedNotes = "Limited edition vinyl with poster";

        // Act
        album.Notes = expectedNotes;

        // Assert
        Assert.That(album.Notes, Is.EqualTo(expectedNotes));
    }

    [Test]
    public void CreatedAt_CanBeSet_AndRetrieved()
    {
        // Arrange
        var album = new Album();
        var expectedDate = new DateTime(2024, 1, 1);

        // Act
        album.CreatedAt = expectedDate;

        // Assert
        Assert.That(album.CreatedAt, Is.EqualTo(expectedDate));
    }

    [Test]
    public void ListeningLogs_CanBePopulated()
    {
        // Arrange
        var album = new Album();
        var log1 = new ListeningLog { ListeningLogId = Guid.NewGuid() };
        var log2 = new ListeningLog { ListeningLogId = Guid.NewGuid() };

        // Act
        album.ListeningLogs.Add(log1);
        album.ListeningLogs.Add(log2);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(album.ListeningLogs.Count, Is.EqualTo(2));
            Assert.That(album.ListeningLogs, Does.Contain(log1));
            Assert.That(album.ListeningLogs, Does.Contain(log2));
        });
    }
}
