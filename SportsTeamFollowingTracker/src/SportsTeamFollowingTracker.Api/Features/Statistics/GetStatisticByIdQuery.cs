using SportsTeamFollowingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SportsTeamFollowingTracker.Api.Features.Statistics;

public record GetStatisticByIdQuery : IRequest<StatisticDto?>
{
    public Guid StatisticId { get; init; }
}

public class GetStatisticByIdQueryHandler : IRequestHandler<GetStatisticByIdQuery, StatisticDto?>
{
    private readonly ISportsTeamFollowingTrackerContext _context;
    private readonly ILogger<GetStatisticByIdQueryHandler> _logger;

    public GetStatisticByIdQueryHandler(
        ISportsTeamFollowingTrackerContext context,
        ILogger<GetStatisticByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<StatisticDto?> Handle(GetStatisticByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting statistic {StatisticId}", request.StatisticId);

        var statistic = await _context.Statistics
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.StatisticId == request.StatisticId, cancellationToken);

        if (statistic == null)
        {
            _logger.LogWarning("Statistic {StatisticId} not found", request.StatisticId);
            return null;
        }

        return statistic.ToDto();
    }
}
