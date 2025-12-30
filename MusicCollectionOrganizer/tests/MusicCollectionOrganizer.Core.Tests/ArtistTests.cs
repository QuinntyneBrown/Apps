// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MusicCollectionOrganizer.Core.Tests;

public class ArtistTests
{
    [Test]
    public void Constructor_CreatesArtist_WithDefaultValues()
    {
        // Arrange & Act
        var artist = new Artist();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(artist.ArtistId, Is.EqualTo(Guid.Empty));
            Assert.That(artist.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(artist.Name, Is.EqualTo(string.Empty));
            Assert.That(artist.Biography, Is.Null);
            Assert.That(artist.Country, Is.Null);
            Assert.That(artist.FormedYear, Is.Null);
            Assert.That(artist.Website, Is.Null);
            Assert.That(artist.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(artist.Albums, Is.Not.Null);
            Assert.That(artist.Albums, Is.Empty);
        });
    }

    [Test]
    public void ArtistId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var artist = new Artist();
        var expectedId = Guid.NewGuid();

        // Act
        artist.ArtistId = expectedId;

        // Assert
        Assert.That(artist.ArtistId, Is.EqualTo(expectedId));
    }

    [Test]
    public void UserId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var artist = new Artist();
        var expectedUserId = Guid.NewGuid();

        // Act
        artist.UserId = expectedUserId;

        // Assert
        Assert.That(artist.UserId, Is.EqualTo(expectedUserId));
    }

    [Test]
    public void Name_CanBeSet_AndRetrieved()
    {
        // Arrange
        var artist = new Artist();
        var expectedName = "The Beatles";

        // Act
        artist.Name = expectedName;

        // Assert
        Assert.That(artist.Name, Is.EqualTo(expectedName));
    }

    [Test]
    public void Biography_CanBeSet_AndRetrieved()
    {
        // Arrange
        var artist = new Artist();
        var expectedBiography = "English rock band formed in Liverpool in 1960";

        // Act
        artist.Biography = expectedBiography;

        // Assert
        Assert.That(artist.Biography, Is.EqualTo(expectedBiography));
    }

    [Test]
    public void Country_CanBeSet_AndRetrieved()
    {
        // Arrange
        var artist = new Artist();
        var expectedCountry = "United Kingdom";

        // Act
        artist.Country = expectedCountry;

        // Assert
        Assert.That(artist.Country, Is.EqualTo(expectedCountry));
    }

    [Test]
    public void FormedYear_CanBeSet_AndRetrieved()
    {
        // Arrange
        var artist = new Artist();
        var expectedYear = 1960;

        // Act
        artist.FormedYear = expectedYear;

        // Assert
        Assert.That(artist.FormedYear, Is.EqualTo(expectedYear));
    }

    [Test]
    public void Website_CanBeSet_AndRetrieved()
    {
        // Arrange
        var artist = new Artist();
        var expectedWebsite = "https://www.thebeatles.com";

        // Act
        artist.Website = expectedWebsite;

        // Assert
        Assert.That(artist.Website, Is.EqualTo(expectedWebsite));
    }

    [Test]
    public void CreatedAt_CanBeSet_AndRetrieved()
    {
        // Arrange
        var artist = new Artist();
        var expectedDate = new DateTime(2024, 1, 1);

        // Act
        artist.CreatedAt = expectedDate;

        // Assert
        Assert.That(artist.CreatedAt, Is.EqualTo(expectedDate));
    }

    [Test]
    public void Albums_CanBePopulated()
    {
        // Arrange
        var artist = new Artist();
        var album1 = new Album { AlbumId = Guid.NewGuid(), Title = "Abbey Road" };
        var album2 = new Album { AlbumId = Guid.NewGuid(), Title = "Revolver" };

        // Act
        artist.Albums.Add(album1);
        artist.Albums.Add(album2);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(artist.Albums.Count, Is.EqualTo(2));
            Assert.That(artist.Albums, Does.Contain(album1));
            Assert.That(artist.Albums, Does.Contain(album2));
        });
    }

    [Test]
    public void Artist_WithNullableFields_CanBeNull()
    {
        // Arrange & Act
        var artist = new Artist
        {
            ArtistId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Artist",
            Biography = null,
            Country = null,
            FormedYear = null,
            Website = null
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(artist.Biography, Is.Null);
            Assert.That(artist.Country, Is.Null);
            Assert.That(artist.FormedYear, Is.Null);
            Assert.That(artist.Website, Is.Null);
        });
    }
}
