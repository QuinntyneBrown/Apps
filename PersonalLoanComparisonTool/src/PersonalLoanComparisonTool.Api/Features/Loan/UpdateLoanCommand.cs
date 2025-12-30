// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalLoanComparisonTool.Core;

namespace PersonalLoanComparisonTool.Api.Features.Loan;

public record UpdateLoanCommand(
    Guid LoanId,
    string Name,
    LoanType LoanType,
    decimal RequestedAmount,
    string Purpose,
    int CreditScore,
    string? Notes
) : IRequest<LoanDto>;

public class UpdateLoanCommandHandler : IRequestHandler<UpdateLoanCommand, LoanDto>
{
    private readonly IPersonalLoanComparisonToolContext _context;

    public UpdateLoanCommandHandler(IPersonalLoanComparisonToolContext context)
    {
        _context = context;
    }

    public async Task<LoanDto> Handle(UpdateLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = await _context.Loans
            .FirstOrDefaultAsync(l => l.LoanId == request.LoanId, cancellationToken);

        if (loan == null)
        {
            throw new InvalidOperationException($"Loan with ID {request.LoanId} not found");
        }

        loan.Name = request.Name;
        loan.LoanType = request.LoanType;
        loan.RequestedAmount = request.RequestedAmount;
        loan.Purpose = request.Purpose;
        loan.CreditScore = request.CreditScore;
        loan.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return loan.ToDto();
    }
}
