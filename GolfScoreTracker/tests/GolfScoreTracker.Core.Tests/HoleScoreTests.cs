// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GolfScoreTracker.Core.Tests;

public class HoleScoreTests
{
    [Test]
    public void HoleScore_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var holeScoreId = Guid.NewGuid();
        var roundId = Guid.NewGuid();
        var holeNumber = 5;
        var par = 4;
        var score = 5;
        var putts = 2;
        var fairwayHit = true;
        var greenInRegulation = false;
        var notes = "Missed fairway left";
        var createdAt = DateTime.UtcNow;

        // Act
        var holeScore = new HoleScore
        {
            HoleScoreId = holeScoreId,
            RoundId = roundId,
            HoleNumber = holeNumber,
            Par = par,
            Score = score,
            Putts = putts,
            FairwayHit = fairwayHit,
            GreenInRegulation = greenInRegulation,
            Notes = notes,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(holeScore.HoleScoreId, Is.EqualTo(holeScoreId));
            Assert.That(holeScore.RoundId, Is.EqualTo(roundId));
            Assert.That(holeScore.HoleNumber, Is.EqualTo(holeNumber));
            Assert.That(holeScore.Par, Is.EqualTo(par));
            Assert.That(holeScore.Score, Is.EqualTo(score));
            Assert.That(holeScore.Putts, Is.EqualTo(putts));
            Assert.That(holeScore.FairwayHit, Is.True);
            Assert.That(holeScore.GreenInRegulation, Is.False);
            Assert.That(holeScore.Notes, Is.EqualTo(notes));
            Assert.That(holeScore.CreatedAt, Is.EqualTo(createdAt));
        });
    }

    [Test]
    public void HoleScore_CreatedAt_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var holeScore = new HoleScore();
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(holeScore.CreatedAt, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void HoleScore_Putts_CanBeNull()
    {
        // Arrange & Act
        var holeScore = new HoleScore
        {
            Putts = null
        };

        // Assert
        Assert.That(holeScore.Putts, Is.Null);
    }

    [Test]
    public void HoleScore_Notes_CanBeNull()
    {
        // Arrange & Act
        var holeScore = new HoleScore
        {
            Notes = null
        };

        // Assert
        Assert.That(holeScore.Notes, Is.Null);
    }

    [Test]
    public void GetScoreToPar_ReturnsPositive_WhenOverPar()
    {
        // Arrange
        var holeScore = new HoleScore
        {
            Score = 6,
            Par = 4
        };

        // Act
        var result = holeScore.GetScoreToPar();

        // Assert
        Assert.That(result, Is.EqualTo(2));
    }

    [Test]
    public void GetScoreToPar_ReturnsNegative_WhenUnderPar()
    {
        // Arrange
        var holeScore = new HoleScore
        {
            Score = 3,
            Par = 4
        };

        // Act
        var result = holeScore.GetScoreToPar();

        // Assert
        Assert.That(result, Is.EqualTo(-1));
    }

    [Test]
    public void GetScoreToPar_ReturnsZero_WhenEqualToPar()
    {
        // Arrange
        var holeScore = new HoleScore
        {
            Score = 4,
            Par = 4
        };

        // Act
        var result = holeScore.GetScoreToPar();

        // Assert
        Assert.That(result, Is.EqualTo(0));
    }

    [Test]
    public void HoleScore_FairwayHit_DefaultsToFalse()
    {
        // Arrange & Act
        var holeScore = new HoleScore();

        // Assert
        Assert.That(holeScore.FairwayHit, Is.False);
    }

    [Test]
    public void HoleScore_GreenInRegulation_DefaultsToFalse()
    {
        // Arrange & Act
        var holeScore = new HoleScore();

        // Assert
        Assert.That(holeScore.GreenInRegulation, Is.False);
    }

    [Test]
    public void HoleScore_Round_CanBeSet()
    {
        // Arrange
        var round = new Round
        {
            RoundId = Guid.NewGuid()
        };

        var holeScore = new HoleScore();

        // Act
        holeScore.Round = round;

        // Assert
        Assert.That(holeScore.Round, Is.EqualTo(round));
    }

    [Test]
    public void HoleScore_CanRepresentPar3()
    {
        // Arrange & Act
        var holeScore = new HoleScore
        {
            HoleNumber = 7,
            Par = 3,
            Score = 3
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(holeScore.Par, Is.EqualTo(3));
            Assert.That(holeScore.GetScoreToPar(), Is.EqualTo(0));
        });
    }

    [Test]
    public void HoleScore_CanRepresentPar5()
    {
        // Arrange & Act
        var holeScore = new HoleScore
        {
            HoleNumber = 9,
            Par = 5,
            Score = 4
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(holeScore.Par, Is.EqualTo(5));
            Assert.That(holeScore.GetScoreToPar(), Is.EqualTo(-1));
        });
    }

    [Test]
    public void HoleScore_Putts_CanBeZero()
    {
        // Arrange & Act
        var holeScore = new HoleScore
        {
            Putts = 0
        };

        // Assert
        Assert.That(holeScore.Putts, Is.EqualTo(0));
    }
}
