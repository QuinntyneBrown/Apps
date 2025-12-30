// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ApplianceWarrantyManualOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplianceWarrantyManualOrganizer.Api.Features.Warranties;

public class DeleteWarranty
{
    public class Command : IRequest<Unit>
    {
        public Guid WarrantyId { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IApplianceWarrantyManualOrganizerContext _context;

        public Handler(IApplianceWarrantyManualOrganizerContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var warranty = await _context.Warranties
                .FirstOrDefaultAsync(w => w.WarrantyId == request.WarrantyId, cancellationToken);

            if (warranty == null)
            {
                throw new KeyNotFoundException($"Warranty with ID {request.WarrantyId} not found.");
            }

            _context.Warranties.Remove(warranty);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
