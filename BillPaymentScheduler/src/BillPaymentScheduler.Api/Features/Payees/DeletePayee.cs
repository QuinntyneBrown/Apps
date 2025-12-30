// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BillPaymentScheduler.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BillPaymentScheduler.Api.Features.Payees;

public class DeletePayee
{
    public record Command(Guid PayeeId) : IRequest<bool>;

    public class Handler : IRequestHandler<Command, bool>
    {
        private readonly IBillPaymentSchedulerContext _context;

        public Handler(IBillPaymentSchedulerContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var payee = await _context.Payees
                .FirstOrDefaultAsync(p => p.PayeeId == request.PayeeId, cancellationToken);

            if (payee == null)
                return false;

            _context.Payees.Remove(payee);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
