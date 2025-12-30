// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SportsTeamFollowingTracker.Core.Tests;

public class TeamTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesTeam()
    {
        // Arrange
        var teamId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Boston Red Sox";
        var sport = Sport.Baseball;
        var league = "MLB";
        var city = "Boston";
        var isFavorite = true;

        // Act
        var team = new Team
        {
            TeamId = teamId,
            UserId = userId,
            Name = name,
            Sport = sport,
            League = league,
            City = city,
            IsFavorite = isFavorite
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(team.TeamId, Is.EqualTo(teamId));
            Assert.That(team.UserId, Is.EqualTo(userId));
            Assert.That(team.Name, Is.EqualTo(name));
            Assert.That(team.Sport, Is.EqualTo(sport));
            Assert.That(team.League, Is.EqualTo(league));
            Assert.That(team.City, Is.EqualTo(city));
            Assert.That(team.IsFavorite, Is.True);
        });
    }

    [Test]
    public void DefaultValues_NewTeam_HasExpectedDefaults()
    {
        // Act
        var team = new Team();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(team.Name, Is.EqualTo(string.Empty));
            Assert.That(team.Sport, Is.EqualTo(Sport.Football));
            Assert.That(team.IsFavorite, Is.False);
            Assert.That(team.Games, Is.Not.Null);
            Assert.That(team.Games, Is.Empty);
            Assert.That(team.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Sport_AllEnumValues_CanBeAssigned()
    {
        // Arrange
        var team = new Team();

        // Act & Assert
        team.Sport = Sport.Football;
        Assert.That(team.Sport, Is.EqualTo(Sport.Football));

        team.Sport = Sport.Basketball;
        Assert.That(team.Sport, Is.EqualTo(Sport.Basketball));

        team.Sport = Sport.Baseball;
        Assert.That(team.Sport, Is.EqualTo(Sport.Baseball));

        team.Sport = Sport.Hockey;
        Assert.That(team.Sport, Is.EqualTo(Sport.Hockey));

        team.Sport = Sport.Soccer;
        Assert.That(team.Sport, Is.EqualTo(Sport.Soccer));

        team.Sport = Sport.Tennis;
        Assert.That(team.Sport, Is.EqualTo(Sport.Tennis));

        team.Sport = Sport.Golf;
        Assert.That(team.Sport, Is.EqualTo(Sport.Golf));

        team.Sport = Sport.Rugby;
        Assert.That(team.Sport, Is.EqualTo(Sport.Rugby));

        team.Sport = Sport.Cricket;
        Assert.That(team.Sport, Is.EqualTo(Sport.Cricket));

        team.Sport = Sport.Other;
        Assert.That(team.Sport, Is.EqualTo(Sport.Other));
    }

    [Test]
    public void IsFavorite_CanBeSetToTrue()
    {
        // Arrange & Act
        var team = new Team
        {
            IsFavorite = true
        };

        // Assert
        Assert.That(team.IsFavorite, Is.True);
    }

    [Test]
    public void IsFavorite_CanBeSetToFalse()
    {
        // Arrange & Act
        var team = new Team
        {
            IsFavorite = false
        };

        // Assert
        Assert.That(team.IsFavorite, Is.False);
    }

    [Test]
    public void League_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var team = new Team
        {
            League = null
        };

        // Assert
        Assert.That(team.League, Is.Null);
    }

    [Test]
    public void City_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var team = new Team
        {
            City = null
        };

        // Assert
        Assert.That(team.City, Is.Null);
    }

    [Test]
    public void League_DifferentLeagues_CanBeStored()
    {
        // Arrange & Act
        var team1 = new Team { League = "NFL" };
        var team2 = new Team { League = "NBA" };
        var team3 = new Team { League = "Premier League" };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(team1.League, Is.EqualTo("NFL"));
            Assert.That(team2.League, Is.EqualTo("NBA"));
            Assert.That(team3.League, Is.EqualTo("Premier League"));
        });
    }

    [Test]
    public void Games_CollectionInitialized_ByDefault()
    {
        // Arrange & Act
        var team = new Team();

        // Assert
        Assert.That(team.Games, Is.Not.Null);
    }

    [Test]
    public void Games_CanAddGames_ToCollection()
    {
        // Arrange
        var team = new Team();
        var game1 = new Game { GameId = Guid.NewGuid(), Opponent = "Team A" };
        var game2 = new Game { GameId = Guid.NewGuid(), Opponent = "Team B" };

        // Act
        team.Games.Add(game1);
        team.Games.Add(game2);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(team.Games, Has.Count.EqualTo(2));
            Assert.That(team.Games, Contains.Item(game1));
            Assert.That(team.Games, Contains.Item(game2));
        });
    }

    [Test]
    public void Name_CanStoreTeamName()
    {
        // Arrange
        var teamName = "Manchester United";

        // Act
        var team = new Team
        {
            Name = teamName
        };

        // Assert
        Assert.That(team.Name, Is.EqualTo(teamName));
    }

    [Test]
    public void City_CanStoreCityName()
    {
        // Arrange
        var cityName = "New York";

        // Act
        var team = new Team
        {
            City = cityName
        };

        // Assert
        Assert.That(team.City, Is.EqualTo(cityName));
    }

    [Test]
    public void UserId_CanBeAssociated()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        var team = new Team
        {
            UserId = userId
        };

        // Assert
        Assert.That(team.UserId, Is.EqualTo(userId));
    }

    [Test]
    public void CreatedAt_AutomaticallySet_OnCreation()
    {
        // Arrange & Act
        var team = new Team();

        // Assert
        Assert.That(team.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }
}
