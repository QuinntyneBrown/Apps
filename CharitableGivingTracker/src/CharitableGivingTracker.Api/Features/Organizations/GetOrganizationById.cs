// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CharitableGivingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CharitableGivingTracker.Api.Features.Organizations;

public class GetOrganizationById
{
    public record Query(Guid OrganizationId) : IRequest<OrganizationDto?>;

    public class Handler : IRequestHandler<Query, OrganizationDto?>
    {
        private readonly ICharitableGivingTrackerContext _context;

        public Handler(ICharitableGivingTrackerContext context)
        {
            _context = context;
        }

        public async Task<OrganizationDto?> Handle(Query request, CancellationToken cancellationToken)
        {
            var organization = await _context.Organizations
                .Include(o => o.Donations)
                .FirstOrDefaultAsync(o => o.OrganizationId == request.OrganizationId, cancellationToken);

            if (organization == null)
                return null;

            return new OrganizationDto
            {
                OrganizationId = organization.OrganizationId,
                Name = organization.Name,
                EIN = organization.EIN,
                Address = organization.Address,
                Website = organization.Website,
                Is501c3 = organization.Is501c3,
                Notes = organization.Notes,
                TotalDonations = organization.CalculateTotalDonations()
            };
        }
    }
}
