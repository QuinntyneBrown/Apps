// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SportsTeamFollowingTracker.Core.Tests;

public class SeasonTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesSeason()
    {
        // Arrange
        var seasonId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var teamId = Guid.NewGuid();
        var seasonName = "2024 Regular Season";
        var year = 2024;
        var wins = 95;
        var losses = 67;
        var notes = "Strong playoff contender";

        // Act
        var season = new Season
        {
            SeasonId = seasonId,
            UserId = userId,
            TeamId = teamId,
            SeasonName = seasonName,
            Year = year,
            Wins = wins,
            Losses = losses,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(season.SeasonId, Is.EqualTo(seasonId));
            Assert.That(season.UserId, Is.EqualTo(userId));
            Assert.That(season.TeamId, Is.EqualTo(teamId));
            Assert.That(season.SeasonName, Is.EqualTo(seasonName));
            Assert.That(season.Year, Is.EqualTo(year));
            Assert.That(season.Wins, Is.EqualTo(wins));
            Assert.That(season.Losses, Is.EqualTo(losses));
            Assert.That(season.Notes, Is.EqualTo(notes));
        });
    }

    [Test]
    public void DefaultValues_NewSeason_HasExpectedDefaults()
    {
        // Act
        var season = new Season();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(season.SeasonName, Is.EqualTo(string.Empty));
            Assert.That(season.Year, Is.EqualTo(0));
            Assert.That(season.Wins, Is.EqualTo(0));
            Assert.That(season.Losses, Is.EqualTo(0));
            Assert.That(season.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Wins_CanStoreValidWinCount()
    {
        // Arrange & Act
        var season = new Season
        {
            Wins = 82
        };

        // Assert
        Assert.That(season.Wins, Is.EqualTo(82));
    }

    [Test]
    public void Losses_CanStoreValidLossCount()
    {
        // Arrange & Act
        var season = new Season
        {
            Losses = 80
        };

        // Assert
        Assert.That(season.Losses, Is.EqualTo(80));
    }

    [Test]
    public void WinsAndLosses_FullSeason_CanStoreTotals()
    {
        // Arrange & Act
        var season = new Season
        {
            Wins = 100,
            Losses = 62
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(season.Wins, Is.EqualTo(100));
            Assert.That(season.Losses, Is.EqualTo(62));
        });
    }

    [Test]
    public void Year_CanStoreValidYear()
    {
        // Arrange & Act
        var season = new Season
        {
            Year = 2024
        };

        // Assert
        Assert.That(season.Year, Is.EqualTo(2024));
    }

    [Test]
    public void Year_HistoricalSeason_CanStoreOldYear()
    {
        // Arrange & Act
        var season = new Season
        {
            Year = 1990
        };

        // Assert
        Assert.That(season.Year, Is.EqualTo(1990));
    }

    [Test]
    public void SeasonName_DifferentFormats_CanBeStored()
    {
        // Arrange & Act
        var season1 = new Season { SeasonName = "2024 Regular Season" };
        var season2 = new Season { SeasonName = "2023-2024 Season" };
        var season3 = new Season { SeasonName = "Spring 2024" };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(season1.SeasonName, Is.EqualTo("2024 Regular Season"));
            Assert.That(season2.SeasonName, Is.EqualTo("2023-2024 Season"));
            Assert.That(season3.SeasonName, Is.EqualTo("Spring 2024"));
        });
    }

    [Test]
    public void Notes_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var season = new Season
        {
            Notes = null
        };

        // Assert
        Assert.That(season.Notes, Is.Null);
    }

    [Test]
    public void Notes_CanStoreDetailedNotes()
    {
        // Arrange
        var notes = "Championship season with record-breaking performance";

        // Act
        var season = new Season
        {
            Notes = notes
        };

        // Assert
        Assert.That(season.Notes, Is.EqualTo(notes));
    }

    [Test]
    public void WinningRecord_MoreWinsThanLosses_CanBeTracked()
    {
        // Arrange & Act
        var season = new Season
        {
            Wins = 110,
            Losses = 52
        };

        // Assert
        Assert.That(season.Wins, Is.GreaterThan(season.Losses));
    }

    [Test]
    public void LosingRecord_MoreLossesThanWins_CanBeTracked()
    {
        // Arrange & Act
        var season = new Season
        {
            Wins = 45,
            Losses = 117
        };

        // Assert
        Assert.That(season.Losses, Is.GreaterThan(season.Wins));
    }

    [Test]
    public void EvenRecord_EqualWinsAndLosses_CanBeTracked()
    {
        // Arrange & Act
        var season = new Season
        {
            Wins = 81,
            Losses = 81
        };

        // Assert
        Assert.That(season.Wins, Is.EqualTo(season.Losses));
    }

    [Test]
    public void TeamId_CanBeAssociated()
    {
        // Arrange
        var teamId = Guid.NewGuid();

        // Act
        var season = new Season
        {
            TeamId = teamId
        };

        // Assert
        Assert.That(season.TeamId, Is.EqualTo(teamId));
    }
}
