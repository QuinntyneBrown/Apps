// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using TaxDeductionOrganizer.Core;

namespace TaxDeductionOrganizer.Api.Features.TaxYears;

public class DeleteTaxYear
{
    public class Command : IRequest<Unit>
    {
        public Guid TaxYearId { get; set; }
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
            var taxYear = await _context.TaxYears
                .FirstOrDefaultAsync(t => t.TaxYearId == request.TaxYearId, cancellationToken)
                ?? throw new InvalidOperationException($"TaxYear with ID {request.TaxYearId} not found.");

            _context.TaxYears.Remove(taxYear);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
