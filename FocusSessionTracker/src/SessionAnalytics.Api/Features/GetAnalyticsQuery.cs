using SessionAnalytics.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SessionAnalytics.Api.Features;

public record GetAnalyticsQuery : IRequest<IEnumerable<AnalyticsDto>>
{
    public Guid? UserId { get; init; }
}

public class GetAnalyticsQueryHandler : IRequestHandler<GetAnalyticsQuery, IEnumerable<AnalyticsDto>>
{
    private readonly ISessionAnalyticsDbContext _context;

    public GetAnalyticsQueryHandler(ISessionAnalyticsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AnalyticsDto>> Handle(GetAnalyticsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Analytics.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(a => a.UserId == request.UserId.Value);
        }

        return await query
            .OrderByDescending(a => a.Date)
            .Select(a => new AnalyticsDto
            {
                AnalyticsId = a.AnalyticsId,
                UserId = a.UserId,
                Date = a.Date,
                TotalSessions = a.TotalSessions,
                TotalFocusMinutes = a.TotalFocusMinutes,
                TotalDistractions = a.TotalDistractions,
                AverageSessionLength = a.AverageSessionLength,
                UpdatedAt = a.UpdatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
