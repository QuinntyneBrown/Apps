using Loans.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Loans.Api.Features;

public record GetLoansQuery : IRequest<IEnumerable<LoanDto>>;

public class GetLoansQueryHandler : IRequestHandler<GetLoansQuery, IEnumerable<LoanDto>>
{
    private readonly ILoansDbContext _context;

    public GetLoansQueryHandler(ILoansDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LoanDto>> Handle(GetLoansQuery request, CancellationToken cancellationToken)
    {
        var loans = await _context.Loans.AsNoTracking().ToListAsync(cancellationToken);
        return loans.Select(l => l.ToDto());
    }
}
