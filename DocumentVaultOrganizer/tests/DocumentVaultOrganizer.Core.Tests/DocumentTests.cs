// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DocumentVaultOrganizer.Core.Tests;

public class DocumentTests
{
    [Test]
    public void Constructor_CreatesDocument_WithDefaultValues()
    {
        // Arrange & Act
        var document = new Document();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(document.DocumentId, Is.EqualTo(Guid.Empty));
            Assert.That(document.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(document.Name, Is.EqualTo(string.Empty));
            Assert.That(document.Category, Is.EqualTo(DocumentCategoryEnum.Personal));
            Assert.That(document.FileUrl, Is.Null);
            Assert.That(document.ExpirationDate, Is.Null);
            Assert.That(document.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void DocumentId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var document = new Document();
        var expectedId = Guid.NewGuid();

        // Act
        document.DocumentId = expectedId;

        // Assert
        Assert.That(document.DocumentId, Is.EqualTo(expectedId));
    }

    [Test]
    public void UserId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var document = new Document();
        var expectedUserId = Guid.NewGuid();

        // Act
        document.UserId = expectedUserId;

        // Assert
        Assert.That(document.UserId, Is.EqualTo(expectedUserId));
    }

    [Test]
    public void Name_CanBeSet_AndRetrieved()
    {
        // Arrange
        var document = new Document();
        var expectedName = "Tax Return 2023.pdf";

        // Act
        document.Name = expectedName;

        // Assert
        Assert.That(document.Name, Is.EqualTo(expectedName));
    }

    [Test]
    public void Category_CanBeSet_ToFinancial()
    {
        // Arrange
        var document = new Document();

        // Act
        document.Category = DocumentCategoryEnum.Financial;

        // Assert
        Assert.That(document.Category, Is.EqualTo(DocumentCategoryEnum.Financial));
    }

    [Test]
    public void Category_CanBeSet_ToLegal()
    {
        // Arrange
        var document = new Document();

        // Act
        document.Category = DocumentCategoryEnum.Legal;

        // Assert
        Assert.That(document.Category, Is.EqualTo(DocumentCategoryEnum.Legal));
    }

    [Test]
    public void Category_CanBeSet_ToMedical()
    {
        // Arrange
        var document = new Document();

        // Act
        document.Category = DocumentCategoryEnum.Medical;

        // Assert
        Assert.That(document.Category, Is.EqualTo(DocumentCategoryEnum.Medical));
    }

    [Test]
    public void FileUrl_CanBeSet_AndRetrieved()
    {
        // Arrange
        var document = new Document();
        var expectedUrl = "https://storage.example.com/documents/file.pdf";

        // Act
        document.FileUrl = expectedUrl;

        // Assert
        Assert.That(document.FileUrl, Is.EqualTo(expectedUrl));
    }

    [Test]
    public void ExpirationDate_CanBeSet_AndRetrieved()
    {
        // Arrange
        var document = new Document();
        var expectedDate = DateTime.UtcNow.AddMonths(6);

        // Act
        document.ExpirationDate = expectedDate;

        // Assert
        Assert.That(document.ExpirationDate, Is.EqualTo(expectedDate));
    }

    [Test]
    public void ExpirationDate_CanBeNull()
    {
        // Arrange
        var document = new Document();

        // Act
        document.ExpirationDate = null;

        // Assert
        Assert.That(document.ExpirationDate, Is.Null);
    }

    [Test]
    public void CreatedAt_DefaultsToUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var document = new Document();
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(document.CreatedAt, Is.GreaterThanOrEqualTo(beforeCreation));
        Assert.That(document.CreatedAt, Is.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void CreatedAt_CanBeSet_ToSpecificDate()
    {
        // Arrange
        var document = new Document();
        var expectedDate = new DateTime(2023, 1, 15, 10, 30, 0, DateTimeKind.Utc);

        // Act
        document.CreatedAt = expectedDate;

        // Assert
        Assert.That(document.CreatedAt, Is.EqualTo(expectedDate));
    }

    [Test]
    public void Document_WithAllProperties_CanBeCreatedAndRetrieved()
    {
        // Arrange
        var documentId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Passport.pdf";
        var category = DocumentCategoryEnum.Legal;
        var fileUrl = "https://storage.example.com/passport.pdf";
        var expirationDate = DateTime.UtcNow.AddYears(10);
        var createdAt = DateTime.UtcNow;

        // Act
        var document = new Document
        {
            DocumentId = documentId,
            UserId = userId,
            Name = name,
            Category = category,
            FileUrl = fileUrl,
            ExpirationDate = expirationDate,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(document.DocumentId, Is.EqualTo(documentId));
            Assert.That(document.UserId, Is.EqualTo(userId));
            Assert.That(document.Name, Is.EqualTo(name));
            Assert.That(document.Category, Is.EqualTo(category));
            Assert.That(document.FileUrl, Is.EqualTo(fileUrl));
            Assert.That(document.ExpirationDate, Is.EqualTo(expirationDate));
            Assert.That(document.CreatedAt, Is.EqualTo(createdAt));
        });
    }
}
