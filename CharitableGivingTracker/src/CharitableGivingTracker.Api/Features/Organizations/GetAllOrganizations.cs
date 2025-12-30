// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CharitableGivingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CharitableGivingTracker.Api.Features.Organizations;

public class GetAllOrganizations
{
    public record Query : IRequest<List<OrganizationDto>>;

    public class Handler : IRequestHandler<Query, List<OrganizationDto>>
    {
        private readonly ICharitableGivingTrackerContext _context;

        public Handler(ICharitableGivingTrackerContext context)
        {
            _context = context;
        }

        public async Task<List<OrganizationDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var organizations = await _context.Organizations
                .Include(o => o.Donations)
                .OrderBy(o => o.Name)
                .ToListAsync(cancellationToken);

            return organizations.Select(o => new OrganizationDto
            {
                OrganizationId = o.OrganizationId,
                Name = o.Name,
                EIN = o.EIN,
                Address = o.Address,
                Website = o.Website,
                Is501c3 = o.Is501c3,
                Notes = o.Notes,
                TotalDonations = o.CalculateTotalDonations()
            }).ToList();
        }
    }
}
