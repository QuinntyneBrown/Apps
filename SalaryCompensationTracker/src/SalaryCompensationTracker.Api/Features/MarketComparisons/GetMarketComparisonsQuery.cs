using SalaryCompensationTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SalaryCompensationTracker.Api.Features.MarketComparisons;

public record GetMarketComparisonsQuery : IRequest<IEnumerable<MarketComparisonDto>>
{
    public Guid? UserId { get; init; }
    public string? JobTitle { get; init; }
    public string? Location { get; init; }
}

public class GetMarketComparisonsQueryHandler : IRequestHandler<GetMarketComparisonsQuery, IEnumerable<MarketComparisonDto>>
{
    private readonly ISalaryCompensationTrackerContext _context;
    private readonly ILogger<GetMarketComparisonsQueryHandler> _logger;

    public GetMarketComparisonsQueryHandler(
        ISalaryCompensationTrackerContext context,
        ILogger<GetMarketComparisonsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<MarketComparisonDto>> Handle(GetMarketComparisonsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting market comparisons for user {UserId}", request.UserId);

        var query = _context.MarketComparisons.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(m => m.UserId == request.UserId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.JobTitle))
        {
            query = query.Where(m => m.JobTitle.Contains(request.JobTitle));
        }

        if (!string.IsNullOrWhiteSpace(request.Location))
        {
            query = query.Where(m => m.Location.Contains(request.Location));
        }

        var marketComparisons = await query
            .OrderByDescending(m => m.ComparisonDate)
            .ToListAsync(cancellationToken);

        return marketComparisons.Select(m => m.ToDto());
    }
}
