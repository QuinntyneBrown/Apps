// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BillPaymentScheduler.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BillPaymentScheduler.Api.Features.Payments;

public class UpdatePayment
{
    public record Command : IRequest<PaymentDto?>
    {
        public Guid PaymentId { get; set; }
        public Guid BillId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? ConfirmationNumber { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Notes { get; set; }
    }

    public class Handler : IRequestHandler<Command, PaymentDto?>
    {
        private readonly IBillPaymentSchedulerContext _context;

        public Handler(IBillPaymentSchedulerContext context)
        {
            _context = context;
        }

        public async Task<PaymentDto?> Handle(Command request, CancellationToken cancellationToken)
        {
            var payment = await _context.Payments
                .FirstOrDefaultAsync(p => p.PaymentId == request.PaymentId, cancellationToken);

            if (payment == null)
                return null;

            payment.BillId = request.BillId;
            payment.Amount = request.Amount;
            payment.PaymentDate = request.PaymentDate;
            payment.ConfirmationNumber = request.ConfirmationNumber;
            payment.PaymentMethod = request.PaymentMethod;
            payment.Notes = request.Notes;

            await _context.SaveChangesAsync(cancellationToken);

            return new PaymentDto
            {
                PaymentId = payment.PaymentId,
                BillId = payment.BillId,
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                ConfirmationNumber = payment.ConfirmationNumber,
                PaymentMethod = payment.PaymentMethod,
                Notes = payment.Notes
            };
        }
    }
}
