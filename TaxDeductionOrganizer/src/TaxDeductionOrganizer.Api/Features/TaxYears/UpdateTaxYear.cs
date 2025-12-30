// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using TaxDeductionOrganizer.Core;

namespace TaxDeductionOrganizer.Api.Features.TaxYears;

public class UpdateTaxYear
{
    public class Command : IRequest<TaxYearDto>
    {
        public Guid TaxYearId { get; set; }
        public int Year { get; set; }
        public bool IsFiled { get; set; }
        public DateTime? FilingDate { get; set; }
        public string? Notes { get; set; }
    }

    public class Handler : IRequestHandler<Command, TaxYearDto>
    {
        private readonly ITaxDeductionOrganizerContext _context;

        public Handler(ITaxDeductionOrganizerContext context)
        {
            _context = context;
        }

        public async Task<TaxYearDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var taxYear = await _context.TaxYears
                .Include(t => t.Deductions)
                .FirstOrDefaultAsync(t => t.TaxYearId == request.TaxYearId, cancellationToken)
                ?? throw new InvalidOperationException($"TaxYear with ID {request.TaxYearId} not found.");

            taxYear.Year = request.Year;
            taxYear.IsFiled = request.IsFiled;
            taxYear.FilingDate = request.FilingDate;
            taxYear.Notes = request.Notes;
            taxYear.CalculateTotalDeductions();

            await _context.SaveChangesAsync(cancellationToken);

            return taxYear.ToDto();
        }
    }
}
