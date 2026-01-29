using Companies.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Companies.Api.Features;

public record GetCompanyByIdQuery(Guid CompanyId) : IRequest<CompanyDto?>;

public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, CompanyDto?>
{
    private readonly ICompaniesDbContext _context;

    public GetCompanyByIdQueryHandler(ICompaniesDbContext context)
    {
        _context = context;
    }

    public async Task<CompanyDto?> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
    {
        var company = await _context.Companies
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CompanyId == request.CompanyId, cancellationToken);

        return company?.ToDto();
    }
}
