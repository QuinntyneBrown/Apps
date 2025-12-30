// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using TaxDeductionOrganizer.Core;

namespace TaxDeductionOrganizer.Api.Features.Receipts;

public class GetReceiptById
{
    public class Query : IRequest<ReceiptDto>
    {
        public Guid ReceiptId { get; set; }
    }

    public class Handler : IRequestHandler<Query, ReceiptDto>
    {
        private readonly ITaxDeductionOrganizerContext _context;

        public Handler(ITaxDeductionOrganizerContext context)
        {
            _context = context;
        }

        public async Task<ReceiptDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var receipt = await _context.Receipts
                .FirstOrDefaultAsync(r => r.ReceiptId == request.ReceiptId, cancellationToken)
                ?? throw new InvalidOperationException($"Receipt with ID {request.ReceiptId} not found.");

            return receipt.ToDto();
        }
    }
}
