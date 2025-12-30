// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BillPaymentScheduler.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BillPaymentScheduler.Api.Features.Payees;

public class UpdatePayee
{
    public record Command : IRequest<PayeeDto?>
    {
        public Guid PayeeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? AccountNumber { get; set; }
        public string? Website { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Notes { get; set; }
    }

    public class Handler : IRequestHandler<Command, PayeeDto?>
    {
        private readonly IBillPaymentSchedulerContext _context;

        public Handler(IBillPaymentSchedulerContext context)
        {
            _context = context;
        }

        public async Task<PayeeDto?> Handle(Command request, CancellationToken cancellationToken)
        {
            var payee = await _context.Payees
                .FirstOrDefaultAsync(p => p.PayeeId == request.PayeeId, cancellationToken);

            if (payee == null)
                return null;

            payee.Name = request.Name;
            payee.AccountNumber = request.AccountNumber;
            payee.Website = request.Website;
            payee.PhoneNumber = request.PhoneNumber;
            payee.Notes = request.Notes;

            await _context.SaveChangesAsync(cancellationToken);

            return new PayeeDto
            {
                PayeeId = payee.PayeeId,
                Name = payee.Name,
                AccountNumber = payee.AccountNumber,
                Website = payee.Website,
                PhoneNumber = payee.PhoneNumber,
                Notes = payee.Notes
            };
        }
    }
}
