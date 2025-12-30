using SportsTeamFollowingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SportsTeamFollowingTracker.Api.Features.Teams;

public record GetTeamByIdQuery : IRequest<TeamDto?>
{
    public Guid TeamId { get; init; }
}

public class GetTeamByIdQueryHandler : IRequestHandler<GetTeamByIdQuery, TeamDto?>
{
    private readonly ISportsTeamFollowingTrackerContext _context;
    private readonly ILogger<GetTeamByIdQueryHandler> _logger;

    public GetTeamByIdQueryHandler(
        ISportsTeamFollowingTrackerContext context,
        ILogger<GetTeamByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TeamDto?> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting team {TeamId}", request.TeamId);

        var team = await _context.Teams
            .Include(t => t.Games)
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TeamId == request.TeamId, cancellationToken);

        if (team == null)
        {
            _logger.LogWarning("Team {TeamId} not found", request.TeamId);
            return null;
        }

        return team.ToDto();
    }
}
