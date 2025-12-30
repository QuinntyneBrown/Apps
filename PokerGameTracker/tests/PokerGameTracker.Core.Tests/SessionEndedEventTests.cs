// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PokerGameTracker.Core.Tests;

public class SessionEndedEventTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesEvent()
    {
        // Arrange & Act
        var eventRecord = new SessionEndedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.SessionId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.CashOut, Is.EqualTo(0m));
            Assert.That(eventRecord.Profit, Is.EqualTo(0m));
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
        var eventRecord = new SessionEndedEvent
        {
            SessionId = sessionId,
            UserId = userId,
            CashOut = 150m,
            Profit = 50m,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.SessionId, Is.EqualTo(sessionId));
            Assert.That(eventRecord.UserId, Is.EqualTo(userId));
            Assert.That(eventRecord.CashOut, Is.EqualTo(150m));
            Assert.That(eventRecord.Profit, Is.EqualTo(50m));
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

        var event1 = new SessionEndedEvent
        {
            SessionId = sessionId,
            UserId = userId,
            CashOut = 150m,
            Profit = 50m,
            Timestamp = timestamp
        };

        var event2 = new SessionEndedEvent
        {
            SessionId = sessionId,
            UserId = userId,
            CashOut = 150m,
            Profit = 50m,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Equality_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new SessionEndedEvent
        {
            SessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CashOut = 150m,
            Profit = 50m
        };

        var event2 = new SessionEndedEvent
        {
            SessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CashOut = 100m,
            Profit = -50m
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
