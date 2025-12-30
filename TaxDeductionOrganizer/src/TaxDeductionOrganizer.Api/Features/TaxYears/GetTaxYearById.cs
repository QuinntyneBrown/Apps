// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using TaxDeductionOrganizer.Core;

namespace TaxDeductionOrganizer.Api.Features.TaxYears;

public class GetTaxYearById
{
    public class Query : IRequest<TaxYearDto>
    {
        public Guid TaxYearId { get; set; }
    }

    public class Handler : IRequestHandler<Query, TaxYearDto>
    {
        private readonly ITaxDeductionOrganizerContext _context;

        public Handler(ITaxDeductionOrganizerContext context)
        {
            _context = context;
        }

        public async Task<TaxYearDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var taxYear = await _context.TaxYears
                .FirstOrDefaultAsync(t => t.TaxYearId == request.TaxYearId, cancellationToken)
                ?? throw new InvalidOperationException($"TaxYear with ID {request.TaxYearId} not found.");

            return taxYear.ToDto();
        }
    }
}
