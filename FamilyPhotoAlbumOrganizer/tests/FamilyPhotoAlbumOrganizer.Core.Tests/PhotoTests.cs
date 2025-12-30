// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyPhotoAlbumOrganizer.Core.Tests;

public class PhotoTests
{
    [Test]
    public void Constructor_CreatesPhoto_WithDefaultValues()
    {
        // Arrange & Act
        var photo = new Photo();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(photo.PhotoId, Is.EqualTo(Guid.Empty));
            Assert.That(photo.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(photo.AlbumId, Is.Null);
            Assert.That(photo.Album, Is.Null);
            Assert.That(photo.FileName, Is.EqualTo(string.Empty));
            Assert.That(photo.FileUrl, Is.EqualTo(string.Empty));
            Assert.That(photo.ThumbnailUrl, Is.Null);
            Assert.That(photo.Caption, Is.Null);
            Assert.That(photo.DateTaken, Is.Null);
            Assert.That(photo.Location, Is.Null);
            Assert.That(photo.IsFavorite, Is.False);
            Assert.That(photo.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            Assert.That(photo.Tags, Is.Not.Null);
            Assert.That(photo.Tags, Is.Empty);
            Assert.That(photo.PersonTags, Is.Not.Null);
            Assert.That(photo.PersonTags, Is.Empty);
        });
    }

    [Test]
    public void PhotoId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var photo = new Photo();
        var expectedId = Guid.NewGuid();

        // Act
        photo.PhotoId = expectedId;

        // Assert
        Assert.That(photo.PhotoId, Is.EqualTo(expectedId));
    }

    [Test]
    public void UserId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var photo = new Photo();
        var expectedUserId = Guid.NewGuid();

        // Act
        photo.UserId = expectedUserId;

        // Assert
        Assert.That(photo.UserId, Is.EqualTo(expectedUserId));
    }

    [Test]
    public void AlbumId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var photo = new Photo();
        var expectedAlbumId = Guid.NewGuid();

        // Act
        photo.AlbumId = expectedAlbumId;

        // Assert
        Assert.That(photo.AlbumId, Is.EqualTo(expectedAlbumId));
    }

    [Test]
    public void AlbumId_CanBeNull()
    {
        // Arrange
        var photo = new Photo();

        // Act
        photo.AlbumId = null;

        // Assert
        Assert.That(photo.AlbumId, Is.Null);
    }

    [Test]
    public void FileName_CanBeSet_AndRetrieved()
    {
        // Arrange
        var photo = new Photo();
        var expectedFileName = "vacation-2023.jpg";

        // Act
        photo.FileName = expectedFileName;

        // Assert
        Assert.That(photo.FileName, Is.EqualTo(expectedFileName));
    }

    [Test]
    public void FileUrl_CanBeSet_AndRetrieved()
    {
        // Arrange
        var photo = new Photo();
        var expectedUrl = "https://storage.example.com/photos/vacation.jpg";

        // Act
        photo.FileUrl = expectedUrl;

        // Assert
        Assert.That(photo.FileUrl, Is.EqualTo(expectedUrl));
    }

    [Test]
    public void ThumbnailUrl_CanBeSet_AndRetrieved()
    {
        // Arrange
        var photo = new Photo();
        var expectedThumbnail = "https://storage.example.com/thumbs/vacation.jpg";

        // Act
        photo.ThumbnailUrl = expectedThumbnail;

        // Assert
        Assert.That(photo.ThumbnailUrl, Is.EqualTo(expectedThumbnail));
    }

    [Test]
    public void Caption_CanBeSet_AndRetrieved()
    {
        // Arrange
        var photo = new Photo();
        var expectedCaption = "Summer vacation at the beach";

        // Act
        photo.Caption = expectedCaption;

        // Assert
        Assert.That(photo.Caption, Is.EqualTo(expectedCaption));
    }

    [Test]
    public void DateTaken_CanBeSet_AndRetrieved()
    {
        // Arrange
        var photo = new Photo();
        var expectedDate = new DateTime(2023, 7, 15, 14, 30, 0);

        // Act
        photo.DateTaken = expectedDate;

        // Assert
        Assert.That(photo.DateTaken, Is.EqualTo(expectedDate));
    }

    [Test]
    public void Location_CanBeSet_AndRetrieved()
    {
        // Arrange
        var photo = new Photo();
        var expectedLocation = "Santa Monica Beach, California";

        // Act
        photo.Location = expectedLocation;

        // Assert
        Assert.That(photo.Location, Is.EqualTo(expectedLocation));
    }

    [Test]
    public void IsFavorite_DefaultsToFalse()
    {
        // Arrange & Act
        var photo = new Photo();

        // Assert
        Assert.That(photo.IsFavorite, Is.False);
    }

    [Test]
    public void IsFavorite_CanBeSet_ToTrue()
    {
        // Arrange
        var photo = new Photo();

        // Act
        photo.IsFavorite = true;

        // Assert
        Assert.That(photo.IsFavorite, Is.True);
    }

    [Test]
    public void Tags_CanBeAdded()
    {
        // Arrange
        var photo = new Photo();
        var tag = new Tag { TagId = Guid.NewGuid(), Name = "Vacation" };

        // Act
        photo.Tags.Add(tag);

        // Assert
        Assert.That(photo.Tags, Has.Count.EqualTo(1));
        Assert.That(photo.Tags.First().Name, Is.EqualTo("Vacation"));
    }

    [Test]
    public void PersonTags_CanBeAdded()
    {
        // Arrange
        var photo = new Photo();
        var personTag = new PersonTag { PersonTagId = Guid.NewGuid(), PersonName = "John Doe" };

        // Act
        photo.PersonTags.Add(personTag);

        // Assert
        Assert.That(photo.PersonTags, Has.Count.EqualTo(1));
        Assert.That(photo.PersonTags.First().PersonName, Is.EqualTo("John Doe"));
    }

    [Test]
    public void Photo_WithAllProperties_CanBeCreatedAndRetrieved()
    {
        // Arrange
        var photoId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var albumId = Guid.NewGuid();
        var fileName = "photo.jpg";
        var fileUrl = "https://example.com/photo.jpg";
        var thumbnailUrl = "https://example.com/thumb.jpg";
        var caption = "Beautiful sunset";
        var dateTaken = DateTime.UtcNow.AddDays(-10);
        var location = "Grand Canyon";
        var isFavorite = true;
        var createdAt = DateTime.UtcNow;

        // Act
        var photo = new Photo
        {
            PhotoId = photoId,
            UserId = userId,
            AlbumId = albumId,
            FileName = fileName,
            FileUrl = fileUrl,
            ThumbnailUrl = thumbnailUrl,
            Caption = caption,
            DateTaken = dateTaken,
            Location = location,
            IsFavorite = isFavorite,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(photo.PhotoId, Is.EqualTo(photoId));
            Assert.That(photo.UserId, Is.EqualTo(userId));
            Assert.That(photo.AlbumId, Is.EqualTo(albumId));
            Assert.That(photo.FileName, Is.EqualTo(fileName));
            Assert.That(photo.FileUrl, Is.EqualTo(fileUrl));
            Assert.That(photo.ThumbnailUrl, Is.EqualTo(thumbnailUrl));
            Assert.That(photo.Caption, Is.EqualTo(caption));
            Assert.That(photo.DateTaken, Is.EqualTo(dateTaken));
            Assert.That(photo.Location, Is.EqualTo(location));
            Assert.That(photo.IsFavorite, Is.EqualTo(isFavorite));
            Assert.That(photo.CreatedAt, Is.EqualTo(createdAt));
        });
    }
}
