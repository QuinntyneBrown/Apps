using RealEstateInvestmentAnalyzer.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace RealEstateInvestmentAnalyzer.Api.Features.Leases;

public record CreateLeaseCommand : IRequest<LeaseDto>
{
    public Guid PropertyId { get; init; }
    public string TenantName { get; init; } = string.Empty;
    public decimal MonthlyRent { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public decimal SecurityDeposit { get; init; }
    public string? Notes { get; init; }
}

public class CreateLeaseCommandHandler : IRequestHandler<CreateLeaseCommand, LeaseDto>
{
    private readonly IRealEstateInvestmentAnalyzerContext _context;
    private readonly ILogger<CreateLeaseCommandHandler> _logger;

    public CreateLeaseCommandHandler(
        IRealEstateInvestmentAnalyzerContext context,
        ILogger<CreateLeaseCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<LeaseDto> Handle(CreateLeaseCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating lease for property {PropertyId}, tenant: {TenantName}",
            request.PropertyId,
            request.TenantName);

        var lease = new Lease
        {
            LeaseId = Guid.NewGuid(),
            PropertyId = request.PropertyId,
            TenantName = request.TenantName,
            MonthlyRent = request.MonthlyRent,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            SecurityDeposit = request.SecurityDeposit,
            IsActive = true,
            Notes = request.Notes,
        };

        _context.Leases.Add(lease);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created lease {LeaseId}",
            lease.LeaseId);

        return lease.ToDto();
    }
}
