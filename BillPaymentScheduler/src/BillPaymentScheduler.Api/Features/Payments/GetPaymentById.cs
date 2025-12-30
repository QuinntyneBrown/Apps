// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BillPaymentScheduler.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BillPaymentScheduler.Api.Features.Payments;

public class GetPaymentById
{
    public record Query(Guid PaymentId) : IRequest<PaymentDto?>;

    public class Handler : IRequestHandler<Query, PaymentDto?>
    {
        private readonly IBillPaymentSchedulerContext _context;

        public Handler(IBillPaymentSchedulerContext context)
        {
            _context = context;
        }

        public async Task<PaymentDto?> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.Payments
                .Include(p => p.Bill)
                .Where(p => p.PaymentId == request.PaymentId)
                .Select(p => new PaymentDto
                {
                    PaymentId = p.PaymentId,
                    BillId = p.BillId,
                    Amount = p.Amount,
                    PaymentDate = p.PaymentDate,
                    ConfirmationNumber = p.ConfirmationNumber,
                    PaymentMethod = p.PaymentMethod,
                    Notes = p.Notes,
                    BillName = p.Bill != null ? p.Bill.Name : null
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
