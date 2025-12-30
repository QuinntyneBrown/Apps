// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using TaxDeductionOrganizer.Core;

namespace TaxDeductionOrganizer.Api.Features.Receipts;

public class CreateReceipt
{
    public class Command : IRequest<ReceiptDto>
    {
        public Guid DeductionId { get; set; }
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
            var receipt = new Receipt
            {
                ReceiptId = Guid.NewGuid(),
                DeductionId = request.DeductionId,
                FileName = request.FileName,
                FileUrl = request.FileUrl,
                UploadDate = DateTime.UtcNow,
                Notes = request.Notes
            };

            _context.Receipts.Add(receipt);

            // Mark the deduction as having a receipt
            var deduction = await _context.Deductions
                .FirstOrDefaultAsync(d => d.DeductionId == request.DeductionId, cancellationToken);
            if (deduction != null)
            {
                deduction.AttachReceipt();
            }

            await _context.SaveChangesAsync(cancellationToken);

            return receipt.ToDto();
        }
    }
}
