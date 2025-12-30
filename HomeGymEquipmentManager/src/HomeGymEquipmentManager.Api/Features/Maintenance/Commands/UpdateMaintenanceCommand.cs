// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeGymEquipmentManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeGymEquipmentManager.Api.Features.Maintenance.Commands;

public class UpdateMaintenanceCommand : IRequest<MaintenanceDto>
{
    public Guid MaintenanceId { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal? Cost { get; set; }
    public DateTime? NextMaintenanceDate { get; set; }
    public string? Notes { get; set; }
}

public class UpdateMaintenanceCommandHandler : IRequestHandler<UpdateMaintenanceCommand, MaintenanceDto>
{
    private readonly IHomeGymEquipmentManagerContext _context;

    public UpdateMaintenanceCommandHandler(IHomeGymEquipmentManagerContext context)
    {
        _context = context;
    }

    public async Task<MaintenanceDto> Handle(UpdateMaintenanceCommand request, CancellationToken cancellationToken)
    {
        var maintenance = await _context.Maintenances
            .FirstOrDefaultAsync(m => m.MaintenanceId == request.MaintenanceId, cancellationToken);

        if (maintenance == null)
        {
            throw new KeyNotFoundException($"Maintenance with ID {request.MaintenanceId} not found.");
        }

        maintenance.MaintenanceDate = request.MaintenanceDate;
        maintenance.Description = request.Description;
        maintenance.Cost = request.Cost;
        maintenance.NextMaintenanceDate = request.NextMaintenanceDate;
        maintenance.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return MaintenanceDto.FromEntity(maintenance);
    }
}
