using SportsTeamFollowingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SportsTeamFollowingTracker.Api.Features.Seasons;

public record GetSeasonsQuery : IRequest<IEnumerable<SeasonDto>>
{
    public Guid? UserId { get; init; }
    public Guid? TeamId { get; init; }
    public int? Year { get; init; }
}

public class GetSeasonsQueryHandler : IRequestHandler<GetSeasonsQuery, IEnumerable<SeasonDto>>
{
    private readonly ISportsTeamFollowingTrackerContext _context;
    private readonly ILogger<GetSeasonsQueryHandler> _logger;

    public GetSeasonsQueryHandler(
        ISportsTeamFollowingTrackerContext context,
        ILogger<GetSeasonsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<SeasonDto>> Handle(GetSeasonsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting seasons for user {UserId}, team {TeamId}", request.UserId, request.TeamId);

        var query = _context.Seasons.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(s => s.UserId == request.UserId.Value);
        }

        if (request.TeamId.HasValue)
        {
            query = query.Where(s => s.TeamId == request.TeamId.Value);
        }

        if (request.Year.HasValue)
        {
            query = query.Where(s => s.Year == request.Year.Value);
        }

        var seasons = await query
            .OrderByDescending(s => s.Year)
            .ThenByDescending(s => s.CreatedAt)
            .ToListAsync(cancellationToken);

        return seasons.Select(s => s.ToDto());
    }
}
