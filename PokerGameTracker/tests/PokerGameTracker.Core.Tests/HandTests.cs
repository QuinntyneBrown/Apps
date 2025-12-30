// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PokerGameTracker.Core.Tests;

public class HandTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesHand()
    {
        // Arrange & Act
        var hand = new Hand();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(hand.HandId, Is.EqualTo(Guid.Empty));
            Assert.That(hand.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(hand.SessionId, Is.EqualTo(Guid.Empty));
            Assert.That(hand.Session, Is.Null);
            Assert.That(hand.StartingCards, Is.Null);
            Assert.That(hand.PotSize, Is.Null);
            Assert.That(hand.WasWon, Is.False);
            Assert.That(hand.Notes, Is.Null);
            Assert.That(hand.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_WithProperties_SetsValuesCorrectly()
    {
        // Arrange
        var handId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var sessionId = Guid.NewGuid();

        // Act
        var hand = new Hand
        {
            HandId = handId,
            UserId = userId,
            SessionId = sessionId,
            StartingCards = "A♠ K♠",
            PotSize = 125.50m,
            WasWon = true,
            Notes = "Great hand"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(hand.HandId, Is.EqualTo(handId));
            Assert.That(hand.UserId, Is.EqualTo(userId));
            Assert.That(hand.SessionId, Is.EqualTo(sessionId));
            Assert.That(hand.StartingCards, Is.EqualTo("A♠ K♠"));
            Assert.That(hand.PotSize, Is.EqualTo(125.50m));
            Assert.That(hand.WasWon, Is.True);
            Assert.That(hand.Notes, Is.EqualTo("Great hand"));
        });
    }

    [Test]
    public void Session_NavigationProperty_CanBeSet()
    {
        // Arrange
        var hand = new Hand();
        var session = new Session
        {
            SessionId = Guid.NewGuid(),
            GameType = GameType.TexasHoldem
        };

        // Act
        hand.Session = session;

        // Assert
        Assert.That(hand.Session, Is.EqualTo(session));
    }

    [Test]
    public void WasWon_CanBeSetToTrue()
    {
        // Arrange & Act
        var hand = new Hand { WasWon = true };

        // Assert
        Assert.That(hand.WasWon, Is.True);
    }

    [Test]
    public void WasWon_DefaultsToFalse()
    {
        // Arrange & Act
        var hand = new Hand();

        // Assert
        Assert.That(hand.WasWon, Is.False);
    }

    [Test]
    public void StartingCards_CanBeNull()
    {
        // Arrange & Act
        var hand = new Hand { StartingCards = null };

        // Assert
        Assert.That(hand.StartingCards, Is.Null);
    }

    [Test]
    public void PotSize_CanBeNull()
    {
        // Arrange & Act
        var hand = new Hand { PotSize = null };

        // Assert
        Assert.That(hand.PotSize, Is.Null);
    }

    [Test]
    public void PotSize_AcceptsDecimalValues()
    {
        // Arrange & Act
        var hand = new Hand { PotSize = 75.25m };

        // Assert
        Assert.That(hand.PotSize, Is.EqualTo(75.25m));
    }

    [Test]
    public void Notes_CanBeNull()
    {
        // Arrange & Act
        var hand = new Hand { Notes = null };

        // Assert
        Assert.That(hand.Notes, Is.Null);
    }
}
