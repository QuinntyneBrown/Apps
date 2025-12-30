// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DocumentVaultOrganizer.Infrastructure.Tests;

/// <summary>
/// Unit tests for the DocumentVaultOrganizerContext.
/// </summary>
[TestFixture]
public class DocumentVaultOrganizerContextTests
{
    private DocumentVaultOrganizerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<DocumentVaultOrganizerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new DocumentVaultOrganizerContext(options);
    }

    /// <summary>
    /// Tears down the test context.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    /// <summary>
    /// Tests that Documents can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Documents_CanAddAndRetrieve()
    {
        // Arrange
        var document = new Document
        {
            DocumentId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Document",
            Category = DocumentCategoryEnum.Personal,
            FileUrl = "https://example.com/test.pdf",
            ExpirationDate = DateTime.UtcNow.AddYears(1),
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Documents.Add(document);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Documents.FindAsync(document.DocumentId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Document"));
        Assert.That(retrieved.Category, Is.EqualTo(DocumentCategoryEnum.Personal));
    }

    /// <summary>
    /// Tests that DocumentCategories can be added and retrieved.
    /// </summary>
    [Test]
    public async Task DocumentCategories_CanAddAndRetrieve()
    {
        // Arrange
        var category = new DocumentCategory
        {
            DocumentCategoryId = Guid.NewGuid(),
            Name = "Test Category",
            Description = "A test category for documents",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.DocumentCategories.Add(category);
        await _context.SaveChangesAsync();

        var retrieved = await _context.DocumentCategories.FindAsync(category.DocumentCategoryId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Category"));
        Assert.That(retrieved.Description, Is.EqualTo("A test category for documents"));
    }

    /// <summary>
    /// Tests that ExpirationAlerts can be added and retrieved.
    /// </summary>
    [Test]
    public async Task ExpirationAlerts_CanAddAndRetrieve()
    {
        // Arrange
        var document = new Document
        {
            DocumentId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Document",
            Category = DocumentCategoryEnum.Legal,
            ExpirationDate = DateTime.UtcNow.AddDays(30),
            CreatedAt = DateTime.UtcNow,
        };

        var alert = new ExpirationAlert
        {
            ExpirationAlertId = Guid.NewGuid(),
            DocumentId = document.DocumentId,
            AlertDate = DateTime.UtcNow.AddDays(7),
            IsAcknowledged = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Documents.Add(document);
        _context.ExpirationAlerts.Add(alert);
        await _context.SaveChangesAsync();

        var retrieved = await _context.ExpirationAlerts.FindAsync(alert.ExpirationAlertId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.DocumentId, Is.EqualTo(document.DocumentId));
        Assert.That(retrieved.IsAcknowledged, Is.False);
    }

    /// <summary>
    /// Tests that Documents can be updated.
    /// </summary>
    [Test]
    public async Task Documents_CanUpdate()
    {
        // Arrange
        var document = new Document
        {
            DocumentId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Original Name",
            Category = DocumentCategoryEnum.Financial,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Documents.Add(document);
        await _context.SaveChangesAsync();

        // Act
        document.Name = "Updated Name";
        document.FileUrl = "https://example.com/updated.pdf";
        await _context.SaveChangesAsync();

        var retrieved = await _context.Documents.FindAsync(document.DocumentId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Updated Name"));
        Assert.That(retrieved.FileUrl, Is.EqualTo("https://example.com/updated.pdf"));
    }

    /// <summary>
    /// Tests that Documents can be deleted.
    /// </summary>
    [Test]
    public async Task Documents_CanDelete()
    {
        // Arrange
        var document = new Document
        {
            DocumentId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Document to Delete",
            Category = DocumentCategoryEnum.Tax,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Documents.Add(document);
        await _context.SaveChangesAsync();

        // Act
        _context.Documents.Remove(document);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Documents.FindAsync(document.DocumentId);

        // Assert
        Assert.That(retrieved, Is.Null);
    }

    /// <summary>
    /// Tests that ExpirationAlerts can be acknowledged.
    /// </summary>
    [Test]
    public async Task ExpirationAlerts_CanAcknowledge()
    {
        // Arrange
        var document = new Document
        {
            DocumentId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Document",
            Category = DocumentCategoryEnum.Insurance,
            ExpirationDate = DateTime.UtcNow.AddDays(30),
            CreatedAt = DateTime.UtcNow,
        };

        var alert = new ExpirationAlert
        {
            ExpirationAlertId = Guid.NewGuid(),
            DocumentId = document.DocumentId,
            AlertDate = DateTime.UtcNow.AddDays(7),
            IsAcknowledged = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Documents.Add(document);
        _context.ExpirationAlerts.Add(alert);
        await _context.SaveChangesAsync();

        // Act
        alert.IsAcknowledged = true;
        await _context.SaveChangesAsync();

        var retrieved = await _context.ExpirationAlerts.FindAsync(alert.ExpirationAlertId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.IsAcknowledged, Is.True);
    }
}
