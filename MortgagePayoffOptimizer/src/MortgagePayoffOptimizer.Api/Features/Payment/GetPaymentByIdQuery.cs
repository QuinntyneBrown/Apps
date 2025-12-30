// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using MortgagePayoffOptimizer.Core;

namespace MortgagePayoffOptimizer.Api.Features.Payment;

/// <summary>
/// Query to get a payment by ID.
/// </summary>
public record GetPaymentByIdQuery : IRequest<PaymentDto?>
{
    public Guid PaymentId { get; set; }
}

/// <summary>
/// Handler for GetPaymentByIdQuery.
/// </summary>
public class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, PaymentDto?>
{
    private readonly IMortgagePayoffOptimizerContext _context;

    public GetPaymentByIdQueryHandler(IMortgagePayoffOptimizerContext context)
    {
        _context = context;
    }

    public async Task<PaymentDto?> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
    {
        var payment = await _context.Payments
            .FirstOrDefaultAsync(p => p.PaymentId == request.PaymentId, cancellationToken);

        return payment?.ToDto();
    }
}
