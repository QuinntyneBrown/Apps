using VehicleValueTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace VehicleValueTracker.Api.Features.MarketComparisons;

public record GetMarketComparisonsQuery : IRequest<IEnumerable<MarketComparisonDto>>
{
    public Guid? VehicleId { get; init; }
    public string? ListingSource { get; init; }
    public bool? IsActive { get; init; }
    public DateTime? FromDate { get; init; }
    public DateTime? ToDate { get; init; }
}

public class GetMarketComparisonsQueryHandler : IRequestHandler<GetMarketComparisonsQuery, IEnumerable<MarketComparisonDto>>
{
    private readonly IVehicleValueTrackerContext _context;
    private readonly ILogger<GetMarketComparisonsQueryHandler> _logger;

    public GetMarketComparisonsQueryHandler(
        IVehicleValueTrackerContext context,
        ILogger<GetMarketComparisonsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<MarketComparisonDto>> Handle(GetMarketComparisonsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting market comparisons");

        var query = _context.MarketComparisons.AsNoTracking();

        if (request.VehicleId.HasValue)
        {
            query = query.Where(m => m.VehicleId == request.VehicleId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.ListingSource))
        {
            query = query.Where(m => m.ListingSource.Contains(request.ListingSource));
        }

        if (request.IsActive.HasValue)
        {
            query = query.Where(m => m.IsActive == request.IsActive.Value);
        }

        if (request.FromDate.HasValue)
        {
            query = query.Where(m => m.ComparisonDate >= request.FromDate.Value);
        }

        if (request.ToDate.HasValue)
        {
            query = query.Where(m => m.ComparisonDate <= request.ToDate.Value);
        }

        var comparisons = await query
            .OrderByDescending(m => m.ComparisonDate)
            .ToListAsync(cancellationToken);

        return comparisons.Select(m => m.ToDto());
    }
}
