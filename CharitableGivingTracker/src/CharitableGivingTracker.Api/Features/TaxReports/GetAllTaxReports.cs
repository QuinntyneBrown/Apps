// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CharitableGivingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CharitableGivingTracker.Api.Features.TaxReports;

public class GetAllTaxReports
{
    public record Query : IRequest<List<TaxReportDto>>;

    public class Handler : IRequestHandler<Query, List<TaxReportDto>>
    {
        private readonly ICharitableGivingTrackerContext _context;

        public Handler(ICharitableGivingTrackerContext context)
        {
            _context = context;
        }

        public async Task<List<TaxReportDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var taxReports = await _context.TaxReports
                .OrderByDescending(t => t.TaxYear)
                .ToListAsync(cancellationToken);

            return taxReports.Select(t => new TaxReportDto
            {
                TaxReportId = t.TaxReportId,
                TaxYear = t.TaxYear,
                TotalCashDonations = t.TotalCashDonations,
                TotalNonCashDonations = t.TotalNonCashDonations,
                TotalDeductibleAmount = t.TotalDeductibleAmount,
                GeneratedDate = t.GeneratedDate,
                Notes = t.Notes
            }).ToList();
        }
    }
}
