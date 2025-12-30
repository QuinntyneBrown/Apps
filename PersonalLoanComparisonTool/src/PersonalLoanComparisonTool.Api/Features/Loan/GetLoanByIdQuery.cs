// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalLoanComparisonTool.Core;

namespace PersonalLoanComparisonTool.Api.Features.Loan;

public record GetLoanByIdQuery(Guid LoanId) : IRequest<LoanDto?>;

public class GetLoanByIdQueryHandler : IRequestHandler<GetLoanByIdQuery, LoanDto?>
{
    private readonly IPersonalLoanComparisonToolContext _context;

    public GetLoanByIdQueryHandler(IPersonalLoanComparisonToolContext context)
    {
        _context = context;
    }

    public async Task<LoanDto?> Handle(GetLoanByIdQuery request, CancellationToken cancellationToken)
    {
        var loan = await _context.Loans
            .FirstOrDefaultAsync(l => l.LoanId == request.LoanId, cancellationToken);

        return loan?.ToDto();
    }
}
