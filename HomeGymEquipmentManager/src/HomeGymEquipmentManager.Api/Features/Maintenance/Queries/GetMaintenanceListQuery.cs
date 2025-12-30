// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeGymEquipmentManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeGymEquipmentManager.Api.Features.Maintenance.Queries;

public class GetMaintenanceListQuery : IRequest<List<MaintenanceDto>>
{
    public Guid? UserId { get; set; }
    public Guid? EquipmentId { get; set; }
}

public class GetMaintenanceListQueryHandler : IRequestHandler<GetMaintenanceListQuery, List<MaintenanceDto>>
{
    private readonly IHomeGymEquipmentManagerContext _context;

    public GetMaintenanceListQueryHandler(IHomeGymEquipmentManagerContext context)
    {
        _context = context;
    }

    public async Task<List<MaintenanceDto>> Handle(GetMaintenanceListQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Maintenances.AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(m => m.UserId == request.UserId.Value);
        }

        if (request.EquipmentId.HasValue)
        {
            query = query.Where(m => m.EquipmentId == request.EquipmentId.Value);
        }

        var maintenances = await query
            .OrderByDescending(m => m.MaintenanceDate)
            .ToListAsync(cancellationToken);

        return maintenances.Select(MaintenanceDto.FromEntity).ToList();
    }
}
