// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using TaxDeductionOrganizer.Core;

namespace TaxDeductionOrganizer.Api.Features.Deductions;

public class DeleteDeduction
{
    public class Command : IRequest<Unit>
    {
        public Guid DeductionId { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ITaxDeductionOrganizerContext _context;

        public Handler(ITaxDeductionOrganizerContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var deduction = await _context.Deductions
                .FirstOrDefaultAsync(d => d.DeductionId == request.DeductionId, cancellationToken)
                ?? throw new InvalidOperationException($"Deduction with ID {request.DeductionId} not found.");

            _context.Deductions.Remove(deduction);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
