// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ApplianceWarrantyManualOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplianceWarrantyManualOrganizer.Api.Features.Manuals;

public class DeleteManual
{
    public class Command : IRequest<Unit>
    {
        public Guid ManualId { get; set; }
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
            var manual = await _context.Manuals
                .FirstOrDefaultAsync(m => m.ManualId == request.ManualId, cancellationToken);

            if (manual == null)
            {
                throw new KeyNotFoundException($"Manual with ID {request.ManualId} not found.");
            }

            _context.Manuals.Remove(manual);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
