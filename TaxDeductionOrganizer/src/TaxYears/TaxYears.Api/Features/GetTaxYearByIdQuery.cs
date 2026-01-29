using TaxYears.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TaxYears.Api.Features;

public record GetTaxYearByIdQuery(Guid TaxYearId) : IRequest<TaxYearDto?>;

public class GetTaxYearByIdQueryHandler : IRequestHandler<GetTaxYearByIdQuery, TaxYearDto?>
{
    private readonly ITaxYearsDbContext _context;

    public GetTaxYearByIdQueryHandler(ITaxYearsDbContext context)
    {
        _context = context;
    }

    public async Task<TaxYearDto?> Handle(GetTaxYearByIdQuery request, CancellationToken cancellationToken)
    {
        var taxYear = await _context.TaxYears
            .FirstOrDefaultAsync(t => t.TaxYearId == request.TaxYearId, cancellationToken);
        return taxYear?.ToDto();
    }
}
