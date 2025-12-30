// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BillPaymentScheduler.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BillPaymentScheduler.Api.Features.Bills;

public class GetBillById
{
    public record Query(Guid BillId) : IRequest<BillDto?>;

    public class Handler : IRequestHandler<Query, BillDto?>
    {
        private readonly IBillPaymentSchedulerContext _context;

        public Handler(IBillPaymentSchedulerContext context)
        {
            _context = context;
        }

        public async Task<BillDto?> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.Bills
                .Include(b => b.Payee)
                .Where(b => b.BillId == request.BillId)
                .Select(b => new BillDto
                {
                    BillId = b.BillId,
                    PayeeId = b.PayeeId,
                    Name = b.Name,
                    Amount = b.Amount,
                    DueDate = b.DueDate,
                    BillingFrequency = b.BillingFrequency,
                    Status = b.Status,
                    IsAutoPay = b.IsAutoPay,
                    Notes = b.Notes,
                    PayeeName = b.Payee != null ? b.Payee.Name : null
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
