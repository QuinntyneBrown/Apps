// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DocumentVaultOrganizer.Core.Tests;

public class DocumentUploadedEventTests
{
    [Test]
    public void Constructor_CreatesEvent_WithAllProperties()
    {
        // Arrange
        var documentId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Contract.pdf";
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new DocumentUploadedEvent
        {
            DocumentId = documentId,
            UserId = userId,
            Name = name,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.DocumentId, Is.EqualTo(documentId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Event_WithDefaultTimestamp_SetsUtcNow()
    {
        // Arrange
        var documentId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Report.pdf";

        // Act
        var evt = new DocumentUploadedEvent
        {
            DocumentId = documentId,
            UserId = userId,
            Name = name
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.DocumentId, Is.EqualTo(documentId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Event_IsRecord_AndSupportsValueEquality()
    {
        // Arrange
        var documentId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Document.pdf";
        var timestamp = DateTime.UtcNow;

        // Act
        var event1 = new DocumentUploadedEvent
        {
            DocumentId = documentId,
            UserId = userId,
            Name = name,
            Timestamp = timestamp
        };

        var event2 = new DocumentUploadedEvent
        {
            DocumentId = documentId,
            UserId = userId,
            Name = name,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Event_WithDifferentProperties_AreNotEqual()
    {
        // Arrange
        var documentId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new DocumentUploadedEvent
        {
            DocumentId = documentId,
            UserId = userId,
            Name = "File1.pdf",
            Timestamp = timestamp
        };

        var event2 = new DocumentUploadedEvent
        {
            DocumentId = documentId,
            UserId = userId,
            Name = "File2.pdf",
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }

    [Test]
    public void Event_WithEmptyName_CanBeCreated()
    {
        // Arrange
        var documentId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        // Act
        var evt = new DocumentUploadedEvent
        {
            DocumentId = documentId,
            UserId = userId,
            Name = string.Empty
        };

        // Assert
        Assert.That(evt.Name, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Event_Properties_AreInitOnly()
    {
        // Arrange
        var documentId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Test.pdf";

        // Act
        var evt = new DocumentUploadedEvent
        {
            DocumentId = documentId,
            UserId = userId,
            Name = name
        };

        // Assert - Properties are init-only, so they can only be set during initialization
        Assert.Multiple(() =>
        {
            Assert.That(evt.DocumentId, Is.EqualTo(documentId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Name, Is.EqualTo(name));
        });
    }

    [Test]
    public void Event_ToString_ContainsTypeName()
    {
        // Arrange
        var evt = new DocumentUploadedEvent
        {
            DocumentId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test.pdf"
        };

        // Act
        var result = evt.ToString();

        // Assert
        Assert.That(result, Does.Contain("DocumentUploadedEvent"));
    }
}
