using SportsTeamFollowingTracker.Core;

namespace SportsTeamFollowingTracker.Api.Features.Seasons;

public record SeasonDto
{
    public Guid SeasonId { get; init; }
    public Guid UserId { get; init; }
    public Guid TeamId { get; init; }
    public string SeasonName { get; init; } = string.Empty;
    public int Year { get; init; }
    public int Wins { get; init; }
    public int Losses { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
    public int TotalGames { get; init; }
    public double WinPercentage { get; init; }
}

public static class SeasonExtensions
{
    public static SeasonDto ToDto(this Season season)
    {
        var totalGames = season.Wins + season.Losses;
        var winPercentage = totalGames > 0 ? (double)season.Wins / totalGames * 100 : 0;

        return new SeasonDto
        {
            SeasonId = season.SeasonId,
            UserId = season.UserId,
            TeamId = season.TeamId,
            SeasonName = season.SeasonName,
            Year = season.Year,
            Wins = season.Wins,
            Losses = season.Losses,
            Notes = season.Notes,
            CreatedAt = season.CreatedAt,
            TotalGames = totalGames,
            WinPercentage = Math.Round(winPercentage, 2),
        };
    }
}
