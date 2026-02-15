using Companies.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Companies.Api.Features;

public record GetCompaniesQuery : IRequest<IEnumerable<CompanyDto>>;

public class GetCompaniesQueryHandler : IRequestHandler<GetCompaniesQuery, IEnumerable<CompanyDto>>
{
    private readonly ICompaniesDbContext _context;

    public GetCompaniesQueryHandler(ICompaniesDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CompanyDto>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Companies
            .AsNoTracking()
            .Select(c => c.ToDto())
            .ToListAsync(cancellationToken);
    }
}
