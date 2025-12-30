// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CharitableGivingTracker.Core;
using MediatR;

namespace CharitableGivingTracker.Api.Features.TaxReports;

public class CreateTaxReport
{
    public record Command : IRequest<TaxReportDto>
    {
        public int TaxYear { get; init; }
        public decimal TotalCashDonations { get; init; }
        public decimal TotalNonCashDonations { get; init; }
        public string? Notes { get; init; }
    }

    public class Handler : IRequestHandler<Command, TaxReportDto>
    {
        private readonly ICharitableGivingTrackerContext _context;

        public Handler(ICharitableGivingTrackerContext context)
        {
            _context = context;
        }

        public async Task<TaxReportDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var taxReport = new TaxReport
            {
                TaxReportId = Guid.NewGuid(),
                TaxYear = request.TaxYear,
                TotalCashDonations = request.TotalCashDonations,
                TotalNonCashDonations = request.TotalNonCashDonations,
                Notes = request.Notes,
                GeneratedDate = DateTime.UtcNow
            };

            taxReport.CalculateTotals();

            _context.TaxReports.Add(taxReport);
            await _context.SaveChangesAsync(cancellationToken);

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
