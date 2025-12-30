// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BillPaymentScheduler.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BillPaymentScheduler.Api.Features.Payees;

public class GetPayees
{
    public record Query : IRequest<List<PayeeDto>>;

    public class Handler : IRequestHandler<Query, List<PayeeDto>>
    {
        private readonly IBillPaymentSchedulerContext _context;

        public Handler(IBillPaymentSchedulerContext context)
        {
            _context = context;
        }

        public async Task<List<PayeeDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.Payees
                .Select(p => new PayeeDto
                {
                    PayeeId = p.PayeeId,
                    Name = p.Name,
                    AccountNumber = p.AccountNumber,
                    Website = p.Website,
                    PhoneNumber = p.PhoneNumber,
                    Notes = p.Notes
                })
                .ToListAsync(cancellationToken);
        }
    }
}
