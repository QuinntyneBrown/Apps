// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyPhotoAlbumOrganizer.Core.Tests;

public class PersonTagTests
{
    [Test]
    public void Constructor_CreatesPersonTag_WithDefaultValues()
    {
        // Arrange & Act
        var personTag = new PersonTag();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(personTag.PersonTagId, Is.EqualTo(Guid.Empty));
            Assert.That(personTag.PhotoId, Is.EqualTo(Guid.Empty));
            Assert.That(personTag.Photo, Is.Null);
            Assert.That(personTag.PersonName, Is.EqualTo(string.Empty));
            Assert.That(personTag.CoordinateX, Is.Null);
            Assert.That(personTag.CoordinateY, Is.Null);
            Assert.That(personTag.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void PersonTagId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var personTag = new PersonTag();
        var expectedId = Guid.NewGuid();

        // Act
        personTag.PersonTagId = expectedId;

        // Assert
        Assert.That(personTag.PersonTagId, Is.EqualTo(expectedId));
    }

    [Test]
    public void PhotoId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var personTag = new PersonTag();
        var expectedPhotoId = Guid.NewGuid();

        // Act
        personTag.PhotoId = expectedPhotoId;

        // Assert
        Assert.That(personTag.PhotoId, Is.EqualTo(expectedPhotoId));
    }

    [Test]
    public void PersonName_CanBeSet_AndRetrieved()
    {
        // Arrange
        var personTag = new PersonTag();
        var expectedName = "John Smith";

        // Act
        personTag.PersonName = expectedName;

        // Assert
        Assert.That(personTag.PersonName, Is.EqualTo(expectedName));
    }

    [Test]
    public void CoordinateX_CanBeSet_AndRetrieved()
    {
        // Arrange
        var personTag = new PersonTag();
        var expectedX = 150;

        // Act
        personTag.CoordinateX = expectedX;

        // Assert
        Assert.That(personTag.CoordinateX, Is.EqualTo(expectedX));
    }

    [Test]
    public void CoordinateY_CanBeSet_AndRetrieved()
    {
        // Arrange
        var personTag = new PersonTag();
        var expectedY = 200;

        // Act
        personTag.CoordinateY = expectedY;

        // Assert
        Assert.That(personTag.CoordinateY, Is.EqualTo(expectedY));
    }

    [Test]
    public void Coordinates_CanBeNull()
    {
        // Arrange
        var personTag = new PersonTag();

        // Act
        personTag.CoordinateX = null;
        personTag.CoordinateY = null;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(personTag.CoordinateX, Is.Null);
            Assert.That(personTag.CoordinateY, Is.Null);
        });
    }

    [Test]
    public void CoordinateX_CanBeZero()
    {
        // Arrange
        var personTag = new PersonTag();

        // Act
        personTag.CoordinateX = 0;

        // Assert
        Assert.That(personTag.CoordinateX, Is.EqualTo(0));
    }

    [Test]
    public void CoordinateY_CanBeNegative()
    {
        // Arrange
        var personTag = new PersonTag();

        // Act
        personTag.CoordinateY = -10;

        // Assert
        Assert.That(personTag.CoordinateY, Is.EqualTo(-10));
    }

    [Test]
    public void PersonTag_WithAllProperties_CanBeCreatedAndRetrieved()
    {
        // Arrange
        var personTagId = Guid.NewGuid();
        var photoId = Guid.NewGuid();
        var personName = "Jane Doe";
        var coordinateX = 100;
        var coordinateY = 150;
        var createdAt = DateTime.UtcNow;

        // Act
        var personTag = new PersonTag
        {
            PersonTagId = personTagId,
            PhotoId = photoId,
            PersonName = personName,
            CoordinateX = coordinateX,
            CoordinateY = coordinateY,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(personTag.PersonTagId, Is.EqualTo(personTagId));
            Assert.That(personTag.PhotoId, Is.EqualTo(photoId));
            Assert.That(personTag.PersonName, Is.EqualTo(personName));
            Assert.That(personTag.CoordinateX, Is.EqualTo(coordinateX));
            Assert.That(personTag.CoordinateY, Is.EqualTo(coordinateY));
            Assert.That(personTag.CreatedAt, Is.EqualTo(createdAt));
        });
    }

    [Test]
    public void PersonTag_WithoutCoordinates_CanBeCreated()
    {
        // Arrange
        var personTagId = Guid.NewGuid();
        var photoId = Guid.NewGuid();
        var personName = "Alice Brown";

        // Act
        var personTag = new PersonTag
        {
            PersonTagId = personTagId,
            PhotoId = photoId,
            PersonName = personName
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(personTag.PersonTagId, Is.EqualTo(personTagId));
            Assert.That(personTag.PhotoId, Is.EqualTo(photoId));
            Assert.That(personTag.PersonName, Is.EqualTo(personName));
            Assert.That(personTag.CoordinateX, Is.Null);
            Assert.That(personTag.CoordinateY, Is.Null);
        });
    }
}
