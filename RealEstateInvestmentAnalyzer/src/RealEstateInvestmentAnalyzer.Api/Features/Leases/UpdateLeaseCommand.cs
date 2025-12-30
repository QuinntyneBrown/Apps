using RealEstateInvestmentAnalyzer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RealEstateInvestmentAnalyzer.Api.Features.Leases;

public record UpdateLeaseCommand : IRequest<LeaseDto?>
{
    public Guid LeaseId { get; init; }
    public Guid PropertyId { get; init; }
    public string TenantName { get; init; } = string.Empty;
    public decimal MonthlyRent { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public decimal SecurityDeposit { get; init; }
    public bool IsActive { get; init; }
    public string? Notes { get; init; }
}

public class UpdateLeaseCommandHandler : IRequestHandler<UpdateLeaseCommand, LeaseDto?>
{
    private readonly IRealEstateInvestmentAnalyzerContext _context;
    private readonly ILogger<UpdateLeaseCommandHandler> _logger;

    public UpdateLeaseCommandHandler(
        IRealEstateInvestmentAnalyzerContext context,
        ILogger<UpdateLeaseCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<LeaseDto?> Handle(UpdateLeaseCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating lease {LeaseId}", request.LeaseId);

        var lease = await _context.Leases
            .FirstOrDefaultAsync(l => l.LeaseId == request.LeaseId, cancellationToken);

        if (lease == null)
        {
            _logger.LogWarning("Lease {LeaseId} not found", request.LeaseId);
            return null;
        }

        lease.PropertyId = request.PropertyId;
        lease.TenantName = request.TenantName;
        lease.MonthlyRent = request.MonthlyRent;
        lease.StartDate = request.StartDate;
        lease.EndDate = request.EndDate;
        lease.SecurityDeposit = request.SecurityDeposit;
        lease.IsActive = request.IsActive;
        lease.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated lease {LeaseId}", request.LeaseId);

        return lease.ToDto();
    }
}
