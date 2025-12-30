// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CharitableGivingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CharitableGivingTracker.Api.Features.Organizations;

public class DeleteOrganization
{
    public record Command(Guid OrganizationId) : IRequest<bool>;

    public class Handler : IRequestHandler<Command, bool>
    {
        private readonly ICharitableGivingTrackerContext _context;

        public Handler(ICharitableGivingTrackerContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var organization = await _context.Organizations
                .FirstOrDefaultAsync(o => o.OrganizationId == request.OrganizationId, cancellationToken);

            if (organization == null)
                return false;

            _context.Organizations.Remove(organization);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
