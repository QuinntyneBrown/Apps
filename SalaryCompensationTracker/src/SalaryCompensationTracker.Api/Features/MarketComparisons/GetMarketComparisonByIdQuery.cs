using SalaryCompensationTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SalaryCompensationTracker.Api.Features.MarketComparisons;

public record GetMarketComparisonByIdQuery : IRequest<MarketComparisonDto?>
{
    public Guid MarketComparisonId { get; init; }
}

public class GetMarketComparisonByIdQueryHandler : IRequestHandler<GetMarketComparisonByIdQuery, MarketComparisonDto?>
{
    private readonly ISalaryCompensationTrackerContext _context;
    private readonly ILogger<GetMarketComparisonByIdQueryHandler> _logger;

    public GetMarketComparisonByIdQueryHandler(
        ISalaryCompensationTrackerContext context,
        ILogger<GetMarketComparisonByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MarketComparisonDto?> Handle(GetMarketComparisonByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting market comparison {MarketComparisonId}", request.MarketComparisonId);

        var marketComparison = await _context.MarketComparisons
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.MarketComparisonId == request.MarketComparisonId, cancellationToken);

        return marketComparison?.ToDto();
    }
}
