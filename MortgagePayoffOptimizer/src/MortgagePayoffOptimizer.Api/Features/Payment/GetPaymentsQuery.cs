// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using MortgagePayoffOptimizer.Core;

namespace MortgagePayoffOptimizer.Api.Features.Payment;

/// <summary>
/// Query to get all payments.
/// </summary>
public record GetPaymentsQuery : IRequest<List<PaymentDto>>;

/// <summary>
/// Handler for GetPaymentsQuery.
/// </summary>
public class GetPaymentsQueryHandler : IRequestHandler<GetPaymentsQuery, List<PaymentDto>>
{
    private readonly IMortgagePayoffOptimizerContext _context;

    public GetPaymentsQueryHandler(IMortgagePayoffOptimizerContext context)
    {
        _context = context;
    }

    public async Task<List<PaymentDto>> Handle(GetPaymentsQuery request, CancellationToken cancellationToken)
    {
        var payments = await _context.Payments
            .OrderByDescending(p => p.PaymentDate)
            .ToListAsync(cancellationToken);

        return payments.Select(p => p.ToDto()).ToList();
    }
}
