// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VideoGameCollectionManager.Core.Tests;

public class EventTests
{
    [Test]
    public void GameAddedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "The Legend of Zelda";
        var platform = Platform.NintendoSwitch;
        var timestamp = DateTime.UtcNow;

        // Act
        var eventData = new GameAddedEvent
        {
            GameId = gameId,
            UserId = userId,
            Title = title,
            Platform = platform,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.GameId, Is.EqualTo(gameId));
            Assert.That(eventData.UserId, Is.EqualTo(userId));
            Assert.That(eventData.Title, Is.EqualTo(title));
            Assert.That(eventData.Platform, Is.EqualTo(platform));
            Assert.That(eventData.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void GameCompletedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var eventData = new GameCompletedEvent
        {
            GameId = gameId,
            UserId = userId,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.GameId, Is.EqualTo(gameId));
            Assert.That(eventData.UserId, Is.EqualTo(userId));
            Assert.That(eventData.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void PlaySessionStartedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var playSessionId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var gameId = Guid.NewGuid();
        var startTime = DateTime.UtcNow;
        var timestamp = DateTime.UtcNow;

        // Act
        var eventData = new PlaySessionStartedEvent
        {
            PlaySessionId = playSessionId,
            UserId = userId,
            GameId = gameId,
            StartTime = startTime,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.PlaySessionId, Is.EqualTo(playSessionId));
            Assert.That(eventData.UserId, Is.EqualTo(userId));
            Assert.That(eventData.GameId, Is.EqualTo(gameId));
            Assert.That(eventData.StartTime, Is.EqualTo(startTime));
            Assert.That(eventData.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void PlaySessionEndedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var playSessionId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var gameId = Guid.NewGuid();
        var durationMinutes = 120;
        var timestamp = DateTime.UtcNow;

        // Act
        var eventData = new PlaySessionEndedEvent
        {
            PlaySessionId = playSessionId,
            UserId = userId,
            GameId = gameId,
            DurationMinutes = durationMinutes,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.PlaySessionId, Is.EqualTo(playSessionId));
            Assert.That(eventData.UserId, Is.EqualTo(userId));
            Assert.That(eventData.GameId, Is.EqualTo(gameId));
            Assert.That(eventData.DurationMinutes, Is.EqualTo(durationMinutes));
            Assert.That(eventData.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void WishlistItemAddedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var wishlistId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "Upcoming Game";
        var timestamp = DateTime.UtcNow;

        // Act
        var eventData = new WishlistItemAddedEvent
        {
            WishlistId = wishlistId,
            UserId = userId,
            Title = title,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.WishlistId, Is.EqualTo(wishlistId));
            Assert.That(eventData.UserId, Is.EqualTo(userId));
            Assert.That(eventData.Title, Is.EqualTo(title));
            Assert.That(eventData.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void WishlistItemRemovedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var wishlistId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var eventData = new WishlistItemRemovedEvent
        {
            WishlistId = wishlistId,
            UserId = userId,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.WishlistId, Is.EqualTo(wishlistId));
            Assert.That(eventData.UserId, Is.EqualTo(userId));
            Assert.That(eventData.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Events_Timestamp_DefaultsToUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var event1 = new GameAddedEvent { GameId = Guid.NewGuid(), UserId = Guid.NewGuid(), Title = "Test", Platform = Platform.PC };
        var event2 = new PlaySessionStartedEvent { PlaySessionId = Guid.NewGuid(), UserId = Guid.NewGuid(), GameId = Guid.NewGuid(), StartTime = DateTime.UtcNow };
        var event3 = new WishlistItemAddedEvent { WishlistId = Guid.NewGuid(), UserId = Guid.NewGuid(), Title = "Test" };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(event1.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
            Assert.That(event2.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
            Assert.That(event3.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
        });
    }

    [Test]
    public void Events_AreRecords_SupportValueEquality()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new GameAddedEvent
        {
            GameId = gameId,
            UserId = userId,
            Title = "Zelda",
            Platform = Platform.NintendoSwitch,
            Timestamp = timestamp
        };

        var event2 = new GameAddedEvent
        {
            GameId = gameId,
            UserId = userId,
            Title = "Zelda",
            Platform = Platform.NintendoSwitch,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }
}
