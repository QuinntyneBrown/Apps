using SportsTeamFollowingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SportsTeamFollowingTracker.Api.Features.Teams;

public record GetTeamsQuery : IRequest<IEnumerable<TeamDto>>
{
    public Guid? UserId { get; init; }
    public Sport? Sport { get; init; }
    public bool? IsFavorite { get; init; }
}

public class GetTeamsQueryHandler : IRequestHandler<GetTeamsQuery, IEnumerable<TeamDto>>
{
    private readonly ISportsTeamFollowingTrackerContext _context;
    private readonly ILogger<GetTeamsQueryHandler> _logger;

    public GetTeamsQueryHandler(
        ISportsTeamFollowingTrackerContext context,
        ILogger<GetTeamsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<TeamDto>> Handle(GetTeamsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting teams for user {UserId}", request.UserId);

        var query = _context.Teams
            .Include(t => t.Games)
            .AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(t => t.UserId == request.UserId.Value);
        }

        if (request.Sport.HasValue)
        {
            query = query.Where(t => t.Sport == request.Sport.Value);
        }

        if (request.IsFavorite.HasValue)
        {
            query = query.Where(t => t.IsFavorite == request.IsFavorite.Value);
        }

        var teams = await query
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(cancellationToken);

        return teams.Select(t => t.ToDto());
    }
}
