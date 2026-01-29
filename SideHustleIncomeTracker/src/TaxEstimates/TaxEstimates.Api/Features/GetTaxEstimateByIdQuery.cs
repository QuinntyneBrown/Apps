using TaxEstimates.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TaxEstimates.Api.Features;

public record GetTaxEstimateByIdQuery(Guid TaxEstimateId, Guid TenantId) : IRequest<TaxEstimateDto?>;

public class GetTaxEstimateByIdQueryHandler : IRequestHandler<GetTaxEstimateByIdQuery, TaxEstimateDto?>
{
    private readonly ITaxEstimatesDbContext _context;

    public GetTaxEstimateByIdQueryHandler(ITaxEstimatesDbContext context)
    {
        _context = context;
    }

    public async Task<TaxEstimateDto?> Handle(GetTaxEstimateByIdQuery request, CancellationToken cancellationToken)
    {
        var estimate = await _context.TaxEstimates
            .FirstOrDefaultAsync(t => t.TaxEstimateId == request.TaxEstimateId && t.TenantId == request.TenantId, cancellationToken);

        return estimate == null ? null : new TaxEstimateDto(estimate.TaxEstimateId, estimate.TenantId, estimate.UserId, estimate.BusinessId, estimate.Year, estimate.Quarter, estimate.TotalIncome, estimate.TotalExpenses, estimate.NetIncome, estimate.EstimatedTax, estimate.TaxRate, estimate.CalculatedAt);
    }
}
