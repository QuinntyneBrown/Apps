// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GolfScoreTracker.Core.Tests;

public class RoundTests
{
    [Test]
    public void Round_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var roundId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var courseId = Guid.NewGuid();
        var playedDate = DateTime.UtcNow;
        var totalScore = 85;
        var totalPar = 72;
        var weather = "Sunny";
        var notes = "Great round!";
        var createdAt = DateTime.UtcNow;

        // Act
        var round = new Round
        {
            RoundId = roundId,
            UserId = userId,
            CourseId = courseId,
            PlayedDate = playedDate,
            TotalScore = totalScore,
            TotalPar = totalPar,
            Weather = weather,
            Notes = notes,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(round.RoundId, Is.EqualTo(roundId));
            Assert.That(round.UserId, Is.EqualTo(userId));
            Assert.That(round.CourseId, Is.EqualTo(courseId));
            Assert.That(round.PlayedDate, Is.EqualTo(playedDate));
            Assert.That(round.TotalScore, Is.EqualTo(totalScore));
            Assert.That(round.TotalPar, Is.EqualTo(totalPar));
            Assert.That(round.Weather, Is.EqualTo(weather));
            Assert.That(round.Notes, Is.EqualTo(notes));
            Assert.That(round.CreatedAt, Is.EqualTo(createdAt));
        });
    }

    [Test]
    public void Round_PlayedDate_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var round = new Round();
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(round.PlayedDate, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void Round_CreatedAt_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var round = new Round();
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(round.CreatedAt, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void Round_Weather_CanBeNull()
    {
        // Arrange & Act
        var round = new Round
        {
            Weather = null
        };

        // Assert
        Assert.That(round.Weather, Is.Null);
    }

    [Test]
    public void Round_Notes_CanBeNull()
    {
        // Arrange & Act
        var round = new Round
        {
            Notes = null
        };

        // Assert
        Assert.That(round.Notes, Is.Null);
    }

    [Test]
    public void Round_HoleScores_DefaultsToEmptyList()
    {
        // Arrange & Act
        var round = new Round();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(round.HoleScores, Is.Not.Null);
            Assert.That(round.HoleScores, Is.Empty);
        });
    }

    [Test]
    public void GetScoreToPar_ReturnsPositive_WhenOverPar()
    {
        // Arrange
        var round = new Round
        {
            TotalScore = 85,
            TotalPar = 72
        };

        // Act
        var result = round.GetScoreToPar();

        // Assert
        Assert.That(result, Is.EqualTo(13));
    }

    [Test]
    public void GetScoreToPar_ReturnsNegative_WhenUnderPar()
    {
        // Arrange
        var round = new Round
        {
            TotalScore = 68,
            TotalPar = 72
        };

        // Act
        var result = round.GetScoreToPar();

        // Assert
        Assert.That(result, Is.EqualTo(-4));
    }

    [Test]
    public void GetScoreToPar_ReturnsZero_WhenEqualToPar()
    {
        // Arrange
        var round = new Round
        {
            TotalScore = 72,
            TotalPar = 72
        };

        // Act
        var result = round.GetScoreToPar();

        // Assert
        Assert.That(result, Is.EqualTo(0));
    }

    [Test]
    public void Round_Course_CanBeSet()
    {
        // Arrange
        var course = new Course
        {
            CourseId = Guid.NewGuid(),
            Name = "Augusta National"
        };

        var round = new Round();

        // Act
        round.Course = course;

        // Assert
        Assert.That(round.Course, Is.EqualTo(course));
    }

    [Test]
    public void Round_CanHaveMultipleHoleScores()
    {
        // Arrange
        var round = new Round();
        var holeScore1 = new HoleScore { HoleNumber = 1, Score = 4 };
        var holeScore2 = new HoleScore { HoleNumber = 2, Score = 3 };

        // Act
        round.HoleScores.Add(holeScore1);
        round.HoleScores.Add(holeScore2);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(round.HoleScores, Has.Count.EqualTo(2));
            Assert.That(round.HoleScores, Contains.Item(holeScore1));
            Assert.That(round.HoleScores, Contains.Item(holeScore2));
        });
    }

    [Test]
    public void Round_TotalScore_CanBeAnyValue()
    {
        // Arrange & Act
        var goodRound = new Round { TotalScore = 72 };
        var badRound = new Round { TotalScore = 120 };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(goodRound.TotalScore, Is.EqualTo(72));
            Assert.That(badRound.TotalScore, Is.EqualTo(120));
        });
    }
}
