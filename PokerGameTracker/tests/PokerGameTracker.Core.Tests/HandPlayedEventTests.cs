// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PokerGameTracker.Core.Tests;

public class HandPlayedEventTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesEvent()
    {
        // Arrange & Act
        var eventRecord = new HandPlayedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.HandId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.SessionId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.WasWon, Is.False);
            Assert.That(eventRecord.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_WithProperties_SetsValuesCorrectly()
    {
        // Arrange
        var handId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var sessionId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var eventRecord = new HandPlayedEvent
        {
            HandId = handId,
            UserId = userId,
            SessionId = sessionId,
            WasWon = true,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.HandId, Is.EqualTo(handId));
            Assert.That(eventRecord.UserId, Is.EqualTo(userId));
            Assert.That(eventRecord.SessionId, Is.EqualTo(sessionId));
            Assert.That(eventRecord.WasWon, Is.True);
            Assert.That(eventRecord.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Equality_SameValues_AreEqual()
    {
        // Arrange
        var handId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var sessionId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new HandPlayedEvent
        {
            HandId = handId,
            UserId = userId,
            SessionId = sessionId,
            WasWon = true,
            Timestamp = timestamp
        };

        var event2 = new HandPlayedEvent
        {
            HandId = handId,
            UserId = userId,
            SessionId = sessionId,
            WasWon = true,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Equality_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new HandPlayedEvent
        {
            HandId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            SessionId = Guid.NewGuid(),
            WasWon = true
        };

        var event2 = new HandPlayedEvent
        {
            HandId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            SessionId = Guid.NewGuid(),
            WasWon = false
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
