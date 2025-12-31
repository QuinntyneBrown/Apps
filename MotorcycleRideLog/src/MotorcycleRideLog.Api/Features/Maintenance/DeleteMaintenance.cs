// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MotorcycleRideLog.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MotorcycleRideLog.Api.Features.Maintenance;

public class DeleteMaintenance
{
    public class Command : IRequest<Unit>
    {
        public Guid MaintenanceId { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IMotorcycleRideLogContext _context;

        public Handler(IMotorcycleRideLogContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var maintenance = await _context.MaintenanceRecords
                .FirstOrDefaultAsync(x => x.MaintenanceId == request.MaintenanceId, cancellationToken)
                ?? throw new KeyNotFoundException($"Maintenance with ID {request.MaintenanceId} not found.");

            _context.MaintenanceRecords.Remove(maintenance);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
