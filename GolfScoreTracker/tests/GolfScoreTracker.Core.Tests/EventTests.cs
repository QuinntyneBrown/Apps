// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GolfScoreTracker.Core.Tests;

public class EventTests
{
    [Test]
    public void HandicapCalculatedEvent_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var handicapId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var handicapIndex = 12.5m;
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new HandicapCalculatedEvent
        {
            HandicapId = handicapId,
            UserId = userId,
            HandicapIndex = handicapIndex,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.HandicapId, Is.EqualTo(handicapId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.HandicapIndex, Is.EqualTo(handicapIndex));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void HandicapCalculatedEvent_Timestamp_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var evt = new HandicapCalculatedEvent
        {
            HandicapId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            HandicapIndex = 10m
        };
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(evt.Timestamp, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void RoundCompletedEvent_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var roundId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var totalScore = 85;
        var playedDate = DateTime.UtcNow.AddDays(-1);
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new RoundCompletedEvent
        {
            RoundId = roundId,
            UserId = userId,
            TotalScore = totalScore,
            PlayedDate = playedDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.RoundId, Is.EqualTo(roundId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.TotalScore, Is.EqualTo(totalScore));
            Assert.That(evt.PlayedDate, Is.EqualTo(playedDate));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void RoundCompletedEvent_Timestamp_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var evt = new RoundCompletedEvent
        {
            RoundId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            TotalScore = 90,
            PlayedDate = DateTime.UtcNow
        };
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(evt.Timestamp, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void HoleCompletedEvent_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var holeScoreId = Guid.NewGuid();
        var roundId = Guid.NewGuid();
        var holeNumber = 9;
        var score = 4;
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new HoleCompletedEvent
        {
            HoleScoreId = holeScoreId,
            RoundId = roundId,
            HoleNumber = holeNumber,
            Score = score,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.HoleScoreId, Is.EqualTo(holeScoreId));
            Assert.That(evt.RoundId, Is.EqualTo(roundId));
            Assert.That(evt.HoleNumber, Is.EqualTo(holeNumber));
            Assert.That(evt.Score, Is.EqualTo(score));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void HoleCompletedEvent_Timestamp_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var evt = new HoleCompletedEvent
        {
            HoleScoreId = Guid.NewGuid(),
            RoundId = Guid.NewGuid(),
            HoleNumber = 1,
            Score = 5
        };
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(evt.Timestamp, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void CourseAddedEvent_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var courseId = Guid.NewGuid();
        var name = "St. Andrews";
        var totalPar = 72;
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new CourseAddedEvent
        {
            CourseId = courseId,
            Name = name,
            TotalPar = totalPar,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.CourseId, Is.EqualTo(courseId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.TotalPar, Is.EqualTo(totalPar));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void CourseAddedEvent_Timestamp_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var evt = new CourseAddedEvent
        {
            CourseId = Guid.NewGuid(),
            Name = "Test Course",
            TotalPar = 72
        };
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(evt.Timestamp, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void Events_AreRecords_AndSupportValueEquality()
    {
        // Arrange
        var id = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new HandicapCalculatedEvent
        {
            HandicapId = id,
            UserId = userId,
            HandicapIndex = 15m,
            Timestamp = timestamp
        };

        var event2 = new HandicapCalculatedEvent
        {
            HandicapId = id,
            UserId = userId,
            HandicapIndex = 15m,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }
}
