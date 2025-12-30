// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BillPaymentScheduler.Core;
using MediatR;

namespace BillPaymentScheduler.Api.Features.Payees;

public class CreatePayee
{
    public record Command : IRequest<PayeeDto>
    {
        public string Name { get; set; } = string.Empty;
        public string? AccountNumber { get; set; }
        public string? Website { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Notes { get; set; }
    }

    public class Handler : IRequestHandler<Command, PayeeDto>
    {
        private readonly IBillPaymentSchedulerContext _context;

        public Handler(IBillPaymentSchedulerContext context)
        {
            _context = context;
        }

        public async Task<PayeeDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var payee = new Payee
            {
                PayeeId = Guid.NewGuid(),
                Name = request.Name,
                AccountNumber = request.AccountNumber,
                Website = request.Website,
                PhoneNumber = request.PhoneNumber,
                Notes = request.Notes
            };

            _context.Payees.Add(payee);
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
