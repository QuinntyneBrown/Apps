// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CharitableGivingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CharitableGivingTracker.Api.Features.Organizations;

public class CreateOrganization
{
    public record Command : IRequest<OrganizationDto>
    {
        public string Name { get; init; } = string.Empty;
        public string? EIN { get; init; }
        public string? Address { get; init; }
        public string? Website { get; init; }
        public bool Is501c3 { get; init; } = true;
        public string? Notes { get; init; }
    }

    public class Handler : IRequestHandler<Command, OrganizationDto>
    {
        private readonly ICharitableGivingTrackerContext _context;

        public Handler(ICharitableGivingTrackerContext context)
        {
            _context = context;
        }

        public async Task<OrganizationDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var organization = new Organization
            {
                OrganizationId = Guid.NewGuid(),
                Name = request.Name,
                EIN = request.EIN,
                Address = request.Address,
                Website = request.Website,
                Is501c3 = request.Is501c3,
                Notes = request.Notes
            };

            _context.Organizations.Add(organization);
            await _context.SaveChangesAsync(cancellationToken);

            var createdOrganization = await _context.Organizations
                .Include(o => o.Donations)
                .FirstOrDefaultAsync(o => o.OrganizationId == organization.OrganizationId, cancellationToken);

            return new OrganizationDto
            {
                OrganizationId = createdOrganization!.OrganizationId,
                Name = createdOrganization.Name,
                EIN = createdOrganization.EIN,
                Address = createdOrganization.Address,
                Website = createdOrganization.Website,
                Is501c3 = createdOrganization.Is501c3,
                Notes = createdOrganization.Notes,
                TotalDonations = createdOrganization.CalculateTotalDonations()
            };
        }
    }
}
