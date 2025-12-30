// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VideoGameCollectionManager.Core.Tests;

public class PlaySessionTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesPlaySession()
    {
        // Arrange
        var playSessionId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var gameId = Guid.NewGuid();
        var startTime = new DateTime(2024, 3, 15, 18, 0, 0, DateTimeKind.Utc);
        var endTime = new DateTime(2024, 3, 15, 20, 30, 0, DateTimeKind.Utc);
        var durationMinutes = 150;
        var notes = "Great gaming session";

        // Act
        var session = new PlaySession
        {
            PlaySessionId = playSessionId,
            UserId = userId,
            GameId = gameId,
            StartTime = startTime,
            EndTime = endTime,
            DurationMinutes = durationMinutes,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(session.PlaySessionId, Is.EqualTo(playSessionId));
            Assert.That(session.UserId, Is.EqualTo(userId));
            Assert.That(session.GameId, Is.EqualTo(gameId));
            Assert.That(session.StartTime, Is.EqualTo(startTime));
            Assert.That(session.EndTime, Is.EqualTo(endTime));
            Assert.That(session.DurationMinutes, Is.EqualTo(durationMinutes));
            Assert.That(session.Notes, Is.EqualTo(notes));
        });
    }

    [Test]
    public void StartTime_DefaultsToUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var session = new PlaySession
        {
            PlaySessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            GameId = Guid.NewGuid()
        };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(session.StartTime, Is.GreaterThanOrEqualTo(beforeCreation));
            Assert.That(session.StartTime, Is.LessThanOrEqualTo(afterCreation));
        });
    }

    [Test]
    public void CreatedAt_DefaultsToUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var session = new PlaySession
        {
            PlaySessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            GameId = Guid.NewGuid()
        };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(session.CreatedAt, Is.GreaterThanOrEqualTo(beforeCreation));
            Assert.That(session.CreatedAt, Is.LessThanOrEqualTo(afterCreation));
        });
    }

    [Test]
    public void PlaySession_OptionalFields_CanBeNull()
    {
        // Arrange & Act
        var session = new PlaySession
        {
            PlaySessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            GameId = Guid.NewGuid()
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(session.EndTime, Is.Null);
            Assert.That(session.DurationMinutes, Is.Null);
            Assert.That(session.Notes, Is.Null);
        });
    }

    [Test]
    public void Game_NavigationProperty_CanBeSet()
    {
        // Arrange
        var game = new Game
        {
            GameId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Game",
            Platform = Platform.PC,
            Genre = Genre.Action,
            Status = CompletionStatus.InProgress
        };

        var session = new PlaySession
        {
            PlaySessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            GameId = game.GameId
        };

        // Act
        session.Game = game;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(session.Game, Is.Not.Null);
            Assert.That(session.Game.GameId, Is.EqualTo(game.GameId));
        });
    }

    [Test]
    public void DurationMinutes_CanBeShort()
    {
        // Arrange & Act
        var session = new PlaySession
        {
            PlaySessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            GameId = Guid.NewGuid(),
            DurationMinutes = 15
        };

        // Assert
        Assert.That(session.DurationMinutes, Is.EqualTo(15));
    }

    [Test]
    public void DurationMinutes_CanBeLong()
    {
        // Arrange & Act
        var session = new PlaySession
        {
            PlaySessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            GameId = Guid.NewGuid(),
            DurationMinutes = 480 // 8 hours
        };

        // Assert
        Assert.That(session.DurationMinutes, Is.EqualTo(480));
    }

    [Test]
    public void PlaySession_WithLongNotes_StoresCorrectly()
    {
        // Arrange
        var longNotes = "Had an amazing gaming session today. Completed three challenging boss fights and discovered " +
                       "several secret areas. Really enjoying the storyline and character development.";

        // Act
        var session = new PlaySession
        {
            PlaySessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            GameId = Guid.NewGuid(),
            Notes = longNotes
        };

        // Assert
        Assert.That(session.Notes, Is.EqualTo(longNotes));
    }

    [Test]
    public void PlaySession_ActiveSession_HasNoEndTime()
    {
        // Arrange & Act
        var session = new PlaySession
        {
            PlaySessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            GameId = Guid.NewGuid(),
            StartTime = DateTime.UtcNow
        };

        // Assert
        Assert.That(session.EndTime, Is.Null);
    }
}
