// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ApplianceWarrantyManualOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplianceWarrantyManualOrganizer.Api.Features.Appliances;

public class DeleteAppliance
{
    public class Command : IRequest<Unit>
    {
        public Guid ApplianceId { get; set; }
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
            var appliance = await _context.Appliances
                .FirstOrDefaultAsync(a => a.ApplianceId == request.ApplianceId, cancellationToken);

            if (appliance == null)
            {
                throw new KeyNotFoundException($"Appliance with ID {request.ApplianceId} not found.");
            }

            _context.Appliances.Remove(appliance);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
