// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BillPaymentScheduler.Core;
using MediatR;

namespace BillPaymentScheduler.Api.Features.Payments;

public class CreatePayment
{
    public record Command : IRequest<PaymentDto>
    {
        public Guid BillId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? ConfirmationNumber { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Notes { get; set; }
    }

    public class Handler : IRequestHandler<Command, PaymentDto>
    {
        private readonly IBillPaymentSchedulerContext _context;

        public Handler(IBillPaymentSchedulerContext context)
        {
            _context = context;
        }

        public async Task<PaymentDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var payment = new Payment
            {
                PaymentId = Guid.NewGuid(),
                BillId = request.BillId,
                Amount = request.Amount,
                PaymentDate = request.PaymentDate,
                ConfirmationNumber = request.ConfirmationNumber,
                PaymentMethod = request.PaymentMethod,
                Notes = request.Notes
            };

            _context.Payments.Add(payment);
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
