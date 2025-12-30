// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PokerGameTracker.Core.Tests;

public class SessionStartedEventTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesEvent()
    {
        // Arrange & Act
        var eventRecord = new SessionStartedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.SessionId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.GameType, Is.EqualTo(GameType.TexasHoldem));
            Assert.That(eventRecord.BuyIn, Is.EqualTo(0m));
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
        var eventRecord = new SessionStartedEvent
        {
            SessionId = sessionId,
            UserId = userId,
            GameType = GameType.TexasHoldem,
            BuyIn = 100m,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.SessionId, Is.EqualTo(sessionId));
            Assert.That(eventRecord.UserId, Is.EqualTo(userId));
            Assert.That(eventRecord.GameType, Is.EqualTo(GameType.TexasHoldem));
            Assert.That(eventRecord.BuyIn, Is.EqualTo(100m));
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

        var event1 = new SessionStartedEvent
        {
            SessionId = sessionId,
            UserId = userId,
            GameType = GameType.TexasHoldem,
            BuyIn = 100m,
            Timestamp = timestamp
        };

        var event2 = new SessionStartedEvent
        {
            SessionId = sessionId,
            UserId = userId,
            GameType = GameType.TexasHoldem,
            BuyIn = 100m,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Equality_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new SessionStartedEvent
        {
            SessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            GameType = GameType.TexasHoldem,
            BuyIn = 100m
        };

        var event2 = new SessionStartedEvent
        {
            SessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            GameType = GameType.Tournament,
            BuyIn = 200m
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
