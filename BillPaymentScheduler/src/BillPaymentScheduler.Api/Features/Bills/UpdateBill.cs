// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BillPaymentScheduler.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BillPaymentScheduler.Api.Features.Bills;

public class UpdateBill
{
    public record Command : IRequest<BillDto?>
    {
        public Guid BillId { get; set; }
        public Guid PayeeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public BillingFrequency BillingFrequency { get; set; }
        public BillStatus Status { get; set; }
        public bool IsAutoPay { get; set; }
        public string? Notes { get; set; }
    }

    public class Handler : IRequestHandler<Command, BillDto?>
    {
        private readonly IBillPaymentSchedulerContext _context;

        public Handler(IBillPaymentSchedulerContext context)
        {
            _context = context;
        }

        public async Task<BillDto?> Handle(Command request, CancellationToken cancellationToken)
        {
            var bill = await _context.Bills
                .FirstOrDefaultAsync(b => b.BillId == request.BillId, cancellationToken);

            if (bill == null)
                return null;

            bill.PayeeId = request.PayeeId;
            bill.Name = request.Name;
            bill.Amount = request.Amount;
            bill.DueDate = request.DueDate;
            bill.BillingFrequency = request.BillingFrequency;
            bill.Status = request.Status;
            bill.IsAutoPay = request.IsAutoPay;
            bill.Notes = request.Notes;

            await _context.SaveChangesAsync(cancellationToken);

            return new BillDto
            {
                BillId = bill.BillId,
                PayeeId = bill.PayeeId,
                Name = bill.Name,
                Amount = bill.Amount,
                DueDate = bill.DueDate,
                BillingFrequency = bill.BillingFrequency,
                Status = bill.Status,
                IsAutoPay = bill.IsAutoPay,
                Notes = bill.Notes
            };
        }
    }
}
