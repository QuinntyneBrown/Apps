// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalLoanComparisonTool.Core;

namespace PersonalLoanComparisonTool.Api.Features.Loan;

public record GetLoansQuery() : IRequest<List<LoanDto>>;

public class GetLoansQueryHandler : IRequestHandler<GetLoansQuery, List<LoanDto>>
{
    private readonly IPersonalLoanComparisonToolContext _context;

    public GetLoansQueryHandler(IPersonalLoanComparisonToolContext context)
    {
        _context = context;
    }

    public async Task<List<LoanDto>> Handle(GetLoansQuery request, CancellationToken cancellationToken)
    {
        return await _context.Loans
            .Select(l => l.ToDto())
            .ToListAsync(cancellationToken);
    }
}
