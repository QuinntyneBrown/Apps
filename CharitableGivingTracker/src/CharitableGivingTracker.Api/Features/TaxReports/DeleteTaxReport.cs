// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CharitableGivingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CharitableGivingTracker.Api.Features.TaxReports;

public class DeleteTaxReport
{
    public record Command(Guid TaxReportId) : IRequest<bool>;

    public class Handler : IRequestHandler<Command, bool>
    {
        private readonly ICharitableGivingTrackerContext _context;

        public Handler(ICharitableGivingTrackerContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var taxReport = await _context.TaxReports
                .FirstOrDefaultAsync(t => t.TaxReportId == request.TaxReportId, cancellationToken);

            if (taxReport == null)
                return false;

            _context.TaxReports.Remove(taxReport);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
