// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using MortgagePayoffOptimizer.Core;

namespace MortgagePayoffOptimizer.Api.Features.Payment;

/// <summary>
/// Command to delete a payment.
/// </summary>
public record DeletePaymentCommand : IRequest<Unit>
{
    public Guid PaymentId { get; set; }
}

/// <summary>
/// Handler for DeletePaymentCommand.
/// </summary>
public class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand, Unit>
{
    private readonly IMortgagePayoffOptimizerContext _context;

    public DeletePaymentCommandHandler(IMortgagePayoffOptimizerContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await _context.Payments
            .FirstOrDefaultAsync(p => p.PaymentId == request.PaymentId, cancellationToken);

        if (payment == null)
        {
            throw new Exception($"Payment with ID {request.PaymentId} not found.");
        }

        _context.Payments.Remove(payment);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
