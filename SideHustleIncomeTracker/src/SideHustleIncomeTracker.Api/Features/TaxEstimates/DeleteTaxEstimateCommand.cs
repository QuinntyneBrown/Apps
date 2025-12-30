using SideHustleIncomeTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SideHustleIncomeTracker.Api.Features.TaxEstimates;

public record DeleteTaxEstimateCommand : IRequest<bool>
{
    public Guid TaxEstimateId { get; init; }
}

public class DeleteTaxEstimateCommandHandler : IRequestHandler<DeleteTaxEstimateCommand, bool>
{
    private readonly ISideHustleIncomeTrackerContext _context;
    private readonly ILogger<DeleteTaxEstimateCommandHandler> _logger;

    public DeleteTaxEstimateCommandHandler(
        ISideHustleIncomeTrackerContext context,
        ILogger<DeleteTaxEstimateCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteTaxEstimateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting tax estimate {TaxEstimateId}", request.TaxEstimateId);

        var taxEstimate = await _context.TaxEstimates
            .FirstOrDefaultAsync(t => t.TaxEstimateId == request.TaxEstimateId, cancellationToken);

        if (taxEstimate == null)
        {
            _logger.LogWarning("Tax estimate {TaxEstimateId} not found", request.TaxEstimateId);
            return false;
        }

        _context.TaxEstimates.Remove(taxEstimate);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted tax estimate {TaxEstimateId}", request.TaxEstimateId);

        return true;
    }
}
