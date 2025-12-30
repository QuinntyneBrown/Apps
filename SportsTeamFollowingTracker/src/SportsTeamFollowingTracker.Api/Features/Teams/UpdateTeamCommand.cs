using SportsTeamFollowingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SportsTeamFollowingTracker.Api.Features.Teams;

public record UpdateTeamCommand : IRequest<TeamDto?>
{
    public Guid TeamId { get; init; }
    public string Name { get; init; } = string.Empty;
    public Sport Sport { get; init; }
    public string? League { get; init; }
    public string? City { get; init; }
    public bool IsFavorite { get; init; }
}

public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand, TeamDto?>
{
    private readonly ISportsTeamFollowingTrackerContext _context;
    private readonly ILogger<UpdateTeamCommandHandler> _logger;

    public UpdateTeamCommandHandler(
        ISportsTeamFollowingTrackerContext context,
        ILogger<UpdateTeamCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TeamDto?> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating team {TeamId}", request.TeamId);

        var team = await _context.Teams
            .Include(t => t.Games)
            .FirstOrDefaultAsync(t => t.TeamId == request.TeamId, cancellationToken);

        if (team == null)
        {
            _logger.LogWarning("Team {TeamId} not found", request.TeamId);
            return null;
        }

        team.Name = request.Name;
        team.Sport = request.Sport;
        team.League = request.League;
        team.City = request.City;
        team.IsFavorite = request.IsFavorite;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated team {TeamId}", request.TeamId);

        return team.ToDto();
    }
}
