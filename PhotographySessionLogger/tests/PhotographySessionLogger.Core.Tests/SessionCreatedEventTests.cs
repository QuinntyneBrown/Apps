// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PhotographySessionLogger.Core.Tests;

public class SessionCreatedEventTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesEvent()
    {
        // Arrange & Act
        var eventRecord = new SessionCreatedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.SessionId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.Title, Is.EqualTo(string.Empty));
            Assert.That(eventRecord.SessionType, Is.EqualTo(SessionType.Portrait));
            Assert.That(eventRecord.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_WithProperties_SetsValuesCorrectly()
    {
        // Arrange
        var sessionId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var eventRecord = new SessionCreatedEvent
        {
            SessionId = sessionId,
            UserId = userId,
            Title = "Wedding Shoot",
            SessionType = SessionType.Wedding,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.SessionId, Is.EqualTo(sessionId));
            Assert.That(eventRecord.UserId, Is.EqualTo(userId));
            Assert.That(eventRecord.Title, Is.EqualTo("Wedding Shoot"));
            Assert.That(eventRecord.SessionType, Is.EqualTo(SessionType.Wedding));
            Assert.That(eventRecord.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Equality_SameValues_AreEqual()
    {
        // Arrange
        var sessionId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new SessionCreatedEvent
        {
            SessionId = sessionId,
            UserId = userId,
            Title = "Portrait Session",
            SessionType = SessionType.Portrait,
            Timestamp = timestamp
        };

        var event2 = new SessionCreatedEvent
        {
            SessionId = sessionId,
            UserId = userId,
            Title = "Portrait Session",
            SessionType = SessionType.Portrait,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Equality_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new SessionCreatedEvent
        {
            SessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Session 1"
        };

        var event2 = new SessionCreatedEvent
        {
            SessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Session 2"
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
