using SideHustleIncomeTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SideHustleIncomeTracker.Api.Features.TaxEstimates;

public record GetTaxEstimateByIdQuery : IRequest<TaxEstimateDto?>
{
    public Guid TaxEstimateId { get; init; }
}

public class GetTaxEstimateByIdQueryHandler : IRequestHandler<GetTaxEstimateByIdQuery, TaxEstimateDto?>
{
    private readonly ISideHustleIncomeTrackerContext _context;
    private readonly ILogger<GetTaxEstimateByIdQueryHandler> _logger;

    public GetTaxEstimateByIdQueryHandler(
        ISideHustleIncomeTrackerContext context,
        ILogger<GetTaxEstimateByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TaxEstimateDto?> Handle(GetTaxEstimateByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting tax estimate {TaxEstimateId}", request.TaxEstimateId);

        var taxEstimate = await _context.TaxEstimates
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TaxEstimateId == request.TaxEstimateId, cancellationToken);

        return taxEstimate?.ToDto();
    }
}
