using PersonalHealthDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalHealthDashboard.Api.Features.HealthTrends;

public record GetHealthTrendsQuery : IRequest<IEnumerable<HealthTrendDto>>
{
    public Guid? UserId { get; init; }
    public string? MetricName { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public string? TrendDirection { get; init; }
}

public class GetHealthTrendsQueryHandler : IRequestHandler<GetHealthTrendsQuery, IEnumerable<HealthTrendDto>>
{
    private readonly IPersonalHealthDashboardContext _context;
    private readonly ILogger<GetHealthTrendsQueryHandler> _logger;

    public GetHealthTrendsQueryHandler(
        IPersonalHealthDashboardContext context,
        ILogger<GetHealthTrendsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<HealthTrendDto>> Handle(GetHealthTrendsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting health trends for user {UserId}", request.UserId);

        var query = _context.HealthTrends.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(h => h.UserId == request.UserId.Value);
        }

        if (!string.IsNullOrEmpty(request.MetricName))
        {
            query = query.Where(h => h.MetricName == request.MetricName);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(h => h.StartDate >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(h => h.EndDate <= request.EndDate.Value);
        }

        if (!string.IsNullOrEmpty(request.TrendDirection))
        {
            query = query.Where(h => h.TrendDirection == request.TrendDirection);
        }

        var healthTrends = await query
            .OrderByDescending(h => h.CreatedAt)
            .ToListAsync(cancellationToken);

        return healthTrends.Select(h => h.ToDto());
    }
}
