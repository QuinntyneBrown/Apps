using Loans.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Loans.Api.Features;

public record UpdateLoanCommand(
    Guid LoanId,
    string Name,
    string LoanType,
    decimal RequestedAmount,
    string Purpose,
    int CreditScore,
    string? Notes) : IRequest<LoanDto?>;

public class UpdateLoanCommandHandler : IRequestHandler<UpdateLoanCommand, LoanDto?>
{
    private readonly ILoansDbContext _context;
    private readonly ILogger<UpdateLoanCommandHandler> _logger;

    public UpdateLoanCommandHandler(ILoansDbContext context, ILogger<UpdateLoanCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<LoanDto?> Handle(UpdateLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = await _context.Loans.FirstOrDefaultAsync(l => l.LoanId == request.LoanId, cancellationToken);
        if (loan == null) return null;

        loan.Name = request.Name;
        loan.LoanType = request.LoanType;
        loan.RequestedAmount = request.RequestedAmount;
        loan.Purpose = request.Purpose;
        loan.CreditScore = request.CreditScore;
        loan.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Loan updated: {LoanId}", loan.LoanId);

        return loan.ToDto();
    }
}
