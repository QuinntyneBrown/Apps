// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CharitableGivingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CharitableGivingTracker.Api.Features.Donations;

public class GetDonationsByOrganization
{
    public record Query(Guid OrganizationId) : IRequest<List<DonationDto>>;

    public class Handler : IRequestHandler<Query, List<DonationDto>>
    {
        private readonly ICharitableGivingTrackerContext _context;

        public Handler(ICharitableGivingTrackerContext context)
        {
            _context = context;
        }

        public async Task<List<DonationDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var donations = await _context.Donations
                .Include(d => d.Organization)
                .Where(d => d.OrganizationId == request.OrganizationId)
                .OrderByDescending(d => d.DonationDate)
                .ToListAsync(cancellationToken);

            return donations.Select(d => new DonationDto
            {
                DonationId = d.DonationId,
                OrganizationId = d.OrganizationId,
                Amount = d.Amount,
                DonationDate = d.DonationDate,
                DonationType = d.DonationType,
                ReceiptNumber = d.ReceiptNumber,
                IsTaxDeductible = d.IsTaxDeductible,
                Notes = d.Notes,
                OrganizationName = d.Organization?.Name
            }).ToList();
        }
    }
}
