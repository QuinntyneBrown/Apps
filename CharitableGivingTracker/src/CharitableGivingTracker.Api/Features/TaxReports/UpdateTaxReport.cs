// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CharitableGivingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CharitableGivingTracker.Api.Features.TaxReports;

public class UpdateTaxReport
{
    public record Command : IRequest<TaxReportDto?>
    {
        public Guid TaxReportId { get; init; }
        public int TaxYear { get; init; }
        public decimal TotalCashDonations { get; init; }
        public decimal TotalNonCashDonations { get; init; }
        public string? Notes { get; init; }
    }

    public class Handler : IRequestHandler<Command, TaxReportDto?>
    {
        private readonly ICharitableGivingTrackerContext _context;

        public Handler(ICharitableGivingTrackerContext context)
        {
            _context = context;
        }

        public async Task<TaxReportDto?> Handle(Command request, CancellationToken cancellationToken)
        {
            var taxReport = await _context.TaxReports
                .FirstOrDefaultAsync(t => t.TaxReportId == request.TaxReportId, cancellationToken);

            if (taxReport == null)
                return null;

            taxReport.TaxYear = request.TaxYear;
            taxReport.TotalCashDonations = request.TotalCashDonations;
            taxReport.TotalNonCashDonations = request.TotalNonCashDonations;
            taxReport.Notes = request.Notes;

            taxReport.CalculateTotals();

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
