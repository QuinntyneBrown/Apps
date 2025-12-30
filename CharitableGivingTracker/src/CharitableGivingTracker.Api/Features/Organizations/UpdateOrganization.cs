// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CharitableGivingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CharitableGivingTracker.Api.Features.Organizations;

public class UpdateOrganization
{
    public record Command : IRequest<OrganizationDto?>
    {
        public Guid OrganizationId { get; init; }
        public string Name { get; init; } = string.Empty;
        public string? EIN { get; init; }
        public string? Address { get; init; }
        public string? Website { get; init; }
        public bool Is501c3 { get; init; }
        public string? Notes { get; init; }
    }

    public class Handler : IRequestHandler<Command, OrganizationDto?>
    {
        private readonly ICharitableGivingTrackerContext _context;

        public Handler(ICharitableGivingTrackerContext context)
        {
            _context = context;
        }

        public async Task<OrganizationDto?> Handle(Command request, CancellationToken cancellationToken)
        {
            var organization = await _context.Organizations
                .Include(o => o.Donations)
                .FirstOrDefaultAsync(o => o.OrganizationId == request.OrganizationId, cancellationToken);

            if (organization == null)
                return null;

            organization.Name = request.Name;
            organization.EIN = request.EIN;
            organization.Address = request.Address;
            organization.Website = request.Website;
            organization.Is501c3 = request.Is501c3;
            organization.Notes = request.Notes;

            await _context.SaveChangesAsync(cancellationToken);

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
