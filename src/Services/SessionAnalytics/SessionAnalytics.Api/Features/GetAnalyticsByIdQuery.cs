using SessionAnalytics.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SessionAnalytics.Api.Features;

public record GetAnalyticsByIdQuery : IRequest<AnalyticsDto?>
{
    public Guid AnalyticsId { get; init; }
}

public class GetAnalyticsByIdQueryHandler : IRequestHandler<GetAnalyticsByIdQuery, AnalyticsDto?>
{
    private readonly ISessionAnalyticsDbContext _context;

    public GetAnalyticsByIdQueryHandler(ISessionAnalyticsDbContext context)
    {
        _context = context;
    }

    public async Task<AnalyticsDto?> Handle(GetAnalyticsByIdQuery request, CancellationToken cancellationToken)
    {
        var analytics = await _context.Analytics
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.AnalyticsId == request.AnalyticsId, cancellationToken);

        if (analytics == null) return null;

        return new AnalyticsDto
        {
            AnalyticsId = analytics.AnalyticsId,
            UserId = analytics.UserId,
            Date = analytics.Date,
            TotalSessions = analytics.TotalSessions,
            TotalFocusMinutes = analytics.TotalFocusMinutes,
            TotalDistractions = analytics.TotalDistractions,
            AverageSessionLength = analytics.AverageSessionLength,
            UpdatedAt = analytics.UpdatedAt
        };
    }
}
