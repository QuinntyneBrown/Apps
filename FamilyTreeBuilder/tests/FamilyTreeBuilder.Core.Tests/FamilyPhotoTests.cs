// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyTreeBuilder.Core.Tests;

public class FamilyPhotoTests
{
    [Test]
    public void Constructor_CreatesFamilyPhoto_WithDefaultValues()
    {
        // Arrange & Act
        var photo = new FamilyPhoto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(photo.FamilyPhotoId, Is.EqualTo(Guid.Empty));
            Assert.That(photo.PersonId, Is.EqualTo(Guid.Empty));
            Assert.That(photo.Person, Is.Null);
            Assert.That(photo.PhotoUrl, Is.Null);
            Assert.That(photo.Caption, Is.Null);
            Assert.That(photo.DateTaken, Is.Null);
            Assert.That(photo.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void FamilyPhotoId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var photo = new FamilyPhoto();
        var expectedId = Guid.NewGuid();

        // Act
        photo.FamilyPhotoId = expectedId;

        // Assert
        Assert.That(photo.FamilyPhotoId, Is.EqualTo(expectedId));
    }

    [Test]
    public void PersonId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var photo = new FamilyPhoto();
        var expectedPersonId = Guid.NewGuid();

        // Act
        photo.PersonId = expectedPersonId;

        // Assert
        Assert.That(photo.PersonId, Is.EqualTo(expectedPersonId));
    }

    [Test]
    public void PhotoUrl_CanBeSet_AndRetrieved()
    {
        // Arrange
        var photo = new FamilyPhoto();
        var expectedUrl = "https://example.com/photo.jpg";

        // Act
        photo.PhotoUrl = expectedUrl;

        // Assert
        Assert.That(photo.PhotoUrl, Is.EqualTo(expectedUrl));
    }

    [Test]
    public void Caption_CanBeSet_AndRetrieved()
    {
        // Arrange
        var photo = new FamilyPhoto();
        var expectedCaption = "Grandfather's 80th birthday";

        // Act
        photo.Caption = expectedCaption;

        // Assert
        Assert.That(photo.Caption, Is.EqualTo(expectedCaption));
    }

    [Test]
    public void DateTaken_CanBeSet_AndRetrieved()
    {
        // Arrange
        var photo = new FamilyPhoto();
        var expectedDate = new DateTime(1950, 6, 15);

        // Act
        photo.DateTaken = expectedDate;

        // Assert
        Assert.That(photo.DateTaken, Is.EqualTo(expectedDate));
    }

    [Test]
    public void PhotoUrl_CanBeNull()
    {
        // Arrange
        var photo = new FamilyPhoto();

        // Act
        photo.PhotoUrl = null;

        // Assert
        Assert.That(photo.PhotoUrl, Is.Null);
    }

    [Test]
    public void FamilyPhoto_WithAllProperties_CanBeCreatedAndRetrieved()
    {
        // Arrange
        var photoId = Guid.NewGuid();
        var personId = Guid.NewGuid();
        var photoUrl = "https://storage.example.com/family-photo.jpg";
        var caption = "Family reunion 2020";
        var dateTaken = new DateTime(2020, 7, 4);
        var createdAt = DateTime.UtcNow;

        // Act
        var photo = new FamilyPhoto
        {
            FamilyPhotoId = photoId,
            PersonId = personId,
            PhotoUrl = photoUrl,
            Caption = caption,
            DateTaken = dateTaken,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(photo.FamilyPhotoId, Is.EqualTo(photoId));
            Assert.That(photo.PersonId, Is.EqualTo(personId));
            Assert.That(photo.PhotoUrl, Is.EqualTo(photoUrl));
            Assert.That(photo.Caption, Is.EqualTo(caption));
            Assert.That(photo.DateTaken, Is.EqualTo(dateTaken));
            Assert.That(photo.CreatedAt, Is.EqualTo(createdAt));
        });
    }
}
