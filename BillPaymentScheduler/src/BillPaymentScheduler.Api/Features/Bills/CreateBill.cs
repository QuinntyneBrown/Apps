// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BillPaymentScheduler.Core;
using MediatR;

namespace BillPaymentScheduler.Api.Features.Bills;

public class CreateBill
{
    public record Command : IRequest<BillDto>
    {
        public Guid PayeeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public BillingFrequency BillingFrequency { get; set; }
        public BillStatus Status { get; set; }
        public bool IsAutoPay { get; set; }
        public string? Notes { get; set; }
    }

    public class Handler : IRequestHandler<Command, BillDto>
    {
        private readonly IBillPaymentSchedulerContext _context;

        public Handler(IBillPaymentSchedulerContext context)
        {
            _context = context;
        }

        public async Task<BillDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var bill = new Bill
            {
                BillId = Guid.NewGuid(),
                PayeeId = request.PayeeId,
                Name = request.Name,
                Amount = request.Amount,
                DueDate = request.DueDate,
                BillingFrequency = request.BillingFrequency,
                Status = request.Status,
                IsAutoPay = request.IsAutoPay,
                Notes = request.Notes
            };

            _context.Bills.Add(bill);
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
