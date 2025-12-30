using SportsTeamFollowingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SportsTeamFollowingTracker.Api.Features.Games;

public record GetGamesQuery : IRequest<IEnumerable<GameDto>>
{
    public Guid? UserId { get; init; }
    public Guid? TeamId { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
}

public class GetGamesQueryHandler : IRequestHandler<GetGamesQuery, IEnumerable<GameDto>>
{
    private readonly ISportsTeamFollowingTrackerContext _context;
    private readonly ILogger<GetGamesQueryHandler> _logger;

    public GetGamesQueryHandler(
        ISportsTeamFollowingTrackerContext context,
        ILogger<GetGamesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<GameDto>> Handle(GetGamesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting games for user {UserId}, team {TeamId}", request.UserId, request.TeamId);

        var query = _context.Games
            .Include(g => g.Team)
            .AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(g => g.UserId == request.UserId.Value);
        }

        if (request.TeamId.HasValue)
        {
            query = query.Where(g => g.TeamId == request.TeamId.Value);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(g => g.GameDate >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(g => g.GameDate <= request.EndDate.Value);
        }

        var games = await query
            .OrderByDescending(g => g.GameDate)
            .ToListAsync(cancellationToken);

        return games.Select(g => g.ToDto());
    }
}
