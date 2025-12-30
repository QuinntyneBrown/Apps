using SideHustleIncomeTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SideHustleIncomeTracker.Api.Features.TaxEstimates;

public record GetTaxEstimatesQuery : IRequest<IEnumerable<TaxEstimateDto>>
{
    public Guid? BusinessId { get; init; }
    public int? TaxYear { get; init; }
    public bool? IsPaid { get; init; }
}

public class GetTaxEstimatesQueryHandler : IRequestHandler<GetTaxEstimatesQuery, IEnumerable<TaxEstimateDto>>
{
    private readonly ISideHustleIncomeTrackerContext _context;
    private readonly ILogger<GetTaxEstimatesQueryHandler> _logger;

    public GetTaxEstimatesQueryHandler(
        ISideHustleIncomeTrackerContext context,
        ILogger<GetTaxEstimatesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<TaxEstimateDto>> Handle(GetTaxEstimatesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting tax estimates for business {BusinessId}", request.BusinessId);

        var query = _context.TaxEstimates.AsNoTracking();

        if (request.BusinessId.HasValue)
        {
            query = query.Where(t => t.BusinessId == request.BusinessId.Value);
        }

        if (request.TaxYear.HasValue)
        {
            query = query.Where(t => t.TaxYear == request.TaxYear.Value);
        }

        if (request.IsPaid.HasValue)
        {
            query = query.Where(t => t.IsPaid == request.IsPaid.Value);
        }

        var taxEstimates = await query
            .OrderByDescending(t => t.TaxYear)
            .ThenByDescending(t => t.Quarter)
            .ToListAsync(cancellationToken);

        return taxEstimates.Select(t => t.ToDto());
    }
}
