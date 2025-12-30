// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using TaxDeductionOrganizer.Core;

namespace TaxDeductionOrganizer.Api.Features.TaxYears;

public class GetAllTaxYears
{
    public class Query : IRequest<List<TaxYearDto>>
    {
    }

    public class Handler : IRequestHandler<Query, List<TaxYearDto>>
    {
        private readonly ITaxDeductionOrganizerContext _context;

        public Handler(ITaxDeductionOrganizerContext context)
        {
            _context = context;
        }

        public async Task<List<TaxYearDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var taxYears = await _context.TaxYears
                .OrderByDescending(t => t.Year)
                .ToListAsync(cancellationToken);

            return taxYears.Select(t => t.ToDto()).ToList();
        }
    }
}
