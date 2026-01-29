using Loans.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Loans.Api.Features;

public record GetLoanByIdQuery(Guid LoanId) : IRequest<LoanDto?>;

public class GetLoanByIdQueryHandler : IRequestHandler<GetLoanByIdQuery, LoanDto?>
{
    private readonly ILoansDbContext _context;

    public GetLoanByIdQueryHandler(ILoansDbContext context)
    {
        _context = context;
    }

    public async Task<LoanDto?> Handle(GetLoanByIdQuery request, CancellationToken cancellationToken)
    {
        var loan = await _context.Loans.AsNoTracking()
            .FirstOrDefaultAsync(l => l.LoanId == request.LoanId, cancellationToken);
        return loan?.ToDto();
    }
}
