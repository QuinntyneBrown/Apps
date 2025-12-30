// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BillPaymentScheduler.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BillPaymentScheduler.Api.Features.Bills;

public class GetBills
{
    public record Query : IRequest<List<BillDto>>;

    public class Handler : IRequestHandler<Query, List<BillDto>>
    {
        private readonly IBillPaymentSchedulerContext _context;

        public Handler(IBillPaymentSchedulerContext context)
        {
            _context = context;
        }

        public async Task<List<BillDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.Bills
                .Include(b => b.Payee)
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
                .ToListAsync(cancellationToken);
        }
    }
}
