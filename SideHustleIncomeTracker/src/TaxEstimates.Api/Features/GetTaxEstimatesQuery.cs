using TaxEstimates.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TaxEstimates.Api.Features;

public record GetTaxEstimatesQuery(Guid TenantId, Guid UserId, int? Year) : IRequest<IEnumerable<TaxEstimateDto>>;

public class GetTaxEstimatesQueryHandler : IRequestHandler<GetTaxEstimatesQuery, IEnumerable<TaxEstimateDto>>
{
    private readonly ITaxEstimatesDbContext _context;

    public GetTaxEstimatesQueryHandler(ITaxEstimatesDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaxEstimateDto>> Handle(GetTaxEstimatesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.TaxEstimates
            .Where(t => t.TenantId == request.TenantId && t.UserId == request.UserId);

        if (request.Year.HasValue)
            query = query.Where(t => t.Year == request.Year.Value);

        return await query
            .Select(t => new TaxEstimateDto(t.TaxEstimateId, t.TenantId, t.UserId, t.BusinessId, t.Year, t.Quarter, t.TotalIncome, t.TotalExpenses, t.NetIncome, t.EstimatedTax, t.TaxRate, t.CalculatedAt))
            .ToListAsync(cancellationToken);
    }
}
