using RealEstateInvestmentAnalyzer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RealEstateInvestmentAnalyzer.Api.Features.Leases;

public record DeleteLeaseCommand : IRequest<bool>
{
    public Guid LeaseId { get; init; }
}

public class DeleteLeaseCommandHandler : IRequestHandler<DeleteLeaseCommand, bool>
{
    private readonly IRealEstateInvestmentAnalyzerContext _context;
    private readonly ILogger<DeleteLeaseCommandHandler> _logger;

    public DeleteLeaseCommandHandler(
        IRealEstateInvestmentAnalyzerContext context,
        ILogger<DeleteLeaseCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteLeaseCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting lease {LeaseId}", request.LeaseId);

        var lease = await _context.Leases
            .FirstOrDefaultAsync(l => l.LeaseId == request.LeaseId, cancellationToken);

        if (lease == null)
        {
            _logger.LogWarning("Lease {LeaseId} not found", request.LeaseId);
            return false;
        }

        _context.Leases.Remove(lease);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted lease {LeaseId}", request.LeaseId);

        return true;
    }
}
