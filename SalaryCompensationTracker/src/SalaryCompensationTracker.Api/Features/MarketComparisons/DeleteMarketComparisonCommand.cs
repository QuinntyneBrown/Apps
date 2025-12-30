using SalaryCompensationTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SalaryCompensationTracker.Api.Features.MarketComparisons;

public record DeleteMarketComparisonCommand : IRequest<bool>
{
    public Guid MarketComparisonId { get; init; }
}

public class DeleteMarketComparisonCommandHandler : IRequestHandler<DeleteMarketComparisonCommand, bool>
{
    private readonly ISalaryCompensationTrackerContext _context;
    private readonly ILogger<DeleteMarketComparisonCommandHandler> _logger;

    public DeleteMarketComparisonCommandHandler(
        ISalaryCompensationTrackerContext context,
        ILogger<DeleteMarketComparisonCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteMarketComparisonCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting market comparison {MarketComparisonId}", request.MarketComparisonId);

        var marketComparison = await _context.MarketComparisons
            .FirstOrDefaultAsync(m => m.MarketComparisonId == request.MarketComparisonId, cancellationToken);

        if (marketComparison == null)
        {
            _logger.LogWarning("Market comparison {MarketComparisonId} not found", request.MarketComparisonId);
            return false;
        }

        _context.MarketComparisons.Remove(marketComparison);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted market comparison {MarketComparisonId}", request.MarketComparisonId);

        return true;
    }
}
