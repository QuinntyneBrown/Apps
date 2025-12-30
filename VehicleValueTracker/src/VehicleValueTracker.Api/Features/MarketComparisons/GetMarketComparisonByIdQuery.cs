using VehicleValueTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace VehicleValueTracker.Api.Features.MarketComparisons;

public record GetMarketComparisonByIdQuery : IRequest<MarketComparisonDto?>
{
    public Guid MarketComparisonId { get; init; }
}

public class GetMarketComparisonByIdQueryHandler : IRequestHandler<GetMarketComparisonByIdQuery, MarketComparisonDto?>
{
    private readonly IVehicleValueTrackerContext _context;
    private readonly ILogger<GetMarketComparisonByIdQueryHandler> _logger;

    public GetMarketComparisonByIdQueryHandler(
        IVehicleValueTrackerContext context,
        ILogger<GetMarketComparisonByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MarketComparisonDto?> Handle(GetMarketComparisonByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting market comparison {MarketComparisonId}", request.MarketComparisonId);

        var comparison = await _context.MarketComparisons
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.MarketComparisonId == request.MarketComparisonId, cancellationToken);

        if (comparison == null)
        {
            _logger.LogWarning("Market comparison {MarketComparisonId} not found", request.MarketComparisonId);
            return null;
        }

        return comparison.ToDto();
    }
}
