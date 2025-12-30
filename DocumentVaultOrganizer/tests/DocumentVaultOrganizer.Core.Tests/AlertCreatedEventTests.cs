// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DocumentVaultOrganizer.Core.Tests;

public class AlertCreatedEventTests
{
    [Test]
    public void Constructor_CreatesEvent_WithAllProperties()
    {
        // Arrange
        var alertId = Guid.NewGuid();
        var documentId = Guid.NewGuid();
        var alertDate = DateTime.UtcNow.AddDays(7);
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new AlertCreatedEvent
        {
            ExpirationAlertId = alertId,
            DocumentId = documentId,
            AlertDate = alertDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ExpirationAlertId, Is.EqualTo(alertId));
            Assert.That(evt.DocumentId, Is.EqualTo(documentId));
            Assert.That(evt.AlertDate, Is.EqualTo(alertDate));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Event_WithDefaultTimestamp_SetsUtcNow()
    {
        // Arrange
        var alertId = Guid.NewGuid();
        var documentId = Guid.NewGuid();
        var alertDate = DateTime.UtcNow.AddDays(30);

        // Act
        var evt = new AlertCreatedEvent
        {
            ExpirationAlertId = alertId,
            DocumentId = documentId,
            AlertDate = alertDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ExpirationAlertId, Is.EqualTo(alertId));
            Assert.That(evt.DocumentId, Is.EqualTo(documentId));
            Assert.That(evt.AlertDate, Is.EqualTo(alertDate));
            Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Event_IsRecord_AndSupportsValueEquality()
    {
        // Arrange
        var alertId = Guid.NewGuid();
        var documentId = Guid.NewGuid();
        var alertDate = DateTime.UtcNow.AddDays(14);
        var timestamp = DateTime.UtcNow;

        // Act
        var event1 = new AlertCreatedEvent
        {
            ExpirationAlertId = alertId,
            DocumentId = documentId,
            AlertDate = alertDate,
            Timestamp = timestamp
        };

        var event2 = new AlertCreatedEvent
        {
            ExpirationAlertId = alertId,
            DocumentId = documentId,
            AlertDate = alertDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Event_WithDifferentProperties_AreNotEqual()
    {
        // Arrange
        var alertId = Guid.NewGuid();
        var documentId1 = Guid.NewGuid();
        var documentId2 = Guid.NewGuid();
        var alertDate = DateTime.UtcNow.AddDays(7);
        var timestamp = DateTime.UtcNow;

        var event1 = new AlertCreatedEvent
        {
            ExpirationAlertId = alertId,
            DocumentId = documentId1,
            AlertDate = alertDate,
            Timestamp = timestamp
        };

        var event2 = new AlertCreatedEvent
        {
            ExpirationAlertId = alertId,
            DocumentId = documentId2,
            AlertDate = alertDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }

    [Test]
    public void Event_WithPastAlertDate_CanBeCreated()
    {
        // Arrange
        var alertId = Guid.NewGuid();
        var documentId = Guid.NewGuid();
        var alertDate = DateTime.UtcNow.AddDays(-1);

        // Act
        var evt = new AlertCreatedEvent
        {
            ExpirationAlertId = alertId,
            DocumentId = documentId,
            AlertDate = alertDate
        };

        // Assert
        Assert.That(evt.AlertDate, Is.EqualTo(alertDate));
    }

    [Test]
    public void Event_WithFutureAlertDate_CanBeCreated()
    {
        // Arrange
        var alertId = Guid.NewGuid();
        var documentId = Guid.NewGuid();
        var alertDate = DateTime.UtcNow.AddMonths(6);

        // Act
        var evt = new AlertCreatedEvent
        {
            ExpirationAlertId = alertId,
            DocumentId = documentId,
            AlertDate = alertDate
        };

        // Assert
        Assert.That(evt.AlertDate, Is.EqualTo(alertDate));
    }

    [Test]
    public void Event_Properties_AreInitOnly()
    {
        // Arrange
        var alertId = Guid.NewGuid();
        var documentId = Guid.NewGuid();
        var alertDate = DateTime.UtcNow.AddDays(10);

        // Act
        var evt = new AlertCreatedEvent
        {
            ExpirationAlertId = alertId,
            DocumentId = documentId,
            AlertDate = alertDate
        };

        // Assert - Properties are init-only, so they can only be set during initialization
        Assert.Multiple(() =>
        {
            Assert.That(evt.ExpirationAlertId, Is.EqualTo(alertId));
            Assert.That(evt.DocumentId, Is.EqualTo(documentId));
            Assert.That(evt.AlertDate, Is.EqualTo(alertDate));
        });
    }

    [Test]
    public void Event_ToString_ContainsTypeName()
    {
        // Arrange
        var evt = new AlertCreatedEvent
        {
            ExpirationAlertId = Guid.NewGuid(),
            DocumentId = Guid.NewGuid(),
            AlertDate = DateTime.UtcNow.AddDays(5)
        };

        // Act
        var result = evt.ToString();

        // Assert
        Assert.That(result, Does.Contain("AlertCreatedEvent"));
    }
}
