using SleepQualityTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SleepQualityTracker.Api.Features.SleepSessions;

public record GetSleepSessionsQuery : IRequest<IEnumerable<SleepSessionDto>>
{
    public Guid? UserId { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public SleepQuality? SleepQuality { get; init; }
    public bool? MeetsRecommendedDuration { get; init; }
}

public class GetSleepSessionsQueryHandler : IRequestHandler<GetSleepSessionsQuery, IEnumerable<SleepSessionDto>>
{
    private readonly ISleepQualityTrackerContext _context;
    private readonly ILogger<GetSleepSessionsQueryHandler> _logger;

    public GetSleepSessionsQueryHandler(
        ISleepQualityTrackerContext context,
        ILogger<GetSleepSessionsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<SleepSessionDto>> Handle(GetSleepSessionsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting sleep sessions for user {UserId}", request.UserId);

        var query = _context.SleepSessions.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(s => s.UserId == request.UserId.Value);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(s => s.Bedtime >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(s => s.Bedtime <= request.EndDate.Value);
        }

        if (request.SleepQuality.HasValue)
        {
            query = query.Where(s => s.SleepQuality == request.SleepQuality.Value);
        }

        var sleepSessions = await query
            .OrderByDescending(s => s.Bedtime)
            .ToListAsync(cancellationToken);

        var results = sleepSessions.Select(s => s.ToDto());

        if (request.MeetsRecommendedDuration.HasValue)
        {
            results = results.Where(s => s.MeetsRecommendedDuration == request.MeetsRecommendedDuration.Value);
        }

        return results;
    }
}
