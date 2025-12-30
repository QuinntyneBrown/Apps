// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CharitableGivingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CharitableGivingTracker.Api.Features.TaxReports;

public class GetTaxReportByYear
{
    public record Query(int TaxYear) : IRequest<TaxReportDto?>;

    public class Handler : IRequestHandler<Query, TaxReportDto?>
    {
        private readonly ICharitableGivingTrackerContext _context;

        public Handler(ICharitableGivingTrackerContext context)
        {
            _context = context;
        }

        public async Task<TaxReportDto?> Handle(Query request, CancellationToken cancellationToken)
        {
            var taxReport = await _context.TaxReports
                .FirstOrDefaultAsync(t => t.TaxYear == request.TaxYear, cancellationToken);

            if (taxReport == null)
                return null;

            return new TaxReportDto
            {
                TaxReportId = taxReport.TaxReportId,
                TaxYear = taxReport.TaxYear,
                TotalCashDonations = taxReport.TotalCashDonations,
                TotalNonCashDonations = taxReport.TotalNonCashDonations,
                TotalDeductibleAmount = taxReport.TotalDeductibleAmount,
                GeneratedDate = taxReport.GeneratedDate,
                Notes = taxReport.Notes
            };
        }
    }
}
