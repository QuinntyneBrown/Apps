using SportsTeamFollowingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SportsTeamFollowingTracker.Api.Features.Seasons;

public record GetSeasonByIdQuery : IRequest<SeasonDto?>
{
    public Guid SeasonId { get; init; }
}

public class GetSeasonByIdQueryHandler : IRequestHandler<GetSeasonByIdQuery, SeasonDto?>
{
    private readonly ISportsTeamFollowingTrackerContext _context;
    private readonly ILogger<GetSeasonByIdQueryHandler> _logger;

    public GetSeasonByIdQueryHandler(
        ISportsTeamFollowingTrackerContext context,
        ILogger<GetSeasonByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<SeasonDto?> Handle(GetSeasonByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting season {SeasonId}", request.SeasonId);

        var season = await _context.Seasons
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.SeasonId == request.SeasonId, cancellationToken);

        if (season == null)
        {
            _logger.LogWarning("Season {SeasonId} not found", request.SeasonId);
            return null;
        }

        return season.ToDto();
    }
}
