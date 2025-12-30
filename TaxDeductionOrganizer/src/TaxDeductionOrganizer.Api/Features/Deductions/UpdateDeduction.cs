// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using TaxDeductionOrganizer.Core;

namespace TaxDeductionOrganizer.Api.Features.Deductions;

public class UpdateDeduction
{
    public class Command : IRequest<DeductionDto>
    {
        public Guid DeductionId { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public DeductionCategory Category { get; set; }
        public string? Notes { get; set; }
    }

    public class Handler : IRequestHandler<Command, DeductionDto>
    {
        private readonly ITaxDeductionOrganizerContext _context;

        public Handler(ITaxDeductionOrganizerContext context)
        {
            _context = context;
        }

        public async Task<DeductionDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var deduction = await _context.Deductions
                .FirstOrDefaultAsync(d => d.DeductionId == request.DeductionId, cancellationToken)
                ?? throw new InvalidOperationException($"Deduction with ID {request.DeductionId} not found.");

            deduction.Description = request.Description;
            deduction.Amount = request.Amount;
            deduction.Date = request.Date;
            deduction.Category = request.Category;
            deduction.Notes = request.Notes;

            await _context.SaveChangesAsync(cancellationToken);

            return deduction.ToDto();
        }
    }
}
