// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using TaxDeductionOrganizer.Core;

namespace TaxDeductionOrganizer.Api.Features.Receipts;

public class UpdateReceipt
{
    public class Command : IRequest<ReceiptDto>
    {
        public Guid ReceiptId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
        public string? Notes { get; set; }
    }

    public class Handler : IRequestHandler<Command, ReceiptDto>
    {
        private readonly ITaxDeductionOrganizerContext _context;

        public Handler(ITaxDeductionOrganizerContext context)
        {
            _context = context;
        }

        public async Task<ReceiptDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var receipt = await _context.Receipts
                .FirstOrDefaultAsync(r => r.ReceiptId == request.ReceiptId, cancellationToken)
                ?? throw new InvalidOperationException($"Receipt with ID {request.ReceiptId} not found.");

            receipt.FileName = request.FileName;
            receipt.FileUrl = request.FileUrl;
            receipt.Notes = request.Notes;

            await _context.SaveChangesAsync(cancellationToken);

            return receipt.ToDto();
        }
    }
}
