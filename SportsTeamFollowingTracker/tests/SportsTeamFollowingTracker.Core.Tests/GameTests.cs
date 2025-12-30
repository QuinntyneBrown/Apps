// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SportsTeamFollowingTracker.Core.Tests;

public class GameTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesGame()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var teamId = Guid.NewGuid();
        var gameDate = new DateTime(2024, 1, 15, 19, 0, 0);
        var opponent = "New York Yankees";
        var teamScore = 5;
        var opponentScore = 3;
        var isWin = true;
        var notes = "Great pitching performance";

        // Act
        var game = new Game
        {
            GameId = gameId,
            UserId = userId,
            TeamId = teamId,
            GameDate = gameDate,
            Opponent = opponent,
            TeamScore = teamScore,
            OpponentScore = opponentScore,
            IsWin = isWin,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(game.GameId, Is.EqualTo(gameId));
            Assert.That(game.UserId, Is.EqualTo(userId));
            Assert.That(game.TeamId, Is.EqualTo(teamId));
            Assert.That(game.GameDate, Is.EqualTo(gameDate));
            Assert.That(game.Opponent, Is.EqualTo(opponent));
            Assert.That(game.TeamScore, Is.EqualTo(teamScore));
            Assert.That(game.OpponentScore, Is.EqualTo(opponentScore));
            Assert.That(game.IsWin, Is.True);
            Assert.That(game.Notes, Is.EqualTo(notes));
        });
    }

    [Test]
    public void DefaultValues_NewGame_HasExpectedDefaults()
    {
        // Act
        var game = new Game();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(game.Opponent, Is.EqualTo(string.Empty));
            Assert.That(game.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void IsWin_WinningGame_CanBeSetToTrue()
    {
        // Arrange & Act
        var game = new Game
        {
            TeamScore = 4,
            OpponentScore = 2,
            IsWin = true
        };

        // Assert
        Assert.That(game.IsWin, Is.True);
    }

    [Test]
    public void IsWin_LosingGame_CanBeSetToFalse()
    {
        // Arrange & Act
        var game = new Game
        {
            TeamScore = 2,
            OpponentScore = 4,
            IsWin = false
        };

        // Assert
        Assert.That(game.IsWin, Is.False);
    }

    [Test]
    public void IsWin_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var game = new Game
        {
            IsWin = null
        };

        // Assert
        Assert.That(game.IsWin, Is.Null);
    }

    [Test]
    public void TeamScore_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var game = new Game
        {
            TeamScore = null
        };

        // Assert
        Assert.That(game.TeamScore, Is.Null);
    }

    [Test]
    public void OpponentScore_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var game = new Game
        {
            OpponentScore = null
        };

        // Assert
        Assert.That(game.OpponentScore, Is.Null);
    }

    [Test]
    public void Notes_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var game = new Game
        {
            Notes = null
        };

        // Assert
        Assert.That(game.Notes, Is.Null);
    }

    [Test]
    public void Team_NavigationProperty_CanBeNull()
    {
        // Arrange & Act
        var game = new Game
        {
            Team = null
        };

        // Assert
        Assert.That(game.Team, Is.Null);
    }

    [Test]
    public void Team_NavigationProperty_CanBeSet()
    {
        // Arrange
        var team = new Team
        {
            TeamId = Guid.NewGuid(),
            Name = "Boston Red Sox"
        };

        // Act
        var game = new Game
        {
            TeamId = team.TeamId,
            Team = team
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(game.Team, Is.Not.Null);
            Assert.That(game.Team.Name, Is.EqualTo("Boston Red Sox"));
            Assert.That(game.TeamId, Is.EqualTo(team.TeamId));
        });
    }

    [Test]
    public void Scores_HighScoringGame_CanStoreValues()
    {
        // Arrange & Act
        var game = new Game
        {
            TeamScore = 12,
            OpponentScore = 10
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(game.TeamScore, Is.EqualTo(12));
            Assert.That(game.OpponentScore, Is.EqualTo(10));
        });
    }

    [Test]
    public void Scores_ShutoutGame_CanStoreZeroScore()
    {
        // Arrange & Act
        var game = new Game
        {
            TeamScore = 3,
            OpponentScore = 0
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(game.TeamScore, Is.EqualTo(3));
            Assert.That(game.OpponentScore, Is.EqualTo(0));
        });
    }

    [Test]
    public void GameDate_CanStoreFutureDate()
    {
        // Arrange
        var futureDate = DateTime.UtcNow.AddDays(7);

        // Act
        var game = new Game
        {
            GameDate = futureDate
        };

        // Assert
        Assert.That(game.GameDate, Is.EqualTo(futureDate));
    }

    [Test]
    public void GameDate_CanStorePastDate()
    {
        // Arrange
        var pastDate = new DateTime(2023, 6, 15);

        // Act
        var game = new Game
        {
            GameDate = pastDate
        };

        // Assert
        Assert.That(game.GameDate, Is.EqualTo(pastDate));
    }

    [Test]
    public void Opponent_CanStoreTeamName()
    {
        // Arrange
        var opponentName = "Los Angeles Lakers";

        // Act
        var game = new Game
        {
            Opponent = opponentName
        };

        // Assert
        Assert.That(game.Opponent, Is.EqualTo(opponentName));
    }
}
