// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BillPaymentScheduler.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BillPaymentScheduler.Api.Features.Bills;

public class DeleteBill
{
    public record Command(Guid BillId) : IRequest<bool>;

    public class Handler : IRequestHandler<Command, bool>
    {
        private readonly IBillPaymentSchedulerContext _context;

        public Handler(IBillPaymentSchedulerContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var bill = await _context.Bills
                .FirstOrDefaultAsync(b => b.BillId == request.BillId, cancellationToken);

            if (bill == null)
                return false;

            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
