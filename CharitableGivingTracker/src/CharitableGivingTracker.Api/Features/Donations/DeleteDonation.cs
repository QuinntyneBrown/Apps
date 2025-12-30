// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CharitableGivingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CharitableGivingTracker.Api.Features.Donations;

public class DeleteDonation
{
    public record Command(Guid DonationId) : IRequest<bool>;

    public class Handler : IRequestHandler<Command, bool>
    {
        private readonly ICharitableGivingTrackerContext _context;

        public Handler(ICharitableGivingTrackerContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var donation = await _context.Donations
                .FirstOrDefaultAsync(d => d.DonationId == request.DonationId, cancellationToken);

            if (donation == null)
                return false;

            _context.Donations.Remove(donation);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
