// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyPhotoAlbumOrganizer.Core.Tests;

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
            Assert.That(album.Name, Is.EqualTo(string.Empty));
            Assert.That(album.Description, Is.Null);
            Assert.That(album.CoverPhotoUrl, Is.Null);
            Assert.That(album.CreatedDate, Is.EqualTo(default(DateTime)));
            Assert.That(album.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            Assert.That(album.Photos, Is.Not.Null);
            Assert.That(album.Photos, Is.Empty);
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
    public void Name_CanBeSet_AndRetrieved()
    {
        // Arrange
        var album = new Album();
        var expectedName = "Summer Vacation 2023";

        // Act
        album.Name = expectedName;

        // Assert
        Assert.That(album.Name, Is.EqualTo(expectedName));
    }

    [Test]
    public void Description_CanBeSet_AndRetrieved()
    {
        // Arrange
        var album = new Album();
        var expectedDescription = "Photos from our trip to Hawaii";

        // Act
        album.Description = expectedDescription;

        // Assert
        Assert.That(album.Description, Is.EqualTo(expectedDescription));
    }

    [Test]
    public void Description_CanBeNull()
    {
        // Arrange
        var album = new Album();

        // Act
        album.Description = null;

        // Assert
        Assert.That(album.Description, Is.Null);
    }

    [Test]
    public void CoverPhotoUrl_CanBeSet_AndRetrieved()
    {
        // Arrange
        var album = new Album();
        var expectedUrl = "https://storage.example.com/cover.jpg";

        // Act
        album.CoverPhotoUrl = expectedUrl;

        // Assert
        Assert.That(album.CoverPhotoUrl, Is.EqualTo(expectedUrl));
    }

    [Test]
    public void CreatedDate_CanBeSet_AndRetrieved()
    {
        // Arrange
        var album = new Album();
        var expectedDate = new DateTime(2023, 7, 1);

        // Act
        album.CreatedDate = expectedDate;

        // Assert
        Assert.That(album.CreatedDate, Is.EqualTo(expectedDate));
    }

    [Test]
    public void Photos_CanBeAdded()
    {
        // Arrange
        var album = new Album();
        var photo = new Photo { PhotoId = Guid.NewGuid(), FileName = "photo1.jpg" };

        // Act
        album.Photos.Add(photo);

        // Assert
        Assert.That(album.Photos, Has.Count.EqualTo(1));
        Assert.That(album.Photos.First().FileName, Is.EqualTo("photo1.jpg"));
    }

    [Test]
    public void Photos_MultiplePhotos_CanBeAdded()
    {
        // Arrange
        var album = new Album();
        var photo1 = new Photo { PhotoId = Guid.NewGuid(), FileName = "photo1.jpg" };
        var photo2 = new Photo { PhotoId = Guid.NewGuid(), FileName = "photo2.jpg" };

        // Act
        album.Photos.Add(photo1);
        album.Photos.Add(photo2);

        // Assert
        Assert.That(album.Photos, Has.Count.EqualTo(2));
    }

    [Test]
    public void Album_WithAllProperties_CanBeCreatedAndRetrieved()
    {
        // Arrange
        var albumId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Family Reunion";
        var description = "Annual family gathering photos";
        var coverPhotoUrl = "https://example.com/cover.jpg";
        var createdDate = new DateTime(2023, 8, 15);
        var createdAt = DateTime.UtcNow;

        // Act
        var album = new Album
        {
            AlbumId = albumId,
            UserId = userId,
            Name = name,
            Description = description,
            CoverPhotoUrl = coverPhotoUrl,
            CreatedDate = createdDate,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(album.AlbumId, Is.EqualTo(albumId));
            Assert.That(album.UserId, Is.EqualTo(userId));
            Assert.That(album.Name, Is.EqualTo(name));
            Assert.That(album.Description, Is.EqualTo(description));
            Assert.That(album.CoverPhotoUrl, Is.EqualTo(coverPhotoUrl));
            Assert.That(album.CreatedDate, Is.EqualTo(createdDate));
            Assert.That(album.CreatedAt, Is.EqualTo(createdAt));
        });
    }
}
