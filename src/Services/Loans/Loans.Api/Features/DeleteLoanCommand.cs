using Loans.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Loans.Api.Features;

public record DeleteLoanCommand(Guid LoanId) : IRequest<bool>;

public class DeleteLoanCommandHandler : IRequestHandler<DeleteLoanCommand, bool>
{
    private readonly ILoansDbContext _context;
    private readonly ILogger<DeleteLoanCommandHandler> _logger;

    public DeleteLoanCommandHandler(ILoansDbContext context, ILogger<DeleteLoanCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = await _context.Loans.FirstOrDefaultAsync(l => l.LoanId == request.LoanId, cancellationToken);
        if (loan == null) return false;

        _context.Loans.Remove(loan);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Loan deleted: {LoanId}", request.LoanId);

        return true;
    }
}
