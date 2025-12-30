// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyPhotoAlbumOrganizer.Core.Tests;

public class TagTests
{
    [Test]
    public void Constructor_CreatesTag_WithDefaultValues()
    {
        // Arrange & Act
        var tag = new Tag();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(tag.TagId, Is.EqualTo(Guid.Empty));
            Assert.That(tag.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(tag.Name, Is.EqualTo(string.Empty));
            Assert.That(tag.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            Assert.That(tag.Photos, Is.Not.Null);
            Assert.That(tag.Photos, Is.Empty);
        });
    }

    [Test]
    public void TagId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var tag = new Tag();
        var expectedId = Guid.NewGuid();

        // Act
        tag.TagId = expectedId;

        // Assert
        Assert.That(tag.TagId, Is.EqualTo(expectedId));
    }

    [Test]
    public void UserId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var tag = new Tag();
        var expectedUserId = Guid.NewGuid();

        // Act
        tag.UserId = expectedUserId;

        // Assert
        Assert.That(tag.UserId, Is.EqualTo(expectedUserId));
    }

    [Test]
    public void Name_CanBeSet_AndRetrieved()
    {
        // Arrange
        var tag = new Tag();
        var expectedName = "Vacation";

        // Act
        tag.Name = expectedName;

        // Assert
        Assert.That(tag.Name, Is.EqualTo(expectedName));
    }

    [Test]
    public void CreatedAt_DefaultsToUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var tag = new Tag();
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(tag.CreatedAt, Is.GreaterThanOrEqualTo(beforeCreation));
        Assert.That(tag.CreatedAt, Is.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void Photos_CanBeAdded()
    {
        // Arrange
        var tag = new Tag();
        var photo = new Photo { PhotoId = Guid.NewGuid(), FileName = "sunset.jpg" };

        // Act
        tag.Photos.Add(photo);

        // Assert
        Assert.That(tag.Photos, Has.Count.EqualTo(1));
        Assert.That(tag.Photos.First().FileName, Is.EqualTo("sunset.jpg"));
    }

    [Test]
    public void Photos_MultiplePhotos_CanBeAdded()
    {
        // Arrange
        var tag = new Tag();
        var photo1 = new Photo { PhotoId = Guid.NewGuid(), FileName = "photo1.jpg" };
        var photo2 = new Photo { PhotoId = Guid.NewGuid(), FileName = "photo2.jpg" };
        var photo3 = new Photo { PhotoId = Guid.NewGuid(), FileName = "photo3.jpg" };

        // Act
        tag.Photos.Add(photo1);
        tag.Photos.Add(photo2);
        tag.Photos.Add(photo3);

        // Assert
        Assert.That(tag.Photos, Has.Count.EqualTo(3));
    }

    [Test]
    public void Tag_WithAllProperties_CanBeCreatedAndRetrieved()
    {
        // Arrange
        var tagId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Family";
        var createdAt = DateTime.UtcNow;

        // Act
        var tag = new Tag
        {
            TagId = tagId,
            UserId = userId,
            Name = name,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(tag.TagId, Is.EqualTo(tagId));
            Assert.That(tag.UserId, Is.EqualTo(userId));
            Assert.That(tag.Name, Is.EqualTo(name));
            Assert.That(tag.CreatedAt, Is.EqualTo(createdAt));
        });
    }

    [Test]
    public void Tag_ForDifferentCategories_CanBeCreated()
    {
        // Arrange & Act
        var vacationTag = new Tag { Name = "Vacation" };
        var familyTag = new Tag { Name = "Family" };
        var birthdayTag = new Tag { Name = "Birthday" };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(vacationTag.Name, Is.EqualTo("Vacation"));
            Assert.That(familyTag.Name, Is.EqualTo("Family"));
            Assert.That(birthdayTag.Name, Is.EqualTo("Birthday"));
        });
    }
}
