// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DocumentVaultOrganizer.Core.Tests;

public class ExpirationAlertTests
{
    [Test]
    public void Constructor_CreatesExpirationAlert_WithDefaultValues()
    {
        // Arrange & Act
        var alert = new ExpirationAlert();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(alert.ExpirationAlertId, Is.EqualTo(Guid.Empty));
            Assert.That(alert.DocumentId, Is.EqualTo(Guid.Empty));
            Assert.That(alert.AlertDate, Is.EqualTo(default(DateTime)));
            Assert.That(alert.IsAcknowledged, Is.False);
            Assert.That(alert.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void ExpirationAlertId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var alert = new ExpirationAlert();
        var expectedId = Guid.NewGuid();

        // Act
        alert.ExpirationAlertId = expectedId;

        // Assert
        Assert.That(alert.ExpirationAlertId, Is.EqualTo(expectedId));
    }

    [Test]
    public void DocumentId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var alert = new ExpirationAlert();
        var expectedDocumentId = Guid.NewGuid();

        // Act
        alert.DocumentId = expectedDocumentId;

        // Assert
        Assert.That(alert.DocumentId, Is.EqualTo(expectedDocumentId));
    }

    [Test]
    public void AlertDate_CanBeSet_AndRetrieved()
    {
        // Arrange
        var alert = new ExpirationAlert();
        var expectedDate = DateTime.UtcNow.AddDays(30);

        // Act
        alert.AlertDate = expectedDate;

        // Assert
        Assert.That(alert.AlertDate, Is.EqualTo(expectedDate));
    }

    [Test]
    public void IsAcknowledged_DefaultsToFalse()
    {
        // Arrange & Act
        var alert = new ExpirationAlert();

        // Assert
        Assert.That(alert.IsAcknowledged, Is.False);
    }

    [Test]
    public void IsAcknowledged_CanBeSet_ToTrue()
    {
        // Arrange
        var alert = new ExpirationAlert();

        // Act
        alert.IsAcknowledged = true;

        // Assert
        Assert.That(alert.IsAcknowledged, Is.True);
    }

    [Test]
    public void IsAcknowledged_CanBeToggled()
    {
        // Arrange
        var alert = new ExpirationAlert { IsAcknowledged = false };

        // Act
        alert.IsAcknowledged = true;
        var firstState = alert.IsAcknowledged;
        alert.IsAcknowledged = false;
        var secondState = alert.IsAcknowledged;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(firstState, Is.True);
            Assert.That(secondState, Is.False);
        });
    }

    [Test]
    public void CreatedAt_DefaultsToUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var alert = new ExpirationAlert();
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(alert.CreatedAt, Is.GreaterThanOrEqualTo(beforeCreation));
        Assert.That(alert.CreatedAt, Is.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void CreatedAt_CanBeSet_ToSpecificDate()
    {
        // Arrange
        var alert = new ExpirationAlert();
        var expectedDate = new DateTime(2023, 1, 15, 10, 30, 0, DateTimeKind.Utc);

        // Act
        alert.CreatedAt = expectedDate;

        // Assert
        Assert.That(alert.CreatedAt, Is.EqualTo(expectedDate));
    }

    [Test]
    public void ExpirationAlert_WithAllProperties_CanBeCreatedAndRetrieved()
    {
        // Arrange
        var alertId = Guid.NewGuid();
        var documentId = Guid.NewGuid();
        var alertDate = DateTime.UtcNow.AddDays(7);
        var isAcknowledged = true;
        var createdAt = DateTime.UtcNow;

        // Act
        var alert = new ExpirationAlert
        {
            ExpirationAlertId = alertId,
            DocumentId = documentId,
            AlertDate = alertDate,
            IsAcknowledged = isAcknowledged,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(alert.ExpirationAlertId, Is.EqualTo(alertId));
            Assert.That(alert.DocumentId, Is.EqualTo(documentId));
            Assert.That(alert.AlertDate, Is.EqualTo(alertDate));
            Assert.That(alert.IsAcknowledged, Is.EqualTo(isAcknowledged));
            Assert.That(alert.CreatedAt, Is.EqualTo(createdAt));
        });
    }

    [Test]
    public void ExpirationAlert_ForUpcomingExpiration_CanBeCreated()
    {
        // Arrange
        var documentId = Guid.NewGuid();
        var alertDate = DateTime.UtcNow.AddDays(14);

        // Act
        var alert = new ExpirationAlert
        {
            ExpirationAlertId = Guid.NewGuid(),
            DocumentId = documentId,
            AlertDate = alertDate,
            IsAcknowledged = false
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(alert.DocumentId, Is.EqualTo(documentId));
            Assert.That(alert.AlertDate, Is.EqualTo(alertDate));
            Assert.That(alert.IsAcknowledged, Is.False);
        });
    }
}
