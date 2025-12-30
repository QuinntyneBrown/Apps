// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BillPaymentScheduler.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BillPaymentScheduler.Api.Features.Payees;

public class GetPayeeById
{
    public record Query(Guid PayeeId) : IRequest<PayeeDto?>;

    public class Handler : IRequestHandler<Query, PayeeDto?>
    {
        private readonly IBillPaymentSchedulerContext _context;

        public Handler(IBillPaymentSchedulerContext context)
        {
            _context = context;
        }

        public async Task<PayeeDto?> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.Payees
                .Where(p => p.PayeeId == request.PayeeId)
                .Select(p => new PayeeDto
                {
                    PayeeId = p.PayeeId,
                    Name = p.Name,
                    AccountNumber = p.AccountNumber,
                    Website = p.Website,
                    PhoneNumber = p.PhoneNumber,
                    Notes = p.Notes
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
