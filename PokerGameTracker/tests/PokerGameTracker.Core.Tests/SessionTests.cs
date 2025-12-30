// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PokerGameTracker.Core.Tests;

public class SessionTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesSession()
    {
        // Arrange & Act
        var session = new Session();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(session.SessionId, Is.EqualTo(Guid.Empty));
            Assert.That(session.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(session.GameType, Is.EqualTo(GameType.TexasHoldem));
            Assert.That(session.StartTime, Is.Not.EqualTo(default(DateTime)));
            Assert.That(session.EndTime, Is.Null);
            Assert.That(session.BuyIn, Is.EqualTo(0m));
            Assert.That(session.CashOut, Is.Null);
            Assert.That(session.Location, Is.Null);
            Assert.That(session.Notes, Is.Null);
            Assert.That(session.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(session.Hands, Is.Not.Null);
            Assert.That(session.Hands, Is.Empty);
        });
    }

    [Test]
    public void Constructor_WithProperties_SetsValuesCorrectly()
    {
        // Arrange
        var sessionId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var startTime = DateTime.UtcNow;
        var endTime = DateTime.UtcNow.AddHours(3);

        // Act
        var session = new Session
        {
            SessionId = sessionId,
            UserId = userId,
            GameType = GameType.TexasHoldem,
            StartTime = startTime,
            EndTime = endTime,
            BuyIn = 100m,
            CashOut = 150m,
            Location = "Casino XYZ",
            Notes = "Great game"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(session.SessionId, Is.EqualTo(sessionId));
            Assert.That(session.UserId, Is.EqualTo(userId));
            Assert.That(session.GameType, Is.EqualTo(GameType.TexasHoldem));
            Assert.That(session.StartTime, Is.EqualTo(startTime));
            Assert.That(session.EndTime, Is.EqualTo(endTime));
            Assert.That(session.BuyIn, Is.EqualTo(100m));
            Assert.That(session.CashOut, Is.EqualTo(150m));
            Assert.That(session.Location, Is.EqualTo("Casino XYZ"));
            Assert.That(session.Notes, Is.EqualTo("Great game"));
        });
    }

    [Test]
    public void GameType_CanBeSetToTexasHoldem()
    {
        // Arrange & Act
        var session = new Session { GameType = GameType.TexasHoldem };

        // Assert
        Assert.That(session.GameType, Is.EqualTo(GameType.TexasHoldem));
    }

    [Test]
    public void GameType_CanBeSetToTournament()
    {
        // Arrange & Act
        var session = new Session { GameType = GameType.Tournament };

        // Assert
        Assert.That(session.GameType, Is.EqualTo(GameType.Tournament));
    }

    [Test]
    public void BuyIn_AcceptsDecimalValues()
    {
        // Arrange & Act
        var session = new Session { BuyIn = 250.50m };

        // Assert
        Assert.That(session.BuyIn, Is.EqualTo(250.50m));
    }

    [Test]
    public void CashOut_CanBeNull()
    {
        // Arrange & Act
        var session = new Session { CashOut = null };

        // Assert
        Assert.That(session.CashOut, Is.Null);
    }

    [Test]
    public void CashOut_AcceptsDecimalValues()
    {
        // Arrange & Act
        var session = new Session { CashOut = 300.75m };

        // Assert
        Assert.That(session.CashOut, Is.EqualTo(300.75m));
    }

    [Test]
    public void EndTime_CanBeNull()
    {
        // Arrange & Act
        var session = new Session { EndTime = null };

        // Assert
        Assert.That(session.EndTime, Is.Null);
    }

    [Test]
    public void Location_CanBeNull()
    {
        // Arrange & Act
        var session = new Session { Location = null };

        // Assert
        Assert.That(session.Location, Is.Null);
    }

    [Test]
    public void Notes_CanBeNull()
    {
        // Arrange & Act
        var session = new Session { Notes = null };

        // Assert
        Assert.That(session.Notes, Is.Null);
    }

    [Test]
    public void Hands_Collection_CanBeModified()
    {
        // Arrange
        var session = new Session();
        var hand = new Hand
        {
            HandId = Guid.NewGuid(),
            WasWon = true
        };

        // Act
        session.Hands.Add(hand);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(session.Hands.Count, Is.EqualTo(1));
            Assert.That(session.Hands.First(), Is.EqualTo(hand));
        });
    }
}
