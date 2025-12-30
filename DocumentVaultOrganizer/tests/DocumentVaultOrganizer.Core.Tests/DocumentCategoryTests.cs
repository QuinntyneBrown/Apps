// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DocumentVaultOrganizer.Core.Tests;

public class DocumentCategoryTests
{
    [Test]
    public void Constructor_CreatesDocumentCategory_WithDefaultValues()
    {
        // Arrange & Act
        var category = new DocumentCategory();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(category.DocumentCategoryId, Is.EqualTo(Guid.Empty));
            Assert.That(category.Name, Is.EqualTo(string.Empty));
            Assert.That(category.Description, Is.Null);
            Assert.That(category.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void DocumentCategoryId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var category = new DocumentCategory();
        var expectedId = Guid.NewGuid();

        // Act
        category.DocumentCategoryId = expectedId;

        // Assert
        Assert.That(category.DocumentCategoryId, Is.EqualTo(expectedId));
    }

    [Test]
    public void Name_CanBeSet_AndRetrieved()
    {
        // Arrange
        var category = new DocumentCategory();
        var expectedName = "Legal Documents";

        // Act
        category.Name = expectedName;

        // Assert
        Assert.That(category.Name, Is.EqualTo(expectedName));
    }

    [Test]
    public void Description_CanBeSet_AndRetrieved()
    {
        // Arrange
        var category = new DocumentCategory();
        var expectedDescription = "All legal and official documents";

        // Act
        category.Description = expectedDescription;

        // Assert
        Assert.That(category.Description, Is.EqualTo(expectedDescription));
    }

    [Test]
    public void Description_CanBeNull()
    {
        // Arrange
        var category = new DocumentCategory();

        // Act
        category.Description = null;

        // Assert
        Assert.That(category.Description, Is.Null);
    }

    [Test]
    public void CreatedAt_DefaultsToUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var category = new DocumentCategory();
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(category.CreatedAt, Is.GreaterThanOrEqualTo(beforeCreation));
        Assert.That(category.CreatedAt, Is.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void CreatedAt_CanBeSet_ToSpecificDate()
    {
        // Arrange
        var category = new DocumentCategory();
        var expectedDate = new DateTime(2023, 1, 15, 10, 30, 0, DateTimeKind.Utc);

        // Act
        category.CreatedAt = expectedDate;

        // Assert
        Assert.That(category.CreatedAt, Is.EqualTo(expectedDate));
    }

    [Test]
    public void DocumentCategory_WithAllProperties_CanBeCreatedAndRetrieved()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var name = "Medical Records";
        var description = "Health and medical documentation";
        var createdAt = DateTime.UtcNow;

        // Act
        var category = new DocumentCategory
        {
            DocumentCategoryId = categoryId,
            Name = name,
            Description = description,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(category.DocumentCategoryId, Is.EqualTo(categoryId));
            Assert.That(category.Name, Is.EqualTo(name));
            Assert.That(category.Description, Is.EqualTo(description));
            Assert.That(category.CreatedAt, Is.EqualTo(createdAt));
        });
    }

    [Test]
    public void DocumentCategory_WithoutDescription_CanBeCreated()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var name = "Tax Documents";

        // Act
        var category = new DocumentCategory
        {
            DocumentCategoryId = categoryId,
            Name = name
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(category.DocumentCategoryId, Is.EqualTo(categoryId));
            Assert.That(category.Name, Is.EqualTo(name));
            Assert.That(category.Description, Is.Null);
        });
    }
}
