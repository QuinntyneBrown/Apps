using SportsTeamFollowingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SportsTeamFollowingTracker.Api.Features.Statistics;

public record GetStatisticsQuery : IRequest<IEnumerable<StatisticDto>>
{
    public Guid? UserId { get; init; }
    public Guid? TeamId { get; init; }
    public string? StatName { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
}

public class GetStatisticsQueryHandler : IRequestHandler<GetStatisticsQuery, IEnumerable<StatisticDto>>
{
    private readonly ISportsTeamFollowingTrackerContext _context;
    private readonly ILogger<GetStatisticsQueryHandler> _logger;

    public GetStatisticsQueryHandler(
        ISportsTeamFollowingTrackerContext context,
        ILogger<GetStatisticsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<StatisticDto>> Handle(GetStatisticsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting statistics for user {UserId}, team {TeamId}", request.UserId, request.TeamId);

        var query = _context.Statistics.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(s => s.UserId == request.UserId.Value);
        }

        if (request.TeamId.HasValue)
        {
            query = query.Where(s => s.TeamId == request.TeamId.Value);
        }

        if (!string.IsNullOrEmpty(request.StatName))
        {
            query = query.Where(s => s.StatName.Contains(request.StatName));
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(s => s.RecordedDate >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(s => s.RecordedDate <= request.EndDate.Value);
        }

        var statistics = await query
            .OrderByDescending(s => s.RecordedDate)
            .ToListAsync(cancellationToken);

        return statistics.Select(s => s.ToDto());
    }
}
