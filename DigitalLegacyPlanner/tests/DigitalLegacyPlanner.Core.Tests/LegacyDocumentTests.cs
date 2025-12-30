namespace DigitalLegacyPlanner.Core.Tests;

public class LegacyDocumentTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesLegacyDocument()
    {
        // Arrange
        var documentId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "Last Will and Testament";
        var documentType = "Will";
        var filePath = "/documents/will.pdf";
        var description = "Legal will document";
        var physicalLocation = "Safe deposit box";
        var accessGrantedTo = "Executor";
        var isEncrypted = true;

        // Act
        var document = new LegacyDocument
        {
            LegacyDocumentId = documentId,
            UserId = userId,
            Title = title,
            DocumentType = documentType,
            FilePath = filePath,
            Description = description,
            PhysicalLocation = physicalLocation,
            AccessGrantedTo = accessGrantedTo,
            IsEncrypted = isEncrypted
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(document.LegacyDocumentId, Is.EqualTo(documentId));
            Assert.That(document.UserId, Is.EqualTo(userId));
            Assert.That(document.Title, Is.EqualTo(title));
            Assert.That(document.DocumentType, Is.EqualTo(documentType));
            Assert.That(document.FilePath, Is.EqualTo(filePath));
            Assert.That(document.Description, Is.EqualTo(description));
            Assert.That(document.PhysicalLocation, Is.EqualTo(physicalLocation));
            Assert.That(document.AccessGrantedTo, Is.EqualTo(accessGrantedTo));
            Assert.That(document.IsEncrypted, Is.EqualTo(isEncrypted));
        });
    }

    [Test]
    public void DefaultValues_AreSetCorrectly()
    {
        // Arrange & Act
        var document = new LegacyDocument();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(document.Title, Is.EqualTo(string.Empty));
            Assert.That(document.DocumentType, Is.EqualTo(string.Empty));
            Assert.That(document.FilePath, Is.Null);
            Assert.That(document.Description, Is.Null);
            Assert.That(document.PhysicalLocation, Is.Null);
            Assert.That(document.AccessGrantedTo, Is.Null);
            Assert.That(document.IsEncrypted, Is.False);
            Assert.That(document.LastReviewedAt, Is.Null);
            Assert.That(document.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void MarkAsReviewed_UpdatesLastReviewedAt()
    {
        // Arrange
        var document = new LegacyDocument
        {
            LegacyDocumentId = Guid.NewGuid()
        };
        var beforeReview = DateTime.UtcNow.AddSeconds(-1);

        // Act
        document.MarkAsReviewed();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(document.LastReviewedAt, Is.Not.Null);
            Assert.That(document.LastReviewedAt!.Value, Is.GreaterThan(beforeReview));
        });
    }

    [Test]
    public void MarkAsReviewed_CalledMultipleTimes_UpdatesTimestamp()
    {
        // Arrange
        var document = new LegacyDocument
        {
            LegacyDocumentId = Guid.NewGuid()
        };

        document.MarkAsReviewed();
        var firstReview = document.LastReviewedAt;

        System.Threading.Thread.Sleep(10);

        // Act
        document.MarkAsReviewed();

        // Assert
        Assert.That(document.LastReviewedAt, Is.GreaterThanOrEqualTo(firstReview));
    }

    [Test]
    public void NeedsReview_NeverReviewed_ReturnsTrue()
    {
        // Arrange
        var document = new LegacyDocument
        {
            LegacyDocumentId = Guid.NewGuid(),
            LastReviewedAt = null
        };

        // Act
        var needsReview = document.NeedsReview();

        // Assert
        Assert.That(needsReview, Is.True);
    }

    [Test]
    public void NeedsReview_ReviewedRecently_ReturnsFalse()
    {
        // Arrange
        var document = new LegacyDocument
        {
            LegacyDocumentId = Guid.NewGuid(),
            LastReviewedAt = DateTime.UtcNow.AddDays(-30)
        };

        // Act
        var needsReview = document.NeedsReview();

        // Assert
        Assert.That(needsReview, Is.False);
    }

    [Test]
    public void NeedsReview_ReviewedOver180DaysAgo_ReturnsTrue()
    {
        // Arrange
        var document = new LegacyDocument
        {
            LegacyDocumentId = Guid.NewGuid(),
            LastReviewedAt = DateTime.UtcNow.AddDays(-181)
        };

        // Act
        var needsReview = document.NeedsReview();

        // Assert
        Assert.That(needsReview, Is.True);
    }

    [Test]
    public void NeedsReview_ReviewedExactly180DaysAgo_ReturnsFalse()
    {
        // Arrange
        var document = new LegacyDocument
        {
            LegacyDocumentId = Guid.NewGuid(),
            LastReviewedAt = DateTime.UtcNow.AddDays(-180)
        };

        // Act
        var needsReview = document.NeedsReview();

        // Assert
        Assert.That(needsReview, Is.False);
    }

    [Test]
    public void NeedsReview_ReviewedToday_ReturnsFalse()
    {
        // Arrange
        var document = new LegacyDocument
        {
            LegacyDocumentId = Guid.NewGuid()
        };
        document.MarkAsReviewed();

        // Act
        var needsReview = document.NeedsReview();

        // Assert
        Assert.That(needsReview, Is.False);
    }

    [Test]
    public void IsEncrypted_CanBeSetToTrue()
    {
        // Arrange & Act
        var document = new LegacyDocument
        {
            IsEncrypted = true
        };

        // Assert
        Assert.That(document.IsEncrypted, Is.True);
    }

    [Test]
    public void IsEncrypted_CanBeSetToFalse()
    {
        // Arrange & Act
        var document = new LegacyDocument
        {
            IsEncrypted = false
        };

        // Assert
        Assert.That(document.IsEncrypted, Is.False);
    }

    [Test]
    public void FilePath_CanBeSet()
    {
        // Arrange
        var filePath = "/secure/documents/trust.pdf";
        var document = new LegacyDocument();

        // Act
        document.FilePath = filePath;

        // Assert
        Assert.That(document.FilePath, Is.EqualTo(filePath));
    }

    [Test]
    public void Description_CanBeSet()
    {
        // Arrange
        var description = "Important legal document";
        var document = new LegacyDocument();

        // Act
        document.Description = description;

        // Assert
        Assert.That(document.Description, Is.EqualTo(description));
    }

    [Test]
    public void PhysicalLocation_CanBeSet()
    {
        // Arrange
        var location = "Bank vault, box #123";
        var document = new LegacyDocument();

        // Act
        document.PhysicalLocation = location;

        // Assert
        Assert.That(document.PhysicalLocation, Is.EqualTo(location));
    }

    [Test]
    public void AccessGrantedTo_CanBeSet()
    {
        // Arrange
        var accessGrantedTo = "Spouse and Attorney";
        var document = new LegacyDocument();

        // Act
        document.AccessGrantedTo = accessGrantedTo;

        // Assert
        Assert.That(document.AccessGrantedTo, Is.EqualTo(accessGrantedTo));
    }

    [Test]
    public void CreatedAt_IsSetOnCreation()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var document = new LegacyDocument();

        // Assert
        Assert.That(document.CreatedAt, Is.GreaterThan(beforeCreation));
    }
}
