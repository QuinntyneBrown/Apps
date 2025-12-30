// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalLoanComparisonTool.Core;

namespace PersonalLoanComparisonTool.Api.Features.Loan;

public record CreateLoanCommand(
    string Name,
    LoanType LoanType,
    decimal RequestedAmount,
    string Purpose,
    int CreditScore,
    string? Notes
) : IRequest<LoanDto>;

public class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, LoanDto>
{
    private readonly IPersonalLoanComparisonToolContext _context;

    public CreateLoanCommandHandler(IPersonalLoanComparisonToolContext context)
    {
        _context = context;
    }

    public async Task<LoanDto> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = new Core.Loan
        {
            LoanId = Guid.NewGuid(),
            Name = request.Name,
            LoanType = request.LoanType,
            RequestedAmount = request.RequestedAmount,
            Purpose = request.Purpose,
            CreditScore = request.CreditScore,
            Notes = request.Notes
        };

        _context.Loans.Add(loan);
        await _context.SaveChangesAsync(cancellationToken);

        return loan.ToDto();
    }
}
