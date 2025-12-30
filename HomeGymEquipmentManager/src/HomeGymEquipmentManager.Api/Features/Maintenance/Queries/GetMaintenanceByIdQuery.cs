// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeGymEquipmentManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeGymEquipmentManager.Api.Features.Maintenance.Queries;

public class GetMaintenanceByIdQuery : IRequest<MaintenanceDto?>
{
    public Guid MaintenanceId { get; set; }
}

public class GetMaintenanceByIdQueryHandler : IRequestHandler<GetMaintenanceByIdQuery, MaintenanceDto?>
{
    private readonly IHomeGymEquipmentManagerContext _context;

    public GetMaintenanceByIdQueryHandler(IHomeGymEquipmentManagerContext context)
    {
        _context = context;
    }

    public async Task<MaintenanceDto?> Handle(GetMaintenanceByIdQuery request, CancellationToken cancellationToken)
    {
        var maintenance = await _context.Maintenances
            .FirstOrDefaultAsync(m => m.MaintenanceId == request.MaintenanceId, cancellationToken);

        return maintenance == null ? null : MaintenanceDto.FromEntity(maintenance);
    }
}
