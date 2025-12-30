using VehicleValueTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace VehicleValueTracker.Api.Features.MarketComparisons;

public record DeleteMarketComparisonCommand : IRequest<bool>
{
    public Guid MarketComparisonId { get; init; }
}

public class DeleteMarketComparisonCommandHandler : IRequestHandler<DeleteMarketComparisonCommand, bool>
{
    private readonly IVehicleValueTrackerContext _context;
    private readonly ILogger<DeleteMarketComparisonCommandHandler> _logger;

    public DeleteMarketComparisonCommandHandler(
        IVehicleValueTrackerContext context,
        ILogger<DeleteMarketComparisonCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteMarketComparisonCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting market comparison {MarketComparisonId}", request.MarketComparisonId);

        var comparison = await _context.MarketComparisons
            .FirstOrDefaultAsync(m => m.MarketComparisonId == request.MarketComparisonId, cancellationToken);

        if (comparison == null)
        {
            _logger.LogWarning("Market comparison {MarketComparisonId} not found", request.MarketComparisonId);
            return false;
        }

        _context.MarketComparisons.Remove(comparison);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted market comparison {MarketComparisonId}", request.MarketComparisonId);

        return true;
    }
}
