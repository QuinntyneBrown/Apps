using TaxYears.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TaxYears.Api.Features;

public record GetTaxYearsQuery : IRequest<IEnumerable<TaxYearDto>>;

public class GetTaxYearsQueryHandler : IRequestHandler<GetTaxYearsQuery, IEnumerable<TaxYearDto>>
{
    private readonly ITaxYearsDbContext _context;

    public GetTaxYearsQueryHandler(ITaxYearsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaxYearDto>> Handle(GetTaxYearsQuery request, CancellationToken cancellationToken)
    {
        var taxYears = await _context.TaxYears.ToListAsync(cancellationToken);
        return taxYears.Select(t => t.ToDto());
    }
}
