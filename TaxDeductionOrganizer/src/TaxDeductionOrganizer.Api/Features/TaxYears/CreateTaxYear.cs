// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using TaxDeductionOrganizer.Core;

namespace TaxDeductionOrganizer.Api.Features.TaxYears;

public class CreateTaxYear
{
    public class Command : IRequest<TaxYearDto>
    {
        public int Year { get; set; }
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
            var taxYear = new TaxYear
            {
                TaxYearId = Guid.NewGuid(),
                Year = request.Year,
                IsFiled = false,
                TotalDeductions = 0,
                Notes = request.Notes
            };

            _context.TaxYears.Add(taxYear);
            await _context.SaveChangesAsync(cancellationToken);

            return taxYear.ToDto();
        }
    }
}
