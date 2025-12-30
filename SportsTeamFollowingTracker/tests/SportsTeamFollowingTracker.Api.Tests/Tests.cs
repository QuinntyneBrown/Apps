using SportsTeamFollowingTracker.Api.Features.Teams;
using SportsTeamFollowingTracker.Api.Features.Games;
using SportsTeamFollowingTracker.Api.Features.Seasons;
using SportsTeamFollowingTracker.Api.Features.Statistics;

namespace SportsTeamFollowingTracker.Api.Tests;

[TestFixture]
public class ApiTests
{
    [Test]
    public void TeamDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var team = new Core.Team
        {
            TeamId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Chicago Bulls",
            Sport = Core.Sport.Basketball,
            League = "NBA",
            City = "Chicago",
            IsFavorite = true,
            CreatedAt = DateTime.UtcNow,
            Games = new List<Core.Game>
            {
                new Core.Game { GameId = Guid.NewGuid() },
                new Core.Game { GameId = Guid.NewGuid() },
            },
        };

        // Act
        var dto = team.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.TeamId, Is.EqualTo(team.TeamId));
            Assert.That(dto.UserId, Is.EqualTo(team.UserId));
            Assert.That(dto.Name, Is.EqualTo(team.Name));
            Assert.That(dto.Sport, Is.EqualTo(team.Sport));
            Assert.That(dto.League, Is.EqualTo(team.League));
            Assert.That(dto.City, Is.EqualTo(team.City));
            Assert.That(dto.IsFavorite, Is.EqualTo(team.IsFavorite));
            Assert.That(dto.CreatedAt, Is.EqualTo(team.CreatedAt));
            Assert.That(dto.GamesCount, Is.EqualTo(2));
        });
    }

    [Test]
    public void GameDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var game = new Core.Game
        {
            GameId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            TeamId = Guid.NewGuid(),
            Team = new Core.Team { Name = "Lakers" },
            GameDate = DateTime.UtcNow,
            Opponent = "Warriors",
            TeamScore = 110,
            OpponentScore = 105,
            IsWin = true,
            Notes = "Great game!",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = game.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.GameId, Is.EqualTo(game.GameId));
            Assert.That(dto.UserId, Is.EqualTo(game.UserId));
            Assert.That(dto.TeamId, Is.EqualTo(game.TeamId));
            Assert.That(dto.TeamName, Is.EqualTo(game.Team.Name));
            Assert.That(dto.GameDate, Is.EqualTo(game.GameDate));
            Assert.That(dto.Opponent, Is.EqualTo(game.Opponent));
            Assert.That(dto.TeamScore, Is.EqualTo(game.TeamScore));
            Assert.That(dto.OpponentScore, Is.EqualTo(game.OpponentScore));
            Assert.That(dto.IsWin, Is.EqualTo(game.IsWin));
            Assert.That(dto.Notes, Is.EqualTo(game.Notes));
            Assert.That(dto.CreatedAt, Is.EqualTo(game.CreatedAt));
            Assert.That(dto.IsCompleted, Is.True);
        });
    }

    [Test]
    public void GameDto_ToDto_IncompleteGame_IsCompletedFalse()
    {
        // Arrange
        var game = new Core.Game
        {
            GameId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            TeamId = Guid.NewGuid(),
            GameDate = DateTime.UtcNow,
            Opponent = "Warriors",
            TeamScore = null,
            OpponentScore = null,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = game.ToDto();

        // Assert
        Assert.That(dto.IsCompleted, Is.False);
    }

    [Test]
    public void SeasonDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var season = new Core.Season
        {
            SeasonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            TeamId = Guid.NewGuid(),
            SeasonName = "2023-2024",
            Year = 2024,
            Wins = 45,
            Losses = 37,
            Notes = "Playoff season",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = season.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.SeasonId, Is.EqualTo(season.SeasonId));
            Assert.That(dto.UserId, Is.EqualTo(season.UserId));
            Assert.That(dto.TeamId, Is.EqualTo(season.TeamId));
            Assert.That(dto.SeasonName, Is.EqualTo(season.SeasonName));
            Assert.That(dto.Year, Is.EqualTo(season.Year));
            Assert.That(dto.Wins, Is.EqualTo(season.Wins));
            Assert.That(dto.Losses, Is.EqualTo(season.Losses));
            Assert.That(dto.Notes, Is.EqualTo(season.Notes));
            Assert.That(dto.CreatedAt, Is.EqualTo(season.CreatedAt));
            Assert.That(dto.TotalGames, Is.EqualTo(82));
            Assert.That(dto.WinPercentage, Is.EqualTo(54.88).Within(0.01));
        });
    }

    [Test]
    public void SeasonDto_ToDto_NoGames_CalculatesZeroPercentage()
    {
        // Arrange
        var season = new Core.Season
        {
            SeasonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            TeamId = Guid.NewGuid(),
            SeasonName = "2024-2025",
            Year = 2025,
            Wins = 0,
            Losses = 0,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = season.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.TotalGames, Is.EqualTo(0));
            Assert.That(dto.WinPercentage, Is.EqualTo(0));
        });
    }

    [Test]
    public void StatisticDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var statistic = new Core.Statistic
        {
            StatisticId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            TeamId = Guid.NewGuid(),
            StatName = "Points Per Game",
            Value = 112.5m,
            RecordedDate = DateTime.UtcNow.AddDays(-1),
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = statistic.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.StatisticId, Is.EqualTo(statistic.StatisticId));
            Assert.That(dto.UserId, Is.EqualTo(statistic.UserId));
            Assert.That(dto.TeamId, Is.EqualTo(statistic.TeamId));
            Assert.That(dto.StatName, Is.EqualTo(statistic.StatName));
            Assert.That(dto.Value, Is.EqualTo(statistic.Value));
            Assert.That(dto.RecordedDate, Is.EqualTo(statistic.RecordedDate));
            Assert.That(dto.CreatedAt, Is.EqualTo(statistic.CreatedAt));
        });
    }
}
