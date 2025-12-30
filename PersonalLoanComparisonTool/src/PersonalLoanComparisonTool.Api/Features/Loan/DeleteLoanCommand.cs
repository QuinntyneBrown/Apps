// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalLoanComparisonTool.Core;

namespace PersonalLoanComparisonTool.Api.Features.Loan;

public record DeleteLoanCommand(Guid LoanId) : IRequest;

public class DeleteLoanCommandHandler : IRequestHandler<DeleteLoanCommand>
{
    private readonly IPersonalLoanComparisonToolContext _context;

    public DeleteLoanCommandHandler(IPersonalLoanComparisonToolContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = await _context.Loans
            .FirstOrDefaultAsync(l => l.LoanId == request.LoanId, cancellationToken);

        if (loan == null)
        {
            throw new InvalidOperationException($"Loan with ID {request.LoanId} not found");
        }

        _context.Loans.Remove(loan);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
