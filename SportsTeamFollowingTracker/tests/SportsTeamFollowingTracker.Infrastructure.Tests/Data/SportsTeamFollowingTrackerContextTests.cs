// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SportsTeamFollowingTracker.Infrastructure.Tests;

/// <summary>
/// Unit tests for the SportsTeamFollowingTrackerContext.
/// </summary>
[TestFixture]
public class SportsTeamFollowingTrackerContextTests
{
    private SportsTeamFollowingTrackerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<SportsTeamFollowingTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new SportsTeamFollowingTrackerContext(options);
    }

    /// <summary>
    /// Tears down the test context.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    /// <summary>
    /// Tests that Teams can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Teams_CanAddAndRetrieve()
    {
        // Arrange
        var team = new Team
        {
            TeamId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Team",
            Sport = Sport.Basketball,
            League = "NBA",
            City = "Test City",
            IsFavorite = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Teams.Add(team);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Teams.FindAsync(team.TeamId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Team"));
        Assert.That(retrieved.Sport, Is.EqualTo(Sport.Basketball));
    }

    /// <summary>
    /// Tests that Games can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Games_CanAddAndRetrieve()
    {
        // Arrange
        var team = new Team
        {
            TeamId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Team",
            Sport = Sport.Football,
            CreatedAt = DateTime.UtcNow,
        };

        var game = new Game
        {
            GameId = Guid.NewGuid(),
            UserId = team.UserId,
            TeamId = team.TeamId,
            GameDate = DateTime.UtcNow,
            Opponent = "Opponent Team",
            TeamScore = 28,
            OpponentScore = 21,
            IsWin = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Teams.Add(team);
        _context.Games.Add(game);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Games.FindAsync(game.GameId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Opponent, Is.EqualTo("Opponent Team"));
        Assert.That(retrieved.IsWin, Is.True);
    }

    /// <summary>
    /// Tests that Seasons can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Seasons_CanAddAndRetrieve()
    {
        // Arrange
        var season = new Season
        {
            SeasonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            TeamId = Guid.NewGuid(),
            SeasonName = "2024 Season",
            Year = 2024,
            Wins = 10,
            Losses = 6,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Seasons.Add(season);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Seasons.FindAsync(season.SeasonId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.SeasonName, Is.EqualTo("2024 Season"));
        Assert.That(retrieved.Wins, Is.EqualTo(10));
    }

    /// <summary>
    /// Tests that Statistics can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Statistics_CanAddAndRetrieve()
    {
        // Arrange
        var statistic = new Statistic
        {
            StatisticId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            TeamId = Guid.NewGuid(),
            StatName = "Points Per Game",
            Value = 115.5m,
            RecordedDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Statistics.Add(statistic);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Statistics.FindAsync(statistic.StatisticId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.StatName, Is.EqualTo("Points Per Game"));
        Assert.That(retrieved.Value, Is.EqualTo(115.5m));
    }

    /// <summary>
    /// Tests that cascade delete works for Team and Games.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedGames()
    {
        // Arrange
        var team = new Team
        {
            TeamId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Team",
            Sport = Sport.Soccer,
            CreatedAt = DateTime.UtcNow,
        };

        var game = new Game
        {
            GameId = Guid.NewGuid(),
            UserId = team.UserId,
            TeamId = team.TeamId,
            GameDate = DateTime.UtcNow,
            Opponent = "Rival Team",
            CreatedAt = DateTime.UtcNow,
        };

        _context.Teams.Add(team);
        _context.Games.Add(game);
        await _context.SaveChangesAsync();

        // Act
        _context.Teams.Remove(team);
        await _context.SaveChangesAsync();

        var retrievedGame = await _context.Games.FindAsync(game.GameId);

        // Assert
        Assert.That(retrievedGame, Is.Null);
    }
}
