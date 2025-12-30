// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CharitableGivingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CharitableGivingTracker.Api.Features.Donations;

public class GetDonationById
{
    public record Query(Guid DonationId) : IRequest<DonationDto?>;

    public class Handler : IRequestHandler<Query, DonationDto?>
    {
        private readonly ICharitableGivingTrackerContext _context;

        public Handler(ICharitableGivingTrackerContext context)
        {
            _context = context;
        }

        public async Task<DonationDto?> Handle(Query request, CancellationToken cancellationToken)
        {
            var donation = await _context.Donations
                .Include(d => d.Organization)
                .FirstOrDefaultAsync(d => d.DonationId == request.DonationId, cancellationToken);

            if (donation == null)
                return null;

            return new DonationDto
            {
                DonationId = donation.DonationId,
                OrganizationId = donation.OrganizationId,
                Amount = donation.Amount,
                DonationDate = donation.DonationDate,
                DonationType = donation.DonationType,
                ReceiptNumber = donation.ReceiptNumber,
                IsTaxDeductible = donation.IsTaxDeductible,
                Notes = donation.Notes,
                OrganizationName = donation.Organization?.Name
            };
        }
    }
}
