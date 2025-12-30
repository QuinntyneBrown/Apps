using RealEstateInvestmentAnalyzer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RealEstateInvestmentAnalyzer.Api.Features.Leases;

public record GetLeasesQuery : IRequest<IEnumerable<LeaseDto>>
{
    public Guid? PropertyId { get; init; }
    public bool? ActiveOnly { get; init; }
}

public class GetLeasesQueryHandler : IRequestHandler<GetLeasesQuery, IEnumerable<LeaseDto>>
{
    private readonly IRealEstateInvestmentAnalyzerContext _context;
    private readonly ILogger<GetLeasesQueryHandler> _logger;

    public GetLeasesQueryHandler(
        IRealEstateInvestmentAnalyzerContext context,
        ILogger<GetLeasesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<LeaseDto>> Handle(GetLeasesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting leases for property {PropertyId}", request.PropertyId);

        var query = _context.Leases.AsNoTracking();

        if (request.PropertyId.HasValue)
        {
            query = query.Where(l => l.PropertyId == request.PropertyId.Value);
        }

        if (request.ActiveOnly == true)
        {
            query = query.Where(l => l.IsActive);
        }

        var leases = await query
            .OrderByDescending(l => l.StartDate)
            .ToListAsync(cancellationToken);

        return leases.Select(l => l.ToDto());
    }
}
