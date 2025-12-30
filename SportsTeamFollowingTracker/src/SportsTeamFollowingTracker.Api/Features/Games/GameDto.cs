using SportsTeamFollowingTracker.Core;

namespace SportsTeamFollowingTracker.Api.Features.Games;

public record GameDto
{
    public Guid GameId { get; init; }
    public Guid UserId { get; init; }
    public Guid TeamId { get; init; }
    public string? TeamName { get; init; }
    public DateTime GameDate { get; init; }
    public string Opponent { get; init; } = string.Empty;
    public int? TeamScore { get; init; }
    public int? OpponentScore { get; init; }
    public bool? IsWin { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
    public bool IsCompleted { get; init; }
}

public static class GameExtensions
{
    public static GameDto ToDto(this Game game)
    {
        return new GameDto
        {
            GameId = game.GameId,
            UserId = game.UserId,
            TeamId = game.TeamId,
            TeamName = game.Team?.Name,
            GameDate = game.GameDate,
            Opponent = game.Opponent,
            TeamScore = game.TeamScore,
            OpponentScore = game.OpponentScore,
            IsWin = game.IsWin,
            Notes = game.Notes,
            CreatedAt = game.CreatedAt,
            IsCompleted = game.TeamScore.HasValue && game.OpponentScore.HasValue,
        };
    }
}
