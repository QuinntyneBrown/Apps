// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ApplianceWarrantyManualOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplianceWarrantyManualOrganizer.Api.Features.ServiceRecords;

public class DeleteServiceRecord
{
    public class Command : IRequest<Unit>
    {
        public Guid ServiceRecordId { get; set; }
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
            var serviceRecord = await _context.ServiceRecords
                .FirstOrDefaultAsync(sr => sr.ServiceRecordId == request.ServiceRecordId, cancellationToken);

            if (serviceRecord == null)
            {
                throw new KeyNotFoundException($"ServiceRecord with ID {request.ServiceRecordId} not found.");
            }

            _context.ServiceRecords.Remove(serviceRecord);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
