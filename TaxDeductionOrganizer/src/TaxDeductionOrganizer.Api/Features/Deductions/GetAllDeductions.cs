// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using TaxDeductionOrganizer.Core;

namespace TaxDeductionOrganizer.Api.Features.Deductions;

public class GetAllDeductions
{
    public class Query : IRequest<List<DeductionDto>>
    {
        public Guid? TaxYearId { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<DeductionDto>>
    {
        private readonly ITaxDeductionOrganizerContext _context;

        public Handler(ITaxDeductionOrganizerContext context)
        {
            _context = context;
        }

        public async Task<List<DeductionDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _context.Deductions.AsQueryable();

            if (request.TaxYearId.HasValue)
            {
                query = query.Where(d => d.TaxYearId == request.TaxYearId.Value);
            }

            var deductions = await query
                .OrderByDescending(d => d.Date)
                .ToListAsync(cancellationToken);

            return deductions.Select(d => d.ToDto()).ToList();
        }
    }
}
