using SportsTeamFollowingTracker.Core;

namespace SportsTeamFollowingTracker.Api.Features.Teams;

public record TeamDto
{
    public Guid TeamId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public Sport Sport { get; init; }
    public string? League { get; init; }
    public string? City { get; init; }
    public bool IsFavorite { get; init; }
    public DateTime CreatedAt { get; init; }
    public int GamesCount { get; init; }
}

public static class TeamExtensions
{
    public static TeamDto ToDto(this Team team)
    {
        return new TeamDto
        {
            TeamId = team.TeamId,
            UserId = team.UserId,
            Name = team.Name,
            Sport = team.Sport,
            League = team.League,
            City = team.City,
            IsFavorite = team.IsFavorite,
            CreatedAt = team.CreatedAt,
            GamesCount = team.Games?.Count ?? 0,
        };
    }
}
