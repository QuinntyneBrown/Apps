// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeGymEquipmentManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeGymEquipmentManager.Api.Features.Maintenance.Commands;

public class DeleteMaintenanceCommand : IRequest<Unit>
{
    public Guid MaintenanceId { get; set; }
}

public class DeleteMaintenanceCommandHandler : IRequestHandler<DeleteMaintenanceCommand, Unit>
{
    private readonly IHomeGymEquipmentManagerContext _context;

    public DeleteMaintenanceCommandHandler(IHomeGymEquipmentManagerContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteMaintenanceCommand request, CancellationToken cancellationToken)
    {
        var maintenance = await _context.Maintenances
            .FirstOrDefaultAsync(m => m.MaintenanceId == request.MaintenanceId, cancellationToken);

        if (maintenance == null)
        {
            throw new KeyNotFoundException($"Maintenance with ID {request.MaintenanceId} not found.");
        }

        _context.Maintenances.Remove(maintenance);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
