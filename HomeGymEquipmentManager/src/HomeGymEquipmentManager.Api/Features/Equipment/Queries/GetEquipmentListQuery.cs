// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeGymEquipmentManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeGymEquipmentManager.Api.Features.Equipment.Queries;

public class GetEquipmentListQuery : IRequest<List<EquipmentDto>>
{
    public Guid? UserId { get; set; }
    public bool? IsActive { get; set; }
}

public class GetEquipmentListQueryHandler : IRequestHandler<GetEquipmentListQuery, List<EquipmentDto>>
{
    private readonly IHomeGymEquipmentManagerContext _context;

    public GetEquipmentListQueryHandler(IHomeGymEquipmentManagerContext context)
    {
        _context = context;
    }

    public async Task<List<EquipmentDto>> Handle(GetEquipmentListQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Equipment.AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(e => e.UserId == request.UserId.Value);
        }

        if (request.IsActive.HasValue)
        {
            query = query.Where(e => e.IsActive == request.IsActive.Value);
        }

        var equipment = await query
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync(cancellationToken);

        return equipment.Select(EquipmentDto.FromEntity).ToList();
    }
}
