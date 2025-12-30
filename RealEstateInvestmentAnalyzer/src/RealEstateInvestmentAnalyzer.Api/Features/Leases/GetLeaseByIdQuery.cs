using RealEstateInvestmentAnalyzer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RealEstateInvestmentAnalyzer.Api.Features.Leases;

public record GetLeaseByIdQuery : IRequest<LeaseDto?>
{
    public Guid LeaseId { get; init; }
}

public class GetLeaseByIdQueryHandler : IRequestHandler<GetLeaseByIdQuery, LeaseDto?>
{
    private readonly IRealEstateInvestmentAnalyzerContext _context;
    private readonly ILogger<GetLeaseByIdQueryHandler> _logger;

    public GetLeaseByIdQueryHandler(
        IRealEstateInvestmentAnalyzerContext context,
        ILogger<GetLeaseByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<LeaseDto?> Handle(GetLeaseByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting lease {LeaseId}", request.LeaseId);

        var lease = await _context.Leases
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.LeaseId == request.LeaseId, cancellationToken);

        return lease?.ToDto();
    }
}
