using Loans.Core.Models;

namespace Loans.Api.Features;

public record LoanDto(
    Guid LoanId,
    Guid UserId,
    string Name,
    string LoanType,
    decimal RequestedAmount,
    string Purpose,
    int CreditScore,
    string? Notes,
    DateTime CreatedAt);

public static class LoanExtensions
{
    public static LoanDto ToDto(this Loan loan)
    {
        return new LoanDto(
            loan.LoanId,
            loan.UserId,
            loan.Name,
            loan.LoanType,
            loan.RequestedAmount,
            loan.Purpose,
            loan.CreditScore,
            loan.Notes,
            loan.CreatedAt);
    }
}
