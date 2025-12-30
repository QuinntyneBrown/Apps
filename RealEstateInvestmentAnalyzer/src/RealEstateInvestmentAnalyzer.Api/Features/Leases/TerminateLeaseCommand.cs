using RealEstateInvestmentAnalyzer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RealEstateInvestmentAnalyzer.Api.Features.Leases;

public record TerminateLeaseCommand : IRequest<LeaseDto?>
{
    public Guid LeaseId { get; init; }
}

public class TerminateLeaseCommandHandler : IRequestHandler<TerminateLeaseCommand, LeaseDto?>
{
    private readonly IRealEstateInvestmentAnalyzerContext _context;
    private readonly ILogger<TerminateLeaseCommandHandler> _logger;

    public TerminateLeaseCommandHandler(
        IRealEstateInvestmentAnalyzerContext context,
        ILogger<TerminateLeaseCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<LeaseDto?> Handle(TerminateLeaseCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Terminating lease {LeaseId}", request.LeaseId);

        var lease = await _context.Leases
            .FirstOrDefaultAsync(l => l.LeaseId == request.LeaseId, cancellationToken);

        if (lease == null)
        {
            _logger.LogWarning("Lease {LeaseId} not found", request.LeaseId);
            return null;
        }

        lease.Terminate();
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Terminated lease {LeaseId}", request.LeaseId);

        return lease.ToDto();
    }
}
